namespace MusicHub.Web.Controllers
{
    using System.Diagnostics;

    using MusicHub.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using MusicHub.Data.Models;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.ViewModels.MusicModels;
    using static MusicHub.Common.GlobalConstants;

    public class HomeController : ApiController
    {
        private readonly IMusicService musicService;

        public HomeController(
            IMusicService musicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.musicService = musicService;
        }

        /// <summary>Get music.</summary>
        /// <param name="page">The page of music.</param>
        /// <returns>Ok(SongsAllViewModel<SongViewModel>).</returns>
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
        /// <returns>Ok(SongsAllViewModel<SongViewModel>).</returns>
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
        [HttpGet(nameof(SuggestSongs) + "/{id}")]
        public ActionResult<MusicViewModel> SuggestSongs(string id)
            => this.Ok(this.musicService.SuggestMusics<MusicViewModel>(id));
    }
}
