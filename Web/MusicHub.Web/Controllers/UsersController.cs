namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MusicHub.Common;
    using MusicHub.Common.Settings;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Services.Messaging;
    using MusicHub.Web.Extensions.Extensions;
    using MusicHub.Web.Infrastucture;
    using MusicHub.Web.Infrastucture.Configurations;
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.NotificationModels;
    using MusicHub.Web.ViewModels.Users;
    using MusicHub.Web.ViewModels.VerificationCodeModels;
    using static MusicHub.Common.GlobalConstants;
    using static MusicHub.Common.ModelConstants;

    public class UsersController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly IUserLoginHistoryService userLoginHistoryService;
        private readonly IVerificationCodeService verificationCodeService;
        private readonly IEmailSender emailSender;

        public UsersController(
            IProfileService profileService,
            IVerificationCodeService verificationCodeService,
            UserManager<User> userManager,
            IEmailSender emailSender,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger,
            IUserLoginHistoryService userLoginHistoryService = null)
            : base(userManager, signInManager, logger)
        {
            this.profileService = profileService;
            this.verificationCodeService = verificationCodeService;
            this.emailSender = emailSender;
            this.userLoginHistoryService = userLoginHistoryService;
        }

        // [Authorize]
        // [Permission(PermissionType.UserAdministration)]
        [HttpGet(nameof(All) + "/{page}")]
        public ActionResult<UserPageModel<UserViewModel>> All(int page)
            => this.Ok(this.profileService.All<UserViewModel>(page, PaginationData.UsersPerPage));

        [Authorize]
        [HttpGet(nameof(List) + "/{page}")]
        public ActionResult<UserPageModel<UserBriefInfoViewModel>> List(int page)
            => this.Ok(this.profileService.All<UserBriefInfoViewModel>(page, PaginationData.UsersPerPage));

        [Authorize]
        [HttpGet(nameof(IsProfileMine) + "/{id}")]
        public ActionResult<bool> IsProfileMine(string id)
         => this.Ok(this.User.GetUserId() == id);

        [HttpGet("{id}")]
        public IActionResult Get(string id)
            => this.Ok(this.profileService.GetById<UserEditInputModel>(id));

        [Authorize]
        [HttpGet(nameof(MyProfile))]
        public IActionResult MyProfile() => this.Ok(this.profileService.GetById<UserEditInputModel>(this.User.GetUserId()));

        [Authorize]
        [HttpPut(nameof(UploadProfileImage))]
        public async Task<IActionResult> UploadProfileImage(UserProfilePictueInputModel model)
        {
            string userId = this.User.GetUserId();
            await this.profileService.UploadProfileImage(userId, model);
            return this.Ok();
        }

        [HttpGet(nameof(ResendConfirmEmail))]
        public async Task<ActionResult<string>> ResendConfirmEmail([FromQuery] ResendEmailConfirmInputModel model)
        {
            var user = this.profileService.GetByEmail<ResendEmailConfirmDetailsModel>(model.Email);
            if (user == null)
            {
                return this.BadRequest("Email does not exist.");
            }

            var verificationCode = this.verificationCodeService.GetActivatedVerificationCode<VerificationCodeViewModel>(user.Id, VerificationType.ConfirmEmail);

            await this.SendConfirmEmail(user.UserName, user.Email, user.Id, verificationCode.Code);
            return this.Ok();
        }

        [HttpGet(nameof(GetByEmail))]
        public ActionResult<UserViewModel> GetByEmail([FromQuery] UserFilter model)
            => this.Ok(this.profileService.GetByEmail<UserViewModel>(model.Email));

        //Old login method without 2FA
        //// <summary>Login user.</summary>
        ///// <param name="model">Login information.</param>
        ///// <returns>Token</returns>
        //[HttpGet(nameof(Login))]
        //public async Task<ActionResult<User>> Login([FromQuery] ViewModels.Users.LoginModel model, [FromServices] IOptions<JwtSettings> settings)
        //{
        //    if (!this.profileService.IsEmailConfirm(model.Username))
        //    {
        //        return this.BadRequest("Email is not verificated. Please confirm it before use your profile.");
        //    }

        //    var jwtToken = await this.userManager.Authenticate(model.Username, model.Password, settings.Value);
        //    if (jwtToken == null)
        //    {
        //        return this.BadRequest("Username or password is incorrect.");
        //    }

        //    var user = await this.userManager.FindByNameAsync(model.Username);
        //    var ipAddress = this.HttpContext.Connection.RemoteIpAddress?.ToString();
        //    var userAgent = this.Request.Headers["User-Agent"].ToString();
        //    await this.userLoginHistoryService.HandleLoginAsync(user, ipAddress, userAgent);
        //    return new JsonResult(jwtToken);
        //}

        [HttpGet(nameof(Login))]
        public async Task<ActionResult> Login([FromQuery] ViewModels.Users.LoginModel model, [FromServices] IOptions<JwtSettings> settings)
        {
            if (!this.profileService.IsEmailConfirm(model.Username))
            {
                return this.BadRequest("Email is not verificated.");
            }

            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return this.BadRequest("Username or password is incorrect.");
            }

            var jwtToken = await this.userManager.Authenticate(model.Username, model.Password, settings.Value);

            if (jwtToken == null)
            {
                return this.BadRequest("Username or password is incorrect.");
            }

            // 2FA ENABLED
            if (user.TwoFactorEnabled)
            {
                var verificationCodeCreateModel = new VerificationCodeCreateInputModel
                {
                    UserId = user.Id,
                    Type = VerificationType.TwoFactorLogin,
                };

                await this.verificationCodeService.Create(verificationCodeCreateModel);

                var data = new Dictionary<string, string>
                {
                    { "userName", user.UserName },
                    { "verificationCode", verificationCodeCreateModel.Code }
                };

                var htmlContent = MessagesContentTemplates.CreateMessageContent(EmailSubjects.TwoFactorLogin,data);
                await this.emailSender.SendEmailAsync(user.Email,EmailSubjects.TwoFactorLogin,htmlContent);
                return this.Ok(new
                {
                    requiresTwoFactor = true,
                    userId = user.Id,
                    token = "",
                });
            }

            // NORMAL LOGIN
            var ipAddress = this.HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = this.Request.Headers["User-Agent"].ToString();
            await this.userLoginHistoryService.HandleLoginAsync(user, ipAddress, userAgent);
            return this.Ok(new
            {
                requiresTwoFactor = false,
                userId = user.Id,
                token = jwtToken,
            });
        }

        [HttpPut(nameof(VerifyTwoFactor) + "/{id}")]
        public async Task<ActionResult> VerifyTwoFactor(string id, VerificationCodeCheckInputModel model,[FromServices] IOptions<JwtSettings> settings)
        {
            if (!this.profileService.UserExistsById(id))
            {
                this.ModelState.AddModelError("User", "User not found.");
            }

            if (!this.verificationCodeService.IsVerificationCodeCorrect(id, model))
            {
                this.ModelState.AddModelError("VerificationCode", "Wrong verification code.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            await this.verificationCodeService.SetVerificationCodeIsUsed(id, model.Code);
            var user = await this.userManager.FindByIdAsync(id);
            var jwtToken = await this.userManager.GenerateJwtToken(user, settings.Value);
            var ipAddress = this.HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = this.Request.Headers["User-Agent"].ToString();
            await this.userLoginHistoryService.HandleLoginAsync(user, ipAddress, userAgent);
            return this.Ok(new
            {
                token = jwtToken,
            });
        }

        [HttpPut(nameof(ConfirmEmail) + "/{id}")]
        public async Task<ActionResult> ConfirmEmail(string id, VerificationCodeCheckInputModel model)
        {
            if (!this.profileService.UserExistsById(id))
            {
                this.ModelState.AddModelError("User", $"User not found.");
            }

            if (!this.verificationCodeService.IsVerificationCodeCorrect(id, model))
            {
                this.ModelState.AddModelError("VerificationCode", $"Wrong verification code.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            await this.verificationCodeService.SetVerificationCodeIsUsed(id, model.Code);
            await this.profileService.ConfirmEmail(id);
            return this.Ok();
        }

        [Authorize]
        [AllowAnonymous]
        [HttpGet(nameof(ValidateToken))]
        public IActionResult ValidateToken() => this.Ok(this.User.Identity.IsAuthenticated);

        [Authorize]
        [AllowAnonymous]
        [HttpGet(nameof(HasPermission))]
        public IActionResult HasPermission(PermissionType permissionType)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Ok(false);
            }

            return this.Ok(this.profileService.HasPermissions(this.User.GetUserId(), permissionType));
        }

        [HttpPut(nameof(ChangePassword))]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeInputModel model)
        {
            User user = await this.userManager.GetUserAsync(this.User);

            IdentityResult result = await this.userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result.ToActionResult();
        }

        [HttpPut(nameof(ChangePasswordWithoutAuth) + "/{id}")]
        public async Task<ActionResult> ChangePasswordWithoutAuth(string id, PasswordChangeWithoutAuthInputModel model)
        {
            User user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

            var result = await this.userManager.ResetPasswordAsync(user, token, model.Password);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreateInputModel model)
        {
            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < UserConstants.PasswordMinLength)
            {
                this.ModelState.AddModelError(nameof(UserCreateInputModel.Password), $"Password must be at least {UserConstants.PasswordMinLength} symbols.");
            }

            this.ValidateUser(model);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            User user = model.To<User>();

            IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, Roles.User);
                await this.profileService.AddDefaultSettings(user.Id);
                var verificationCodeCreateModel = new VerificationCodeCreateInputModel();
                verificationCodeCreateModel.UserId = user.Id;
                verificationCodeCreateModel.Type = VerificationType.ConfirmEmail;
                await this.verificationCodeService.Create(verificationCodeCreateModel);
                var data = new Dictionary<string, string>();
                data.Add("userName", user.UserName);
                data.Add("userId", user.Id);
                data.Add("verificationCode", verificationCodeCreateModel.Code);
                var htmlContent = MessagesContentTemplates.CreateMessageContent(EmailSubjects.ConfirmEmail, data);
                await this.emailSender.SendEmailAsync(
                    user.Email,
                    EmailSubjects.ConfirmEmail,
                    htmlContent);

                return this.Ok(new { userId = user.Id });
            }
            else
            {
                return this.BadRequest(result.Errors.Select(e => e.Description));
            }
        }

        private async Task SendConfirmEmail(string userName, string email, string userId, string verificationCode)
        {
            var data = new Dictionary<string, string>();
            data.Add("userName", userName);
            data.Add("userId", userId);
            data.Add("verificationCode", verificationCode);
            var htmlContent = MessagesContentTemplates.CreateMessageContent(EmailSubjects.ConfirmEmail, data);
            await this.emailSender.SendEmailAsync(
                email,
                EmailSubjects.ConfirmEmail,
                htmlContent);
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(UserEditInputModel model, string id)
        {
            this.ValidateUser(model, id);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            User user = await this.profileService.Update(id, model);
            await this.ClearUserRoles(user);
            await this.userManager.AddToRolesAsync(user, model.Roles);

            return this.Ok();
        }

        [Authorize]
        [HttpPut(nameof(EditMyProfile))]
        public async Task<ActionResult> EditMyProfile(UserEditModel model)
        {
            var id = this.User.GetUserId();
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            User user = await this.profileService.Update(id, model);

            return this.Ok();
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPut(nameof(ChangeRoles) + "/{id}")]
        public async Task<ActionResult> ChangeRoles(UserChangeRolesInputModel model, string id)
        {
            // this.ValidateUser(model, id);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            User user = this.profileService.GetUser(id);
            await this.ClearUserRoles(user);
            await this.userManager.AddToRolesAsync(user, model.RoleIds
                .Select(r => r.RoleName)
                .ToList());

            return this.Ok();
        }

        [Authorize]
        [Permission(PermissionType.ManageUserPermissions)]
        [HttpPatch(nameof(ChangePermissions))]
        public async Task<ActionResult> ChangePermissions(ChangePermissionsInputModel model)
        {
            User user = this.userManager.Users.FirstOrDefault(u => u.Id == model.Id);
            await this.ClearUserRoles(user);
            IdentityResult result = await this.userManager.AddToRolesAsync(user, model.Roles);
            return result.ToActionResult();
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await this.profileService.Delete(id);
            return this.Ok();
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPut(nameof(RenewUser))]
        public async Task<ActionResult> RenewUser([FromBody] string id)
        {
            await this.profileService.Renew(id);
            return this.Ok();
        }

        [Authorize]
        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpGet(nameof(FindUsers))]
        public ActionResult FindUsers(int page, [FromQuery] FindUserInputModel model)
            => this.Ok(this.profileService.Search(page, PaginationData.UsersPerPage, model.SearchQuery));

        [Authorize]
        //[Permission(PermissionType.FindAndReplaceUsers)]
        [HttpGet(nameof(AdvancedSearch) + "/{page}")]
        public ActionResult AdvancedSearch(int page, [FromQuery] UserFilter model) 
            => this.Ok(this.profileService.Search(page, PaginationData.UsersPerPage, model));

        [Authorize]
        [Permission(PermissionType.FindAndReplaceUsers)]
        [HttpGet(nameof(GetFullNames) + "/{page}")]
        public ActionResult GetFullNames(int page) => this.Ok(this.profileService.All<UserFullNameViewModel>(page, PaginationData.UsersPerPage));

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpGet("[action]/{id}")]
        public ActionResult GetUserName(string id) => this.Ok(this.profileService.GetById<UserFullNameViewModel>(id));

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpPost("[action]/{settingId}")]
        public async Task<ActionResult> ToggleSetting(int settingId)
        {
            await this.profileService.ToggleSetting(settingId);
            return this.Ok();
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        [HttpGet(nameof(Settings))]
        public ActionResult Settings()
            => this.Ok(this.profileService.GetSettings<UserSettingViewModel>(this.User.GetUserId()));

        [Authorize]
        private void ValidateUser(UserInputModel model, string id = null)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                this.ModelState.AddModelError(nameof(UserInputModel.UserName), "Username is required!");
            }
            else if (this.profileService.UserExistsByUsername(model.UserName, id))
            {
                this.ModelState.AddModelError(nameof(UserInputModel.UserName), "User with the same 'Username' already exists.");
            }

            if (this.profileService.UserExistsByEmail(model.Email, id))
            {
                this.ModelState.AddModelError(nameof(UserInputModel.Email), "User with the same 'Email' already exists.");
            }
        }

        [Authorize]
        [Permission(PermissionType.UserAdministration)]
        private async Task ClearUserRoles(User user)
        {
            IList<string> roles = await this.userManager.GetRolesAsync(user);
            await this.userManager.RemoveFromRolesAsync(user, roles);
        }
    }
}
