namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using static MusicHub.Common.GlobalConstants;

    public class PlaylistController : ApiController
    {
        private readonly IPlaylistService playlistService;

        public PlaylistController(
            IPlaylistService playlistService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.playlistService = playlistService;
        }

        /// <summary>Get own playlists.</summary>
        /// <param name="page">The page of playlists.</param>
        /// <returns>Ok(PlaylistsAllViewModel<PlaylistViewModel>).</returns>
        [HttpGet(nameof(GetOwn) + "/{page}")]
        [Authorize]
        public ActionResult<PlaylistsAllViewModel<PlaylistViewModel>> GetOwn(int page)
           => this.Ok(this.playlistService.AllOwn<PlaylistViewModel>(page, PaginationData.PlaylistsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Is own playlist.</summary>
        /// <returns>true/false.</returns>
        [HttpGet(nameof(IsOwn) + "/{id}")]
        [Authorize]
        public IActionResult IsOwn(string id)
             => this.Ok(this.playlistService.IsOwn(id, this.userManager.GetUserId(this.User)));

        /// <summary>Get own playlists where song is not added.</summary>
        /// <param name="id">Id - song id</param>
        /// <returns>Ok(IEnumerable<PlaylistViewModel>).</returns>
        [HttpGet(nameof(GetOwnForAddingSong) + "/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<PlaylistViewModel>> GetOwnForAddingSong(string id)
           => this.Ok(this.playlistService.GetOwnForAddingSong<PlaylistViewModel>(id, this.userManager.GetUserId(this.User)));

        /// <summary>Add playlist in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>Name - playlist name.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(PlaylistCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.playlistService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Edit playlist.</summary>
        /// <param name="id">Id- Id of complaylistment.</param>
        /// <param name="model">CommentEditInputModel - new playlist data.
        /// <para>Name - playlist name.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, PlaylistEditInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (!this.playlistService.IsOwn(id, userId))
            {
                return this.Forbid();
            }

            await this.playlistService.Update(model, id);
            return this.Ok();
        }

        /// <summary>Delete playlist.</summary>
        /// <param name="id">Id- Id of playlist.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.playlistService.IsOwn(id, userId))
            {
                return this.Forbid();
            }

            await this.playlistService.Delete(id);
            return this.Ok();
        }

        /// <summary>Filter own playlists.</summary>
        /// <param name="page">The page of playlists.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of playlist.</para>
        /// <para>Username - name of user who created playlist.</para>
        /// <para>UserId - id of user who created playlist.</para>
        /// <para>CategoryId - id of category of playlist.</para>
        /// <para>OrderMethod - order method.</para>
        /// <returns>Ok(PlaylistsAllViewModel<PlaylistViewModel>).</returns>
        [HttpPost(nameof(FilterOwn) + "/{page}")]
        [Authorize]
        public ActionResult<PlaylistsAllViewModel<PlaylistViewModel>> FilterOwn(int page, PlaylistFilterInputModel filter)
            => this.Ok(this.playlistService.FilterOwn<PlaylistViewModel>(page, PaginationData.PlaylistsPerPage, filter, this.userManager.GetUserId(this.User)));
    }
}
