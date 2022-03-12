namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.FollowModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class FollowController : UserController
    {
        private readonly IFollowService followService;

        public FollowController(
            IFollowService followService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.followService = followService;
        }

        /// <summary>Get own followed users.</summary>
        /// <returns>Ok(FollowViewModel).</returns>
        [Authorize]
        [HttpGet(nameof(GetFollowing))]
        public ActionResult<IEnumerable<FollowViewModel>> GetFollowing()
         => this.Ok(this.followService.AllFollowed<FollowViewModel>(this.userManager.GetUserId(this.User)));

        /// <summary>Get own followers users.</summary>
        /// <returns>Ok(FollowedViewModel).</returns>
        [HttpGet(nameof(GetFollowers))]
        [Authorize]
        public ActionResult<IEnumerable<FollowedViewModel>> GetFollowers()
            => this.Ok(this.followService.AllFollowers<FollowedViewModel>(this.userManager.GetUserId(this.User)));

        /// <summary>Get followed users.</summary>
        /// <param name="id">Id - user id.</param>
        /// <returns>Ok(FollowViewModel).</returns>
        [HttpGet(nameof(GetFollowing) + "/{id}")]
        public ActionResult<IEnumerable<FollowViewModel>> GetFollowing(string id)
            => this.Ok(this.followService.AllFollowed<FollowViewModel>(id));

        /// <summary>Get followers users.</summary>
        /// <param name="id">Id - user id.</param>
        /// <returns>Ok(FollowViewModel).</returns>
        [HttpGet(nameof(GetFollowers) + "/{id}")]
        public ActionResult<IEnumerable<FollowedViewModel>> GetFollowers(string id)
            => this.Ok(this.followService.AllFollowers<FollowedViewModel>(id));

        /// <summary>Create follow.</summary>
        /// <param name="model">FollowCreateInputModel.
        /// <para>FollowedId - Id of followed user.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpPost]
        public async Task<IActionResult> Post(FollowCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            if (userId == model.FollowedId)
            {
                return this.BadRequest();
            }

            await this.followService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Delete comment.</summary>
        /// <param name="id">Id- Id of follow.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.followService.Delete(id);
            return this.Ok();
        }

        [HttpPost(nameof(Filter))]
        public ActionResult<IEnumerable<FollowViewModel>> Filter(FollowFilterInputModel model)
            => this.Ok(this.followService.Filter<FollowViewModel>(model, this.userManager.GetUserId(this.User)));

        /// <summary>Check is follow user comment.</summary>
        /// <param name="id">Id- folloed user Id.</param>
        /// <returns>Ok.</returns>
        [HttpGet(nameof(IsFollowed) + "/{id}")]
        public ActionResult<bool> IsFollowed(string id)
            => this.Ok(this.followService.IsFollowed(id, this.userManager.GetUserId(this.User)));

        /// <summary>Get followed data.</summary>
        /// <param name="id">Id- follow Id.</param>
        /// <returns>Ok<FollowViewModel></returns>
        [HttpGet(nameof(GetFollowId) + "/{id}")]
        public ActionResult<FollowViewModel> GetFollowId(string id)
            => this.Ok(this.followService.GetFollow<FollowViewModel>(id, this.userManager.GetUserId(this.User)).Id);

        /// <summary>Get follower data.</summary>
        /// <param name="id">Id- follow Id.</param>
        /// <returns>Ok<FollowViewModel></returns>
        [HttpGet(nameof(GetFollowerId) + "/{id}")]
        public ActionResult<FollowViewModel> GetFollowerId(string id)
            => this.Ok(this.followService.GetFollow<FollowViewModel>(this.userManager.GetUserId(this.User), id).Id);
    }
}
