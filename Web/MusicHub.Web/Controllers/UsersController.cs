using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Common.Settings;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.Extensions.Extensions;
using MusicHub.Web.Infrastucture;
using MusicHub.Web.Infrastucture.Extensions;
using MusicHub.Web.ViewModels.NotificationModels;
using MusicHub.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using MusicHub.Services.Mapping;
using System.Threading.Tasks;
using static MusicHub.Common.ModelConstants;
using Microsoft.Extensions.Options;
using MusicHub.Web.Infrastucture.Configurations;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly INotificationsService notificationsService;
        private readonly IActiveDirectoryService activeDirectoryService;
        private readonly AppSettings appSettings;

        public UsersController(
            IProfileService profileService,
            INotificationsService notificationsService,
            IActiveDirectoryService activeDirectoryService,
            AppSettings appSettings,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.profileService = profileService;
            this.notificationsService = notificationsService;
            this.activeDirectoryService = activeDirectoryService;
            this.appSettings = appSettings;
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpGet("/{page}")]
        public ActionResult All(int page) => Ok(profileService.All<UserViewModel>(page, PaginationData.UsersPerPage));

        [Authorize]
        [HttpGet(nameof(List) + "/{page}")]
        public ActionResult List(int page) => Ok(profileService.All<UserBriefInfoViewModel>(page, PaginationData.UsersPerPage));

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpGet("{id}")]
        public IActionResult Get(string id) => Ok(profileService.GetById<UserEditInputModel>(id));

        [Authorize]
        [HttpGet(nameof(MyProfile))]
        public IActionResult MyProfile() => Ok(profileService.GetById<UserDetailsViewModel>(User.GetUserId()));

        [Authorize]
        [HttpPost(nameof(UploadProfileImage))]
        public async Task<IActionResult> UploadProfileImage(UserProfilePictueInputModel model)
        {
            string userId = User.GetUserId();
            await profileService.UploadProfileImage(userId, model);
            return Ok();
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult> Login(ViewModels.Users.LoginModel model, [FromServices] IOptions<JwtSettings> settings)
        {
            var jwtToken = await this.userManager.Authenticate(model.Username, model.Password, settings.Value);

            if (jwtToken == null)
            {
                return this.BadRequest("Username or password is incorrect.");
            }

            return new JsonResult(jwtToken);
        }

        [Authorize]
        [AllowAnonymous]
        [HttpGet(nameof(ValidateToken))]
        public IActionResult ValidateToken() => Ok(User.Identity.IsAuthenticated);

        [Authorize]
        [AllowAnonymous]
        [HttpGet(nameof(HasPermission))]
        public IActionResult HasPermission(PermissionType permissionType)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(false);
            }

            return Ok(profileService.HasPermissions(User.GetUserId(), permissionType));
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPost(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeInputModel model)
        {
            User user = await userManager.GetUserAsync(User);

            IdentityResult result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreateInputModel model)
        {
            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < UserConstants.PasswordMinLength)
            {
                ModelState.AddModelError(nameof(UserCreateInputModel.Password), $"Password must be at least {UserConstants.PasswordMinLength} symbols.");
            }

            ValidateUser(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Errors());
            }

            User user = model.To<User>();
            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.User);
                await profileService.AddDefaultSettings(user.Id);
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(UserEditInputModel model, string id)
        {
            ValidateUser(model, id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Errors());
            }

            User user = await profileService.Update(id, model);
            await ClearUserRoles(user);
            await userManager.AddToRolesAsync(user, model.Roles);

            return Ok();
        }

        [Authorize]
        [Permission(PermissionType.ManageUserPermissions)]
        [HttpPatch(nameof(ChangePermissions))]
        public async Task<ActionResult> ChangePermissions(ChangePermissionsInputModel model)
        {
            User user = userManager.Users.FirstOrDefault(u => u.Id == model.Id);
            await ClearUserRoles(user);
            IdentityResult result = await userManager.AddToRolesAsync(user, model.Roles);
            return result.ToActionResult();
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await profileService.Delete(id);
            return Ok();
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPost(nameof(RenewUser))]
        public async Task<ActionResult> RenewUser([FromBody] string id)
        {
            await profileService.Renew(id);
            return Ok();
        }

        [Authorize]
        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpPost(nameof(FindUsers))]
        public ActionResult FindUsers(int page, [FromBody] FindUserInputModel model) => Ok(profileService.Search(page, PaginationData.UsersPerPage, model.SearchQuery));

        [Authorize]
        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpPost(nameof(AdvancedSearch) + "/{page}")]
        public ActionResult AdvancedSearch(int page, [FromBody] UserFilter model) => Ok(profileService.Search(page, PaginationData.UsersPerPage, model));

        [Authorize]
        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpGet(nameof(GetFullNames) + "/{page}")]
        public ActionResult GetFullNames(int page) => Ok(profileService.All<UserFullNameViewModel>(page, PaginationData.UsersPerPage));

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpGet("[action]/{id}")]
        public ActionResult GetUserName(string id) => Ok(profileService.GetById<UserFullNameViewModel>(id));

        [Authorize]
        [HttpGet(nameof(Notifications) + "/{page}")]
        public ActionResult Notifications(int page)
        {
            var entitesPerPage = PaginationData.NotificationPerPage;
            return this.Ok(this.notificationsService.GetUserNotifications<NotificationViewModel>(page, entitesPerPage,  User.GetUserId()));
        }

        [Authorize]
        [HttpPost("[action]/{notificationId}")]
        public async Task<ActionResult> MarkNotificationAsSeen(string notificationId)
        {
            if (User.GetUserId() != notificationsService.GetNotificationOwner(notificationId))
            {
                return Forbid();
            }

            await notificationsService.MarkAsSeen(notificationId);
            return Ok();
        }

        [Authorize]
        [HttpPost("[action]/{settingId}")]
        public async Task<ActionResult> ToggleSetting(int settingId)
        {
            await profileService.ToggleSetting(settingId);
            return Ok();
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPost(nameof(Settings))]
        public ActionResult Settings()
        {
            string userId = User.GetUserId();
            IEnumerable<UserSettingViewModel> userSettings = profileService.GetSettings<UserSettingViewModel>(userId);
            return Ok(userSettings);
        }

        [Authorize]
        private void ValidateUser(UserInputModel model, string id = null)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                ModelState.AddModelError(nameof(UserInputModel.UserName), "Username is required!");

            }
            else if (profileService.UserExistsByUsername(model.UserName, id))
            {
                ModelState.AddModelError(nameof(UserInputModel.UserName), "User with the same 'Username' already exists.");
            }

            if (profileService.UserExistsByEmail(model.Email, id))
            {
                ModelState.AddModelError(nameof(UserInputModel.Email), "User with the same 'Email' already exists.");
            }
        }

        [Authorize]
        private async Task ClearUserRoles(User user)
        {
            IList<string> roles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, roles);
        }
    }
}
