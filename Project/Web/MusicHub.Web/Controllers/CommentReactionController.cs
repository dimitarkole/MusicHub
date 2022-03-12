namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.CommentModels;
    using MusicHub.Web.ViewModels.SongReactionModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class CommentReactionController : UserController
    {
        private readonly ICommentReactionService commentReactionService;

        public CommentReactionController(
            ICommentReactionService commentReactionService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
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
        public async Task<IActionResult> Post(CommentReactionCreateModel model)
        {
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
        public async Task<IActionResult> Put(string id, CommentReactionCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.commentReactionService.Update(model, id);
            return this.Ok();
        }

        /// <summary>Delete reaction.</summary>
        /// <param name="id">The id of the reaction.</param>
        /// <returns>Ok.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
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
        public ActionResult<CommentReactionViewModel> Get(string id)
            => this.Ok(this.commentReactionService.GetOwnReaction(id, this.userManager.GetUserId(this.User)));
    }
}
