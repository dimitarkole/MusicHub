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
using MusicHub.Web.ViewModels.MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Controllers
{
    public class MusicController : ApiController
    {
        private readonly IMusicService musicService;

        public MusicController(
            IMusicService musicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.musicService = musicService;
        }

        /// <summary>Get own Musics.</summary>
        /// <param name="page">The page of Musics.</param>
        /// <returns>Ok(MusicsAllViewModel<MusicViewModel>).</returns>
        [HttpGet(nameof(GetOwn) + "/{page}")]
        public ActionResult<MusicsAllViewModel<MusicViewModel>> GetOwn(int page)
            => this.Ok(this.musicService.AllOwn<MusicViewModel>(page, PaginationData.MusicsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Filter own Musics.</summary>
        /// <param name="page">The page of Musics.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of Music.</para>
        /// <para>Username - name of user who created Music.</para>
        /// <para>UserId - id of user who created Music.</para>
        /// <para>CategoryId - id of category of Music.</para>
        /// <para>OrderMethod - order method.</para>
        /// <returns>Ok(MusicsAllViewModel<MusicViewModel>).</returns>
        [HttpPost(nameof(Filter) + "/{page}")]
        public ActionResult<MusicsAllViewModel<MusicViewModel>> Filter(int page, MusicFilter model)
          => this.Ok(this.musicService.Filter<MusicViewModel>(page, PaginationData.MusicsPerPage, model, this.userManager.GetUserId(this.User)));

        /// <summary>Add Music in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>Name - Music name.</para>
        /// <para>CategoryId - category id.</para>
        /// <para>Text - Music text.</para>
        /// <para>ImageFilePath - path to image file name.</para>
        /// <para>AudioFilePath - path to audio file name.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        [Permission(PermissionType.User)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(MusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            var musicId = await this.musicService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { musicId = musicId });
        }

        /// <summary>Edit Music data.</summary>
        /// <param name="id">Id - Id of Music.</param>
        /// <param name="model">Input data.</param>
        /// <para>Name - Music name.</para>
        /// <para>CategoryId - category id.</para>
        /// <para>Text - Music text.</para>
        /// <para>ImageFilePath - path to image file name.</para>
        /// <para>AudioFilePath - path to audio file name.</para>
        /// <returns>Ok().</returns>
        [Authorize]
        [HttpPut("{id}")]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Put(string id, MusicEditModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (!this.musicService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.musicService.Update(id, model);
            return this.Ok();
        }

        /// <summary>Delete Music in data.</summary>
        /// <param name="id">Id - Id of Music.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [Permission(PermissionType.User)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.musicService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.musicService.Delete(id);
            return this.Ok();
        }
    }
}
