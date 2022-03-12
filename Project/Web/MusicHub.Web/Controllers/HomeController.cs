namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels;
    using MusicHub.Web.ViewModels.PaginationModels;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using MusicHub.Web.ViewModels.SongModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using static MusicHub.Common.GlobalConstants;

    public class HomeController : ApiController
    {
        private readonly IPlaylistService playlistService;
        private readonly ISongService songService;
        private readonly ISongViewHistoryService songViewHistoryService;

        public HomeController(
            IPlaylistService playlistService,
            ISongService songService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISongViewHistoryService songViewHistoryService,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.playlistService = playlistService;
            this.songService = songService;
            this.songViewHistoryService = songViewHistoryService;
        }

        /// <summary>Get music.</summary>
        /// <param name="page">The page of music.</param>
        /// <returns>Ok(SongsAllViewModel<SongViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{page}")]
        public ActionResult<SongsAllViewModel<SongViewModel>> Get(int page)
          => this.Ok(this.songService.All<SongViewModel>(page, PaginationData.SongsPerPage));

        /// <summary>Filter music.</summary>
        /// <param name="page">The page of music.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of song.</para>
        /// <para>Username - name of user who created music.</para>
        /// <para>UserId - id of user who created music.</para>
        /// <para>CategoryId - id of category of music.</para>
        /// <para>OrderMethod - order method.</para>
        /// </param>
        /// <returns>Ok(SongsAllViewModel<SongViewModel>).</returns>
        [HttpPost]
        [Route(nameof(Filter) + "/{page}")]
        public ActionResult<SongsAllViewModel<SongViewModel>> Filter(int page, SongFilter filter)
            => this.Ok(this.songService.Filter<SongViewModel>(page, PaginationData.SongsPerPage, filter));

        /// <summary>Get music by id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(SongPlayModel).</returns>
        [HttpGet("{id}")]
        public ActionResult<SongPlayModel> Get(string id)
            => this.Ok(this.songService.GetById<SongPlayModel>(id));

        /// <summary>Get sugesst musics by music id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(SongPlayModel).</returns>
        [HttpGet(nameof(SuggestSongs) + "/{id}")]
        public ActionResult<SongViewModel> SuggestSongs(string id)
            => this.Ok(this.songService.SuggestSongs<SongViewModel>(id));

        /// <summary>Get palylists.</summary>
        /// <param name="page">The page of palylists.</param>
        /// <returns>Ok(PlaylistsAllViewModel<PlaylistViewModel>).</returns>
        [HttpGet(nameof(GetPlaylists) + "/{page}")]
        public ActionResult<PlaylistsAllViewModel<PlaylistViewModel>> GetPlaylists(int page)
            => this.Ok(this.playlistService.All<PlaylistViewModel>(page, PaginationData.PlaylistsPerPage));

        /// <summary>Get playlist by id.</summary>
        /// <param name="id">The id of palylists.</param>
        /// <returns>Ok(PlaylistPlayModel).</returns>
        [HttpGet(nameof(GetPlaylit) + "/{id}")]
        public ActionResult<PlaylistPlayModel> GetPlaylit(string id)
           => this.Ok(this.playlistService.GetById<PlaylistPlayModel>(id));

        /// <summary>Filter playlists.</summary>
        /// <param name="page">The page of music.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of playlist.</para>
        /// <para>Username - name of user who created playlist.</para>
        /// <para>UserId - id of user who created playlist.</para>
        /// <para>CategoryId - id of category of playlist.</para>
        /// <para>OrderMethod - order method.</para>
        /// </param>
        /// <returns>Ok(PlaylistsAllViewModel<PlaylistViewModel>).</returns>
        [HttpPost(nameof(PlaylistFilter) + "/{page}")]
        public ActionResult<PlaylistsAllViewModel<PlaylistViewModel>> PlaylistFilter(int page, PlaylistFilterInputModel model)
            => this.Ok(this.playlistService.Filter<PlaylistViewModel>(page, PaginationData.PlaylistsPerPage, model));
    }
}
