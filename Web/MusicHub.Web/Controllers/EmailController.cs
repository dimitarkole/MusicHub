namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Messaging;
    using MusicHub.Web.ViewModels.VerificationCodeModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using static MusicHub.Common.GlobalConstants;
    using MusicHub.Web.ViewModels.MailModels;
    using MusicHub.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using MusicHub.Services.Data.Interfaces;
    using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
    using MusicHub.Common;

    public class EmailController : ApiController
    {
        private readonly IEmailSender emailSender;
        private readonly IProfileService profileService;
        private readonly IVerificationCodeService verificationCodeService;

        public EmailController(
            IEmailSender emailSender,
            IProfileService profileService,
            IVerificationCodeService verificationCodeService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.emailSender = emailSender;
            this.profileService = profileService;
            this.verificationCodeService = verificationCodeService;
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
            var user = this.profileService.GetById<ProfileViewModel>(id);
            var verificationCreateModel = new VerificationCodeCreateInputModel()
            {
                UserId = id,
                Type = VerificationType.ChangePassword,
            };

            await this.verificationCodeService.Create(verificationCreateModel);
            var data = new Dictionary<string, string>();
            data["userName"] = user.UserName;
            data["verificationCode"] = verificationCreateModel.Code;
            var messageBody = MessagesContentTemplates.CreateMessageContent(EmailSubjects.ChangePassword, data);
            await this.emailSender.SendEmailAsync(user.Email, EmailSubjects.ChangePassword, messageBody);
            return this.Ok();
        }

        /// <summary>Send mail with code for chanigng passord.</summary>
        /// <param name="id">User id.</param>
        /// <returns>Ok().</returns>
        [HttpGet(nameof(SendSuccessfulChangedPasswordEmail) + "/{id}")]
        public async Task<IActionResult> SendSuccessfulChangedPasswordEmail(string id)
        {
            var user = this.profileService.GetById<ProfileViewModel>(id);
            var data = new Dictionary<string, string>();
            data["userName"] = user.UserName;
            var messageBody = MessagesContentTemplates.CreateMessageContent(EmailSubjects.SuccessfulChangedPassword, data);
            await this.emailSender.SendEmailAsync(user.Email, EmailSubjects.ChangePassword, messageBody);
            return this.Ok();
        }
    }
}
