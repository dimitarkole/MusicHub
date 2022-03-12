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

    public class PlaylistSongController : ApiController
    {
        private readonly IPlaylistService playlistService;
        private readonly IPlaylistSongService playlistSongService;

        public PlaylistSongController(
            IPlaylistService playlistService,
            IPlaylistSongService playlistSongService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.playlistService = playlistService;
            this.playlistSongService = playlistSongService;
        }

        /// <summary>Add song to playlist in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>PlaylistId - playlist id.</para>
        /// <para>SongId - song id.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost(nameof(Create))]
        [Authorize]
        public async Task<IActionResult> Create(SongToPlaylistCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.playlistSongService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Delete playlist song in data.</summary>
        /// <param name="id">Id- Id of playlist song.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (this.playlistService.IsOwn(id, userId))
            {
                return this.Forbid();
            }

            await this.playlistSongService.Delete(id);
            return this.Ok();
        }

        /// <summary>Get own playlists.</summary>
        /// <param name="id">Id - playlist id.</param>
        /// <param name="page">The page of playlist song.</param>
        /// <returns>Ok(PlaylistSongsAllViewModel<PlaylistSongViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{id}/{page}")]
        public ActionResult<PlaylistSongsAllViewModel<PlaylistSongViewModel>> Get(string id, int page)
            => this.Ok(this.playlistSongService.All<PlaylistSongViewModel>(page, PaginationData.PlaylistsSongsPerPage, id));
    }
}
