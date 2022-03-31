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
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.PlaylistModels;

    using static MusicHub.Common.GlobalConstants;

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

        /// <summary>Add Music to playlist in data.</summary>
        /// <param name="model">Input data.</param>
        /// <para>PlaylistId - playlist id.</para>
        /// <para>MusicId - Music id.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost(nameof(Create))]
        [Authorize]
        public async Task<IActionResult> Create(PlaylistMusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            await this.playlistMusicService.Create(model);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Get own playlists.</summary>
        /// <param name="id">Id - playlist id.</param>
        /// <param name="page">The page of playlist Music.</param>
        /// <returns>Ok(PlaylistMusicsAllViewModel<PlaylistMusicViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{id}/{page}")]
        public ActionResult<PlaylistMusicsPageModel<PlaylistMusicViewModel>> Get(string id, int page)
            => this.Ok(this.playlistMusicService.All<PlaylistMusicViewModel>(page, PaginationData.PlaylistsMusicsPerPage, id));

        /// <summary>Delete playlist Music in data.</summary>
        /// <param name="id">Id- Id of playlist Music.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var playlistId = this.playlistMusicService.GetPlaylistIdByPlaylistMusicId(id);
            if (!this.playlistService.IsOwned(playlistId, userId))
            {
                return this.Forbid();
            }

            await this.playlistMusicService.Delete(id);
            return this.Ok();
        }

        /// <summary>Is music added to playlist.</summary>
        /// <param name="playlistId">PlaylistId - playlist id.</param>
        /// <param name="MusicId">MusicId - playlist id.</param>
        /// <returns>Ok(PlaylistMusicsAllViewModel<PlaylistMusicViewModel>).</returns>
        [Authorize]
        [HttpPost(nameof(IsAddedToPlaylist))]
        public ActionResult<bool> IsAddedToPlaylist(IsAddedToPlaylistCheckInputModel model)
            => this.Ok(this.playlistMusicService.IsAddedToPlaylist(model));

        /// <summary>Is music added to playlist.</summary>
        /// <param name="playlistId">PlaylistId - playlist id.</param>
        /// <param name="MusicId">MusicId - playlist id.</param>
        /// <returns>Ok(PlaylistMusicsAllViewModel<PlaylistMusicViewModel>).</returns>
        [Authorize]
        [HttpPost(nameof(GetPlaylistMusicId))]
        public ActionResult<PlaylistMusicViewModel> GetPlaylistMusicId(PlaylistMusicCreateInputModel model)
            => this.Ok(this.playlistMusicService.GetPlaylistMusicId<PlaylistMusicViewModel>(model));
    }
}
