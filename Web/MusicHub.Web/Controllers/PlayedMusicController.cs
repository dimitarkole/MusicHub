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
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.PlayedMusicModels;

    using static MusicHub.Common.GlobalConstants;

    [Authorize]
    public class PlayedMusicController : ApiController
    {
        private readonly IPlayedMusicService playedMusicService;

        public PlayedMusicController(
            IPlayedMusicService playedMusicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.playedMusicService = playedMusicService;
        }

        /// <summary>Get own played musics.</summary>
        /// <param name="page">The page of musics.</param>
        /// <returns>Ok(PlayedMusicAllViewModel<PlayedMusicViewModel>).</returns>
        [HttpGet("{page}")]
        public ActionResult<PlayedMusicPageModel<PlayedMusicViewModel>> Get(int page)
            => this.Ok(this.playedMusicService
                .All<PlayedMusicViewModel>(page, PaginationData.PlayedMusicsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Filter own played musics.</summary>
        /// <param name="page">The page of played musics.</param>
        /// <param name="filter">Filter data.</param>
        /// <returns>Ok(PlayedMusicAllViewModel<PlayedMusicViewModel>).</returns>
        [HttpGet(nameof(Filter) + "/{page}")]
        public ActionResult<PlayedMusicPageModel<PlayedMusicViewModel>> Filter(int page, PlayedMusicFilterInputModels filter)
            => this.Ok(this.playedMusicService.Filter<PlayedMusicViewModel>(page, PaginationData.PlayedMusicsPerPage, filter, this.userManager.GetUserId(this.User)));

        /// <summary>Add played music in data.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(PlayedMusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            var playedMusicId = await this.playedMusicService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { playedMusicId = playedMusicId});
        }

        /// <summary>Delete played music in data.</summary>
        /// <param name="id">Id - Id of played music.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.playedMusicService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.playedMusicService.Delete(id);
            return this.Ok();
        }
    }
}
