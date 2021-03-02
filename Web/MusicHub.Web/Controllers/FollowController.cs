using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.ViewModels.FollowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Controllers
{
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

        /// <summary>Get own followed users.</summary>
        /// <returns>Ok(FollowViewModel).</returns>
        [Authorize]
        [HttpGet(nameof(GetFollowed))]
        public ActionResult<IEnumerable<FollowedViewModel>> GetFollowed()
         => this.Ok(this.followService.AllFollowed<FollowedViewModel>(this.userManager.GetUserId(this.User)));

        /// <summary>Get own followers users.</summary>
        /// <returns>Ok(FollowedViewModel).</returns>
        [Authorize]
        [HttpGet(nameof(GetFollowers))]
        public ActionResult<IEnumerable<FollowedViewModel>> GetFollowers()
            => this.Ok(this.followService.AllFollowers<FollowedViewModel>(this.userManager.GetUserId(this.User)));

        /// <summary>Get followed users.</summary>
        /// <param name="id">Id - user id.</param>
        /// <returns>Ok(FollowViewModel).</returns>
        [HttpGet(nameof(GetFollowed) + "/{id}")]
        public ActionResult<IEnumerable<FollowerViewModel>> GetFollowed(string id)
            => this.Ok(this.followService.AllFollowed<FollowerViewModel>(id));

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
        [Authorize]
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.followService.Delete(id);
            return this.Ok();
        }

        [HttpPost(nameof(Filter))]
        [Authorize]
        public ActionResult<IEnumerable<FollowerViewModel>> Filter(FollowFilterInputModel model)
            => this.Ok(this.followService.Filter<FollowerViewModel>(model, this.userManager.GetUserId(this.User)));

        /// <summary>Check is follow user comment.</summary>
        /// <param name="id">Id- folloed user Id.</param>
        /// <returns>Ok.</returns>
        [HttpGet(nameof(IsFollowed) + "/{id}")]
        [Authorize]
        public ActionResult<bool> IsFollowed(string id)
            => this.Ok(this.followService.IsFollowed(id, this.userManager.GetUserId(this.User)));

        /// <summary>Get followed data.</summary>
        /// <param name="id">Id- follow Id.</param>
        /// <returns>Ok<FollowViewModel></returns>
        [Authorize]
        [HttpGet(nameof(GetFollowedId) + "/{id}")]
        public ActionResult<FollowedViewModel> GetFollowedId(string id)
            => this.Ok(this.followService.GetFollow<FollowedViewModel>(id, this.userManager.GetUserId(this.User)).Id);

        /// <summary>Get follower data.</summary>
        /// <param name="id">Id- follow Id.</param>
        /// <returns>Ok<FollowViewModel></returns>
        [Authorize]
        [HttpGet(nameof(GetFollowerId) + "/{id}")]
        public ActionResult<FollowerViewModel> GetFollowerId(string id)
            => this.Ok(this.followService.GetFollow<FollowerViewModel>(this.userManager.GetUserId(this.User), id).Id);
    }
}
