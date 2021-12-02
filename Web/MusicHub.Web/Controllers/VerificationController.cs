using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.ViewModels.VerificationCodeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Controllers
{
    public class VerificationController : ApiController
    {
        private readonly IVerificationCodeService verificationCodeService;
        private readonly SignInManager<User> signInManager;

        public VerificationController(
            IVerificationCodeService verificationCodeService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.verificationCodeService = verificationCodeService;
            this.signInManager = signInManager;
        }

        /// <summary>Get verification code.</summary>
        /// <param name="id">Id of verification code.</param>
        /// <returns>Ok(VerificationCodeViewModel).</returns>
        [HttpGet("{id}")]
        public ActionResult<VerificationCodeViewModel> GetById(string id)
          => this.Ok(this.verificationCodeService.GetById<VerificationCodeViewModel>(id));

        /// <summary>Get active verification code.</summary>
        /// <param name="id">User Id.</param>
        /// <returns>Ok(VerificationCodeViewModel).</returns>
        [HttpGet(nameof(GetActivatedVerificationCode) + "/{id}")]
        public ActionResult<VerificationCodeViewModel> GetActivatedVerificationCode(string id, VerificationCodeCheckInputModel model)
          => this.Ok(this.verificationCodeService.GetActivatedVerificationCode<VerificationCodeViewModel>(id, model.Type));

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

            await this.verificationCodeService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Is currect verification code.</summary>
        /// <returns>true/false.</returns>
        [HttpPost(nameof(CheckCode) + "/{id}")]
        public ActionResult<bool> CheckCode(string id, VerificationCodeCheckInputModel model)
            => this.Ok(this.verificationCodeService.IsVerificationCodeCorrect(id, model));
    }
}
