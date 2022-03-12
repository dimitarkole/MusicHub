namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Messaging;
    using MusicHub.Web.Extensions;
    using MusicHub.Web.Extensions.Extensions;
    using MusicHub.Web.Infrastucture.Configurations;
    using MusicHub.Web.ViewModels.IdentityModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;
        private readonly IEmailSender emailSender;

        public IdentityController(
            IIdentityService identityService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.identityService = identityService;
            this.emailSender = emailSender;
        }

        /// <summary>Create new user.</summary>
        /// <param name="model">Personal data information</param>
        /// <returns>User data.</returns>
        [HttpPost(nameof(Create))]
        public async Task<ActionResult<ApplicationUser>> Create(IdentityCreateInputModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
            };

            user.Roles.Add(this.identityService.SetUserRole(user));

            IdentityResult result = await this.userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return this.BadRequest(result.Errors.Select(e => e.Description).ToList());
            }

            await this.emailSender.SendEmailAfterUserRegistration(model.Email, model.Username, model.Password);
            return user;
        }

        /// <summary>Login user.</summary>
        /// <param name="model">Login information.</param>
        /// <returns>Token</returns>
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<ApplicationUser>> Login(LoginInputModel model, [FromServices] IOptions<JwtSettings> settings)
        {
            var jwtToken = await this.userManager.Authenticate(model.Username, model.Password, settings.Value);

            if (jwtToken == null)
            {
                return this.BadRequest("Username or password is incorrect.");
            }

            return new JsonResult(jwtToken);
        }

        /// <summary>Get username.</summary>
        /// <returns>Username.</returns>
        [Authorize]
        [HttpGet("username")]
        public ActionResult<string> GetUsername()
        {
            return this.User.Identity.Name;
        }

        /// <summary>Validate token.</summary>
        /// <returns>Is user authenticated.</returns>
        [AllowAnonymous]
        [HttpGet(nameof(ValidateToken))]
        public IActionResult ValidateToken() => this.Ok(this.User.Identity.IsAuthenticated);
    }
}
