namespace MusicHub.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.ViewModels;
    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.PlaylistModels;

    using static MusicHub.Common.GlobalConstants;

    public class HomeController : ApiController
    {
        private readonly IMusicService musicService;
        private readonly IPlaylistService playlistService;

        public HomeController(
            IPlaylistService playlistService,
            IMusicService musicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.musicService = musicService;
            this.playlistService = playlistService;
        }

        /// <summary>Get music.</summary>
        /// <param name="page">The page of music.</param>
        /// <returns>Ok(MusicsAllViewModel<SongViewModel>).</returns>
        [HttpGet(nameof(Get) + "/{page}")]
        public ActionResult<MusicsAllViewModel<MusicViewModel>> Get(int page)
          => this.Ok(this.musicService.All<MusicViewModel>(page, PaginationData.MusicsPerPage));

        /// <summary>Filter music.</summary>
        /// <param name="page">The page of music.</param>
        /// <param name="filter">Filter data.
        /// <para>Name - name of song.</para>
        /// <para>Username - name of user who created music.</para>
        /// <para>UserId - id of user who created music.</para>
        /// <para>CategoryId - id of category of music.</para>
        /// <para>OrderMethod - order method.</para>
        /// </param>
        /// <returns>Ok(MusicsAllViewModel<SongViewModel>).</returns>
        [HttpPost]
        [Route(nameof(Filter) + "/{page}")]
        public ActionResult<MusicsAllViewModel<MusicViewModel>> Filter(int page, MusicFilter filter)
            => this.Ok(this.musicService.Filter<MusicViewModel>(page, PaginationData.MusicsPerPage, filter));

        /// <summary>Get music by id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(SongPlayModel).</returns>
        [HttpGet("{id}")]
        public ActionResult<MusicDetailModel> Get(string id)
            => this.Ok(this.musicService.GetById<MusicDetailModel>(id));

        /// <summary>Get sugesst musics by music id.</summary>
        /// <param name="id">Id of music.</param>
        /// <returns>Ok(SongPlayModel).</returns>
        [HttpGet(nameof(SuggestMusics) + "/{id}")]
        public ActionResult<MusicViewModel> SuggestMusics(string id)
            => this.Ok(this.musicService.SuggestMusics<MusicViewModel>(id));


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
