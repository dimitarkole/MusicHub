namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.VerificationCodeModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class VerificationController : ApiController
    {
        private readonly IVerificationService verificationService;

        public VerificationController(
            IVerificationService verificationService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.verificationService = verificationService;
        }

        /// <summary>Get verification code.</summary>
        /// <param name="id">Id of verification code.</param>
        /// <returns>Ok(VerificationCodeViewModel).</returns>
        [HttpGet("{id}")]
        public ActionResult<VerificationCodeViewModel> GetById(string id)
          => this.Ok(this.verificationService.GetById<VerificationCodeViewModel>(id));

        /// <summary>Get active verification code.</summary>
        /// <param name="id">User Id.</param>
        /// <returns>Ok(VerificationCodeViewModel).</returns>
        [HttpGet(nameof(GetActivatedVerificationCode) + "/{id}")]
        public ActionResult<VerificationCodeViewModel> GetActivatedVerificationCode(string id)
          => this.Ok(this.verificationService.GetActivatedVerificationCode<VerificationCodeViewModel>(id));

        /// <summary>Add verification code in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>UserId - id of user.</para>
        /// <para>Code - verification code.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(VerificationCodeCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.verificationService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Is currect verification code.</summary>
        /// <returns>true/false.</returns>
        [HttpPost(nameof(CheckCode))]
        public ActionResult<bool> CheckCode(VerificationCodeCheckInputModel model)
            => this.Ok(this.verificationService.Check(model));
    }
}
