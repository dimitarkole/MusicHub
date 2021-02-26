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

namespace MusicHub.Web.Controllers
{
    [Authorize]
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

        [Permission(PermissionType.UserAdministration)]
        [HttpGet]
        public ActionResult All() => Ok(profileService.All<UserViewModel>());

        [HttpGet(nameof(List))]
        public ActionResult List() => Ok(profileService.All<UserBriefInfoViewModel>());

        [Permission(PermissionType.UserAdministration)]
        [HttpGet("{id}")]
        public IActionResult Get(string id) => Ok(profileService.GetById<UserEditInputModel>(id));

        [HttpGet(nameof(MyProfile))]
        public IActionResult MyProfile() => Ok(profileService.GetById<UserDetailsViewModel>(User.GetUserId()));

        [HttpPost(nameof(UploadProfileImage))]
        public async Task<IActionResult> UploadProfileImage(UserProfilePictueInputModel model)
        {
            string userId = User.GetUserId();
            await profileService.UploadProfileImage(userId, model);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult> Login(ViewModels.Users.LoginModel model)
        {
            LoggedUserModel loggedUser = await userManager.Authenticate(activeDirectoryService, model.Username, model.Password, appSettings);

            if (loggedUser == null)
            {
                return BadRequest(new string[] { "Username or password are incorrect." });
            }

            return Ok(loggedUser);
        }

        [AllowAnonymous]
        [HttpGet(nameof(ValidateToken))]
        public IActionResult ValidateToken() => Ok(User.Identity.IsAuthenticated);

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


        [Permission(PermissionType.UserAdministration)]
        [HttpPost(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeInputModel model)
        {
            User user = await userManager.GetUserAsync(User);

            IdentityResult result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result.ToActionResult();
        }

        [Permission(PermissionType.UserAdministration)]
        [HttpPost]
        public async Task<ActionResult> Create(UserCreateInputModel model)
        {
            if (!model.IsAd)
            {
                if (string.IsNullOrEmpty(model.Password) || model.Password.Length < UserConstants.PasswordMinLength)
                {
                    ModelState.AddModelError(nameof(UserCreateInputModel.Password), $"Password must be at least {UserConstants.PasswordMinLength} symbols.");
                }
            }

            ValidateUser(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Errors());
            }

            if (model.IsAd)
            {
                model.Password = Guid.NewGuid().ToString();
            }

            User user = model.To<User>();
            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, model.Roles);
                await profileService.AddDefaultSettings(user.Id);
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }
        }

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

        [Permission(PermissionType.ManageUserPermissions)]
        [HttpPatch(nameof(ChangePermissions))]
        public async Task<ActionResult> ChangePermissions(ChangePermissionsInputModel model)
        {
            User user = userManager.Users.FirstOrDefault(u => u.Id == model.Id);
            await ClearUserRoles(user);
            IdentityResult result = await userManager.AddToRolesAsync(user, model.Roles);
            return result.ToActionResult();
        }

        [Permission(PermissionType.UserAdministration)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await profileService.Delete(id);
            return Ok();
        }

        [Permission(PermissionType.UserAdministration)]
        [HttpPost(nameof(RenewUser))]
        public async Task<ActionResult> RenewUser([FromBody] string id)
        {
            await profileService.Renew(id);
            return Ok();
        }

        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpPost(nameof(FindUsers))]
        public ActionResult FindUsers([FromBody] FindUserInputModel model) => Ok(profileService.Search(model.SearchQuery));

        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpPost(nameof(AdvancedSearch))]
        public ActionResult AdvancedSearch([FromBody] UserFilter model) => Ok(profileService.Search(model));

        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpGet(nameof(GetFullNames))]
        public ActionResult GetFullNames() => Ok(profileService.All<UserFullNameViewModel>());


        [Permission(PermissionType.UserAdministration)]
        [HttpGet("[action]/{id}")]
        public ActionResult GetUserName(string id) => Ok(profileService.GetById<UserFullNameViewModel>(id));

        [Authorize]
        [HttpGet(nameof(Notifications))]
        public ActionResult Notifications() => Ok(notificationsService.GetUserNotifications<NotificationViewModel>(User.GetUserId()));

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

        [Permission(PermissionType.UserAdministration)]
        [HttpPost(nameof(Settings))]
        public ActionResult Settings()
        {
            string userId = User.GetUserId();
            IEnumerable<UserSettingViewModel> userSettings = profileService.GetSettings<UserSettingViewModel>(userId);
            return Ok(userSettings);
        }

        private void ValidateUser(UserInputModel model, string id = null)
        {
            if (string.IsNullOrEmpty(model.Username))
            {
                ModelState.AddModelError(nameof(UserInputModel.Username), "Username is required!");

            }
            else if (profileService.UserExistsByUsername(model.Username, id))
            {
                ModelState.AddModelError(nameof(UserInputModel.Username), "User with the same 'Username' already exists.");
            }

            if (profileService.UserExistsByEmail(model.Email, id))
            {
                ModelState.AddModelError(nameof(UserInputModel.Email), "User with the same 'Email' already exists.");
            }
        }

        private async Task ClearUserRoles(User user)
        {
            IList<string> roles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, roles);
        }
    }
}
