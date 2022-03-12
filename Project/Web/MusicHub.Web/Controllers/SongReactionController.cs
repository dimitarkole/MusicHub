namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.SongModels;
    using MusicHub.Web.ViewModels.SongReactionModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using static MusicHub.Common.GlobalConstants;

    public class SongReactionController : UserController
    {
        private readonly ISongReactionService songReactionService;

        public SongReactionController(
            ISongReactionService songReactionService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.songReactionService = songReactionService;
        }

        [HttpGet(nameof(GetLikedSongs) + "/{page}")]
        [Authorize]
        public ActionResult<LikedSongsAllViewModel<LikedSongsViewModel>> GetLikedSongs(int page)
            => this.Ok(this.songReactionService.All<LikedSongsViewModel>(page, PaginationData.LikedSongsPerPage, this.userManager.GetUserId(this.User)));

        [HttpPost(nameof(FilterLikedSongs) + "/{page}")]
        [Authorize]
        public ActionResult<LikedSongsAllViewModel<LikedSongsViewModel>> FilterLikedSongs(int page, SongFilter filter)
            => this.Ok(this.songReactionService.Filter<LikedSongsViewModel>(page, PaginationData.LikedSongsPerPage, this.userManager.GetUserId(this.User), filter));

        [HttpGet(nameof(GetOwnReaction) + "/{id}")]
        [Authorize]
        public ActionResult<SongReactionViewModel> GetOwnReaction(string id)
          => this.Ok(this.songReactionService.GetOwnReaction(id, this.userManager.GetUserId(this.User)));

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(SongReactionCreateInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.songReactionService.Create(model, userId);
            return this.Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, SongReactionCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.songReactionService.Update(model, id);
            return this.Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.songReactionService.Delete(id);
            return this.Ok();
        }
    }
}
