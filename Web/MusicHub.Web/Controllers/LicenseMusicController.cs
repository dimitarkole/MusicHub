namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.Infrastucture;
    using MusicHub.Web.ViewModels.LicenseMusicModels;

    public class LicenseMusicController : ApiController
    {
        private readonly ILicenseMusicService licenseMusicService;

        public LicenseMusicController(
            ILicenseMusicService licenseMusicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.licenseMusicService = licenseMusicService;
        }

        /// <summary>Get all licenes of music.</summary>
        /// <returns>Ok(IEnumerable.<LicenseMusicViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{id}")]
        public ActionResult<IEnumerable<LicenseMusicViewModel>> Get(string id)
            => this.Ok(this.licenseMusicService.All<LicenseMusicViewModel>(id));

        /// <summary>Add license  to music in data.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost(nameof(Post))]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Post(LicenseMusicCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.licenseMusicService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Delete music license in data.</summary>
        /// <param name="id">Id - Id of music license.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.licenseMusicService.Delete(id);
            return this.Ok();
        }

        /// <summary>Delete All Music Licenses</summary>
        /// <param name="musicId">Id - Id of music license.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("DeleteAllMusicLicenses/{musicId}")]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> DeleteAllMusicLicenses(string musicId)
        {
            await this.licenseMusicService.DeleteAllMusicLicenses(musicId);
            return this.Ok();
        }
    }
}
