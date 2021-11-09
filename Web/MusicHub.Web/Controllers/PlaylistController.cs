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
    using MusicHub.Web.ViewModels.PlaylistModels;

    using static MusicHub.Common.GlobalConstants;

    public class PlaylistController : ApiController
    {
        private readonly IPlaylistService playlistService;

        public PlaylistController(
            IPlaylistService playlistService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
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
       // [Permission(PermissionType.PlaylistCreator)]
        public ActionResult<PlaylistsAllViewModel<PlaylistViewModel>> GetOwn(int page)
           => this.Ok(this.playlistService.AllOwned<PlaylistViewModel>(page, PaginationData.PlaylistsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Is own playlist.</summary>
        /// <returns>true/false.</returns>
        [HttpGet(nameof(IsOwned) + "/{id}")]
        [Authorize]
        [Permission(PermissionType.PlaylistCreator)]
        public IActionResult IsOwned(string id)
             => this.Ok(this.playlistService.IsOwned(id, this.userManager.GetUserId(this.User)));

        /// <summary>Get own playlists where Music is not added.</summary>
        /// <param name="id">Id - Music id</param>
        /// <returns>Ok(IEnumerable<PlaylistViewModel>).</returns>
        [HttpGet(nameof(GetOwnForAddingMusic) + "/{id}")]
        [Authorize]
        [Permission(PermissionType.PlaylistCreator)]
        public ActionResult<IEnumerable<PlaylistViewModel>> GetOwnForAddingMusic(string id)
           => this.Ok(this.playlistService.GetOwnedForAddingMusic<PlaylistViewModel>(id, this.userManager.GetUserId(this.User)));

        /// <summary>Add playlist in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>Name - playlist name.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        [Permission(PermissionType.PlaylistCreator)]
        public async Task<IActionResult> Post(PlaylistCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            var playlistId = await this.playlistService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { playlistId = playlistId});
        }

        /// <summary>Edit playlist.</summary>
        /// <param name="id">Id- Id of complaylistment.</param>
        /// <param name="model">CommentEditInputModel - new playlist data.
        /// <para>Name - playlist name.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [HttpPut("{id}")]
        [Authorize]
        [Permission(PermissionType.PlaylistCreator)]
        public async Task<IActionResult> Put(string id, PlaylistEditInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            if (!this.playlistService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.playlistService.Update(model, id);
            return this.Ok();
        }

        /// <summary>Delete playlist.</summary>
        /// <param name="id">Id- Id of playlist.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [Permission(PermissionType.PlaylistCreator)]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.playlistService.IsOwned(id, userId))
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
        [Permission(PermissionType.PlaylistCreator)]
        public ActionResult<PlaylistsAllViewModel<PlaylistViewModel>> FilterOwn(int page, PlaylistFilterInputModel filter)
            => this.Ok(this.playlistService.Filter<PlaylistViewModel>(page, PaginationData.PlaylistsPerPage, filter, this.userManager.GetUserId(this.User)));
    }
}
