using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.Infrastucture;
using MusicHub.Web.Infrastucture.Extensions;
using MusicHub.Web.ViewModels.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Controllers
{
    public class CommentReactionController : ApiController
    {
        private readonly ICommentReactionService commentReactionService;

        public CommentReactionController(
            ICommentReactionService commentReactionService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.commentReactionService = commentReactionService;
        }

        /// <summary>
        /// Add reaction to comment in data.
        /// </summary>
        /// <param name="model">Input data.</param>
        /// <para>Reaction - reaction of comment.</para>
        /// <para>CommentId - the Id of comment.</para>
        /// <returns>Status201Created.</returns>
        [HttpPost]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Post(CommentReactionCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentReactionService.Create(model, userId);
            return this.Ok();
        }

        /// <summary>Edit reaction.</summary>
        /// <param name="model">
        /// <para>Reaction - reaction of comment.</para>
        /// </param>
        /// <param name="id">The id of the reaction comment.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpPut("{id}")]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Put(string id, CommentReactionEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            if (!this.commentReactionService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.commentReactionService.Update(model, id);
            return this.Ok();
        }

        /// <summary>Delete reaction.</summary>
        /// <param name="id">The id of the reaction.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.commentReactionService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.commentReactionService.Delete(id);
            return this.Ok();
        }

        /// <summary>Get own reaction of comment.</summary>
        /// <param name="id">The id of the reaction.</param>
        /// <returns>
        /// Ok(CommentReactionViewModel).
        /// <para>Reaction - reaction of comment.</para>
        /// <para>Id - Id of reaction of comment.</para>
        /// </returns>
        [HttpGet("{id}")]
        [Authorize]
        [Permission(PermissionType.User)]
        public ActionResult<CommentReactionViewModel> Get(string id)
            => this.Ok(this.commentReactionService.GetOwnReaction(id, this.userManager.GetUserId(this.User)));
    }
}
