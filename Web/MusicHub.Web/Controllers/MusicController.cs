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
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.MusicModels;

    using static MusicHub.Common.GlobalConstants;

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
        [Permission(PermissionType.MusicCreator)]
        public ActionResult<MusicPageModel<MusicViewModel>> GetOwn(int page)
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
        [HttpGet(nameof(FilterOwn) + "/{page}")]
        [Authorize]
        [Permission(PermissionType.MusicCreator)]
        public ActionResult<MusicPageModel<MusicViewModel>> FilterOwn(int page, [FromQuery] MusicFilter model)
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
        [Permission(PermissionType.MusicCreator)]
        //[DisableRequestSizeLimit]
        public async Task<IActionResult> Post(MusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
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
        [Permission(PermissionType.MusicCreator)]
        public async Task<IActionResult> Put(string id, MusicEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
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
        [Permission(PermissionType.MusicCreator)]
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

        [Authorize]
        [HttpGet(nameof(IsOwned) + "/{id}")]
        public ActionResult<bool> IsOwned(string id)
            => this.Ok(this.musicService.IsOwned(id, this.userManager.GetUserId(this.User)));

        /// <summary>Get music.</summary>
        /// <param name="page">The page of music.</param>
        /// <returns>Ok(MusicsAllViewModel<MusicViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{page}")]
        public ActionResult<MusicPageModel<MusicViewModel>> Get(int page)
          => this.Ok(this.musicService.All<MusicViewModel>(page, PaginationData.MusicsPerPage));

        /// <summary>Filter music.</summary>
        /// <param name="page">The page of music.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of Music.</para>
        /// <para>Username - name of user who created music.</para>
        /// <para>UserId - id of user who created music.</para>
        /// <para>CategoryId - id of category of music.</para>
        /// <para>OrderMethod - order method.</para>
        /// </param>
        /// <returns>Ok(MusicsAllViewModel<MusicViewModel>).</returns>
        [HttpGet(nameof(Filter) + "/{page}")]
        public ActionResult<MusicPageModel<MusicViewModel>> Filter(int page, [FromQuery] MusicFilter filter)
            => this.Ok(this.musicService.Filter<MusicViewModel>(page, PaginationData.MusicsPerPage, filter));

        /// <summary>Get music by id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(MusicPlayModel).</returns>
        [HttpGet("{id}")]
        public ActionResult<MusicDetailModel> Get(string id)
            => this.Ok(this.musicService.GetById<MusicDetailModel>(id));

        /// <summary>Get sugesst musics by music id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(MusicPlayModel).</returns>
        [HttpGet(nameof(SuggestMusics) + "/{id}/{page}")]
        public ActionResult<MusicPageModel<MusicViewModel>> SuggestMusics(int page, string id)
            => this.Ok(this.musicService.SuggestMusics<MusicViewModel>(page, PaginationData.SuggestMusicPerPage, id));
    }
}
