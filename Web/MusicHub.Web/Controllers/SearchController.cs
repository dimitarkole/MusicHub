namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using MusicHub.Web.ViewModels.Search;

    using static MusicHub.Common.GlobalConstants;

    public class SearchController : ApiController
    {
        private readonly IBooleanSearchService booleanSearchService;
        private readonly IMusicService musicService;
        private readonly IPlaylistService playlistService;
        private readonly IPaginationService paginationService;

        public SearchController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger,
            IBooleanSearchService booleanSearchService,
            IMusicService musicService,
            IPlaylistService playlistService,
            IPaginationService paginationService)
            : base(userManager, signInManager, logger)
        {
            this.booleanSearchService = booleanSearchService;
            this.musicService = musicService;
            this.playlistService = playlistService;
            this.paginationService = paginationService;
        }

        [HttpPost(nameof(Musics) + "/{page}")]
        public IActionResult Musics(int page, BooleanSearchInputModel model)
        {
            var filter = new MusicFilter()
            {
                Name = model.Query,
            };
            var entitesPerPage = PaginationData.MusicsPerPage;
            return this.Ok(this.musicService.Filter<MusicViewModel>(page, entitesPerPage, filter));
        }

        [HttpPost(nameof(Playlists) + "/{page}")]
        public IActionResult Playlists(int page, BooleanSearchInputModel model)
        {
            var filter = new PlaylistFilterInputModel()
            {
                Name = model.Query,
            };
            var entitesPerPage = PaginationData.PlaylistsPerPage;
            return this.Ok(this.playlistService.Filter<PlaylistViewModel>(page, entitesPerPage, filter));
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
                => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
