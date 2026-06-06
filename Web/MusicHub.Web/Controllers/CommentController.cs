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
    using MusicHub.Web.Infrastucture.Extensions;
    using MusicHub.Web.ViewModels.CommentModels;

    using static MusicHub.Common.GlobalConstants;

    public class CommentController : ApiController
    {
        private readonly ICommentService commentService;

        public CommentController(
            ICommentService commentService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.commentService = commentService;
        }

        /// <summary>Get comment.</summary>
        /// <param name="id">The id of music.</param>
        /// <param name="page">The page of comments.</param>
        /// <returns>Ok(CommentsAllViewModel<CommentViewModel>).</returns>
        [HttpGet("{id}/{page}")]
        public ActionResult<CommentPageModel<CommentViewModel>> Get(string id, int page)
           => this.Ok(this.commentService.All<CommentViewModel>(page, PaginationData.CommentsPerPage, id));

        /// <summary>Get comment.</summary>
        /// <param name="id">The id of comment.</param>
        /// <param name="page">The page of comments.</param>
        /// <returns>Ok(CommentsAllViewModel<CommentViewModel>).</returns>
        [HttpGet(nameof(GetChildrenComments) + "/{id}/{page}")]
        public ActionResult<CommentPageModel<CommentViewModel>> GetChildrenComments(string id, int page)
           => this.Ok(this.commentService.GetChildrenComments<CommentViewModel>(page, PaginationData.CommentsPerPage, id));

        /// <summary>Get is own comment.</summary>
        /// <param name="id">The id of comment.</param>
        /// <returns>Ok(bool).</returns>
        [Authorize]
        [HttpGet(nameof(IsOwned) + "/{id}")]
        public ActionResult<IEnumerable<bool>> IsOwned(string id)
          => this.Ok(this.commentService.IsOwned(id, this.userManager.GetUserId(this.User)));

        /// <summary>Create comment.</summary>
        /// <param name="model">CommentCreatreInputModel.
        /// <para>ComentId - Id of comment of comment.</para>
        /// <para>Text - text of comment.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [HttpPost(nameof(Post))]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Post(CommentCreatreInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.commentService.Create(model, userId);
            return this.Ok();
        }

        /// <summary>Create children comment of comment.</summary>
        /// <param name="parentCommentId">parentCommentId- Id of parent comment.</param>
        /// <param name="model">CommentChildrenCreatreInputModel - children comment data.
        /// <para>Text - text of children comment.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [HttpPost(nameof(CreateChildComment) + "/{parentCommentId}")]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> CreateChildComment(CommentChildrenCreatreInputModel model, string parentCommentId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.commentService.CreateChildComment(model, parentCommentId, userId);
            return this.Ok();
        }

        /// <summary>Edit comment.</summary>
        /// <param name="id">Id- Id of comment.</param>
        /// <param name="model">CommentEditInputModel - new comment data.
        /// <para>Text - text of children comment.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [HttpPut("{id}")]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Put(string id, CommentEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Errors());
            }

            var userId = this.userManager.GetUserId(this.User);
            if (!this.commentService.IsOwned(id, userId))
            {
                return this.Forbid();
            }

            await this.commentService.Update(id, model);
            return this.Ok();
        }

        /// <summary>Delete comment.</summary>
        /// <param name="id">Id- Id of comment.</param>
        /// <returns>Ok.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [Permission(PermissionType.User)]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.commentService.IsOwned(id, userId))
            {
                return this.BadRequest("This comment is not owned!");
            }

            await this.commentService.Delete(id);
            return this.Ok();
        }
    }
}
