namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data.Migrations;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.SongModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NHibernate.Mapping;
    using static MusicHub.Common.GlobalConstants;

    public class SongController : UserController
    {
        private readonly ISongService songService;

        public SongController(
            ISongService songService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.songService = songService;
        }

        /// <summary>Get own songs.</summary>
        /// <param name="page">The page of songs.</param>
        /// <returns>Ok(SongsAllViewModel<SongViewModel>).</returns>
        [HttpGet(nameof(GetOwn) + "/{page}")]
        public ActionResult<SongsAllViewModel<SongViewModel>> GetOwn(int page)
            => this.Ok(this.songService.AllOwn<SongViewModel>(page, PaginationData.SongsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Filter own songs.</summary>
        /// <param name="page">The page of songs.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of song.</para>
        /// <para>Username - name of user who created song.</para>
        /// <para>UserId - id of user who created song.</para>
        /// <para>CategoryId - id of category of song.</para>
        /// <para>OrderMethod - order method.</para>
        /// <returns>Ok(SongsAllViewModel<SongViewModel>).</returns>
        [HttpPost(nameof(Filter) + "/{page}")]
        public ActionResult<SongsAllViewModel<SongViewModel>> Filter(int page, SongFilter model)
          => this.Ok(this.songService.Filter<SongViewModel>(page, PaginationData.SongsPerPage, model, this.userManager.GetUserId(this.User)));

        /// <summary>Add song in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>Name - song name.</para>
        /// <para>CategoryId - category id.</para>
        /// <para>Text - song text.</para>
        /// <para>ImageFilePath - path to image file name.</para>
        /// <para>AudioFilePath - path to audio file name.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(SongCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            var songId = await this.songService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { songId = songId });
        }

        /// <summary>Edit song data.</summary>
        /// <param name="id">Id - Id of song.</param>
        /// <param name="model">Input data.</param>
        /// <para>Name - song name.</para>
        /// <para>CategoryId - category id.</para>
        /// <para>Text - song text.</para>
        /// <para>ImageFilePath - path to image file name.</para>
        /// <para>AudioFilePath - path to audio file name.</para>
        /// <returns>Ok().</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, SongEditModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (!this.songService.IsOwn(id, userId))
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable);
            }

            await this.songService.Update(id, model);
            return this.Ok();
        }

        /// <summary>Delete song in data.</summary>
        /// <param name="id">Id - Id of song.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.songService.Delete(id);
            return this.Ok();
        }
    }
}
