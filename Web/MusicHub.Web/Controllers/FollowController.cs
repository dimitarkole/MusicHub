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
    using MusicHub.Web.ViewModels.FollowModels;

    using static MusicHub.Common.GlobalConstants;

    public class FollowController : ApiController
    {
        private readonly IFollowService followService;

        public FollowController(
            IFollowService followService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.followService = followService;
        }

        /// <summary>Get own Following users.</summary>
        /// <returns>Ok(FollowViewModel).</returns>
        [Authorize]
        [HttpGet(nameof(GetFollowing) + "/{page}")]
        public ActionResult<FollowingsAllViewModel<FollowingViewModel>> GetFollowing(int page)
         => this.Ok(this.followService.AllFollowing<FollowingViewModel>(page, PaginationData.FollowingsPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Get own followers users.</summary>
        /// <returns>Ok(FollowingViewModel).</returns>
        [Authorize]
        [HttpGet(nameof(GetFollowers) + "/{page}")]
        public ActionResult<FollowingsAllViewModel<FollowingViewModel>> GetFollowers(int page)
            => this.Ok(this.followService.AllFollowers<FollowingViewModel>(page, PaginationData.FollowersPerPage, this.userManager.GetUserId(this.User)));

        /// <summary>Get Following users.</summary>
        /// <param name="id">Id - user id.</param>
        /// <returns>Ok(FollowViewModel).</returns>
        [HttpGet(nameof(GetFollowing) + "/{id}/{page}")]
        public ActionResult<FollowingsAllViewModel<FollowingViewModel>> GetFollowing(string id, int page)
            => this.Ok(this.followService.AllFollowing<FollowingViewModel>(page, PaginationData.FollowingsPerPage, id));

        /// <summary>Get followers users.</summary>
        /// <param name="id">Id - user id.</param>
        /// <param name="page"></param>
        /// <returns>Ok(FollowViewModel).</returns>
        [HttpGet(nameof(GetFollowers) + "/{id}/{page}")]
        public ActionResult<FollowingsAllViewModel<FollowerViewModel>> GetFollowers(string id, int page)
            => this.Ok(this.followService.AllFollowers<FollowerViewModel>(page, PaginationData.FollowersPerPage, id));

        /// <summary>Create follow.</summary>
        /// <param name="model">FollowCreateInputModel.
        /// <para>FollowingId - Id of Following user.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(FollowCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            if (userId == model.FollowerId)
            {
                return this.BadRequest();
            }

            if (this.followService.IsFollowing(userId, model.FollowerId))
            {
                return this.BadRequest();
            }

            await this.followService.Create(model, userId);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>Delete</summary>
        /// <param name="id">Id- Id of follow.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.followService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.followService.Delete(id);
            return this.Ok();
        }

        [HttpPost(nameof(FilterFollowers) + "/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<FollowingViewModel>> FilterFollowers(int page, FollowFilterInputModel model)
            => this.Ok(this.followService.FilterFollowers<FollowingViewModel>(page, PaginationData.FollowingsPerPage, model, this.userManager.GetUserId(this.User)));

        [HttpPost(nameof(FilterFollowings) + "/{id}")]
        [Authorize]
        public ActionResult<FollowingsAllViewModel<FollowerViewModel>> FilterFollowings(int page, FollowFilterInputModel model)
            => this.Ok(this.followService.FilterFollowings<FollowerViewModel>(page, PaginationData.FollowingsPerPage, model, this.userManager.GetUserId(this.User)));

        /// <summary>Check is follow user comment.</summary>
        /// <param name="id">Id- folloed user Id.</param>
        /// <returns>Ok.</returns>
        [HttpGet(nameof(IsFollowing) + "/{id}")]
        [Authorize]
        public ActionResult<bool> IsFollowing(string id)
            => this.Ok(this.followService.IsFollowing(this.userManager.GetUserId(this.User), id));

        /// <summary>Get Following data.</summary>
        /// <param name="id">Id- follow Id.</param>
        /// <returns>Ok<FollowViewModel></returns>
        [Authorize]
        [HttpGet(nameof(GetFollowingId) + "/{id}")]
        public ActionResult<string> GetFollowingId(string id)
        {
            if (!this.followService.IsFollowing(this.userManager.GetUserId(this.User), id))
            {
                return null;
            }

            var result = this.followService.GetFollow<FollowerViewModel>(id, this.userManager.GetUserId(this.User)).Id;
            return this.Ok(new { Id = result });
        }
    }
}
