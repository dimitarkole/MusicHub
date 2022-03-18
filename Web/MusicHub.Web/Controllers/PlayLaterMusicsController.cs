namespace MusicHub.Web.Controllers
{
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
    using MusicHub.Web.ViewModels.PlayLaterMusicModels;

    using static MusicHub.Common.GlobalConstants;

    [Authorize]
    public class PlayLaterMusicsController : ApiController
    {
        private readonly IPlayLaterMusicService playLaterMusicService;

        public PlayLaterMusicsController(
            IPlayLaterMusicService playLaterMusicService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.playLaterMusicService = playLaterMusicService;
        }

        /// <summary>Get own play later musics.</summary>
        /// <param name="page">The page of musics.</param>
        /// <returns>Ok(PlayLaterMusicPageModel<PlayLaterMusicViewModel>).</returns>
        [HttpGet("{page}")]
        public ActionResult<PlayLaterMusicPageModel<PlayLaterMusicViewModel>> Get(int page)
            => this.Ok(this.playLaterMusicService.All<PlayLaterMusicViewModel>(page, PaginationData.PlayLaterMusicsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Filter own played musics.</summary>
        /// <param name="page">The page of played musics.</param>
        /// <param name="filter">Filter data.</param>
        /// <returns>Ok(PlayedMusicAllViewModel<PlayedMusicViewModel>).</returns>
        [HttpGet(nameof(Filter) + "/{page}")]
        public ActionResult<PlayLaterMusicPageModel<PlayLaterMusicViewModel>> Filter(int page, PlayLaterMusicFilterInputModels filter)
            => this.Ok(this.playLaterMusicService.Filter<PlayLaterMusicViewModel>(page, PaginationData.PlayLaterMusicsPerPage, filter, this.userManager.GetUserId(this.User)));

        /// <summary>Add played music in data.</summary>
        /// <param name="model">Input data.</param>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(PlayLaterMusicCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            var playLaterMusicId = await this.playLaterMusicService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created, new { playLaterMusicId = playLaterMusicId });
        }

        /// <summary>Delete play later music in data.</summary>
        /// <param name="id">Id - Id of played music.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.playLaterMusicService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.playLaterMusicService.Delete(id);
            return this.Ok();
        }

        /// <summary>Edit play later music data.</summary>
        /// <param name="id">Id - Id of play later music.</param>
        /// <param name="model">Input data.</param>
        /// <para>position - new position.</para>
        /// <returns>Ok().</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, PlayLaterMusicEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            if (!this.playLaterMusicService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.playLaterMusicService.Update(id, model);
            return this.Ok();
        }
    }
}
