namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Web.Infrastucture;
    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.MusicReactionModels;
    using MusicHub.Web.ViewModels.NotificationModels;
    using static MusicHub.Common.GlobalConstants;

    [Authorize]
    [Permission(PermissionType.User)]
    public class MusicReactionController : ApiController
    {
        private readonly IMusicReactionService musicReactionService;
        private readonly INotificationsService notificationsService;

        public MusicReactionController(
            IMusicReactionService musicReactionService,
            INotificationsService notificationsService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.musicReactionService = musicReactionService;
            this.notificationsService = notificationsService;
        }

        [HttpGet(nameof(GetLikedMusics) + "/{page}")]
        public ActionResult<LikedMusicsAllViewModel<LikedMusicViewModel>> GetLikedMusics(int page)
            => this.Ok(this.musicReactionService.All<LikedMusicViewModel>(page, PaginationData.LikedMusicsPerPage, this.userManager.GetUserId(this.User)));

        [HttpPost(nameof(FilterLikedMusics) + "/{page}")]
        public ActionResult<LikedMusicsAllViewModel<LikedMusicViewModel>> FilterLikedMusics(int page, MusicFilter filter)
            => this.Ok(this.musicReactionService.Filter<LikedMusicViewModel>(page, PaginationData.LikedMusicsPerPage, this.userManager.GetUserId(this.User), filter));

        [HttpGet(nameof(GetOwnReaction) + "/{id}")]
        public ActionResult<MusicReactionViewModel> GetOwnReaction(string id)
          => this.Ok(this.musicReactionService.GetOwnReaction(id, this.userManager.GetUserId(this.User)));

        [HttpPost]
        public async Task<IActionResult> Post(MusicReactionCreateInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.musicReactionService.Create(model, userId);
            return this.Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, MusicReactionCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (!this.musicReactionService.IsOwned(id, this.userManager.GetUserId(this.User)))
            {
                return this.Forbid();
            }

            await this.musicReactionService.Update(model, id);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.musicReactionService.IsOwned(id, this.userManager.GetUserId(this.User)))
            {
                return this.Forbid();
            }

            await this.musicReactionService.Delete(id);
            return this.Ok();
        }
    }
}
