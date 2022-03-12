namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Messaging;
    using MusicHub.Web.ViewModels.MailModels;
    using MusicHub.Web.ViewModels.ProfilleModels;
    using MusicHub.Web.ViewModels.VerificationCodeModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using static MusicHub.Common.GlobalConstants;

    public class EmailController : ApiController
    {
        private readonly IEmailSender emailSender;
        private readonly IIdentityService identityService;
        private readonly IProfileService profileService;
        private readonly IVerificationService verificationService;

        public EmailController(
            IEmailSender emailSender,
            IIdentityService identityService,
            IProfileService profileService,
            IVerificationService verificationService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.emailSender = emailSender;
            this.identityService = identityService;
            this.profileService = profileService;
            this.verificationService = verificationService;
        }

        /// <summary>Send Email.</summary>
        /// <param name="model">Email data.
        /// <para>ToMail - the email address to which the message will be sen.</para>
        /// <para>Subject - subject of email.</para>
        /// <para>MessageBody - massage of email.</para>
        /// </param>
        /// <returns>Ok().</returns>
        [HttpPost(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail(EmailInputModel model)
        {
            await this.emailSender.SendEmailAsync(model);
            return this.Ok();
        }

        /// <summary>Send mail with code for chanigng passord.</summary>
        /// <param name="id">User id.</param>
        /// <returns>Ok().</returns>
        [HttpGet(nameof(SendEmailWithCodeForChangingPassword) + "/{id}")]
        public async Task<IActionResult> SendEmailWithCodeForChangingPassword(string id)
        {
            var userFilter = new UserFilter() { Id = id };
            var user = this.profileService.Search(userFilter).FirstOrDefault();
            var verificationCodeInfo = this.verificationService.GetActivatedVerificationCode<VerificationCodeViewModel>(id);
            var messageBody = Messeges.ChangePassword(user.UserName, verificationCodeInfo.Code);
            var model = new EmailInputModel() { ToMail = user.Email, Subject = "Verification code", MessageBody = messageBody };
            await this.emailSender.SendEmailAsync(model);
            return this.Ok();
        }
    }
}
