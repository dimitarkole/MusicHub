using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.ViewModels.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Controllers
{
    public class PlaylistMusicController : ApiController
    {
        private readonly IPlaylistService playlistService;
        private readonly IPlaylistMusicService playlistMusicService;

        public PlaylistMusicController(
            IPlaylistService playlistService,
            IPlaylistMusicService playlistMusicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.playlistService = playlistService;
            this.playlistMusicService = playlistMusicService;
        }

        /// <summary>Add song to playlist in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>PlaylistId - playlist id.</para>
        /// <para>SongId - song id.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost(nameof(Create))]
        [Authorize]
        public async Task<IActionResult> Create(PlaylistMusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.playlistMusicService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Get own playlists.</summary>
        /// <param name="id">Id - playlist id.</param>
        /// <param name="page">The page of playlist song.</param>
        /// <returns>Ok(PlaylistSongsAllViewModel<PlaylistSongViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{id}/{page}")]
        public ActionResult<PlaylistMusicsAllViewModel<PlaylistMusicViewModel>> Get(string id, int page)
            => this.Ok(this.playlistMusicService.All<PlaylistMusicViewModel>(page, PaginationData.PlaylistsMusicsPerPage, id));

        /// <summary>Delete playlist song in data.</summary>
        /// <param name="id">Id- Id of playlist song.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (this.playlistService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.playlistMusicService.Delete(id);
            return this.Ok();
        }
    }
}
