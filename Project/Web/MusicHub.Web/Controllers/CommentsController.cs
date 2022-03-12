namespace MusicHub.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.CommentModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using static MusicHub.Common.GlobalConstants;

    public class CommentsController : ApiController
    {
        private readonly ICommentService commentService;

        public CommentsController(
            ICommentService commentService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.commentService = commentService;
        }

        /// <summary>Get comment.</summary>
        /// <param name="id">The id of song.</param>
        /// <param name="page">The page of comments.</param>
        /// <returns>Ok(CommentsAllViewModel<CommentViewModel>).</returns>
        [HttpGet("{id}/{page}")]
        public ActionResult<CommentsAllViewModel<CommentViewModel>> Get(string id, int page)
           => this.Ok(this.commentService.All<CommentViewModel>(page, PaginationData.SongsPerPage, id));

        /// <summary>Get is own comment.</summary>
        /// <param name="id">The id of comment.</param>
        /// <returns>Ok(bool).</returns>
        [Authorize]
        [HttpGet(nameof(IsOwn) + "/{id}")]
        public ActionResult<IEnumerable<bool>> IsOwn(string id)
          => this.Ok(this.commentService.IsOwn(id, this.userManager.GetUserId(this.User)));

        /// <summary>Create comment.</summary>
        /// <param name="model">CommentCreatreInputModel.
        /// <para>SongId - Id of comment of comment.</para>
        /// <para>Text - text of comment.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [HttpPost(nameof(Post))]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public async Task<IActionResult> Post(CommentCreatreInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
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
        [HttpPut(nameof(CreateChildrenCommentar) + "/{parentCommentId}")]
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        public async Task<IActionResult> CreateChildrenCommentar(CommentChildrenCreatreInputModel model, string parentCommentId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.commentService.CreateChildrenComment(model, parentCommentId, userId);
            return this.Ok();
        }

        /// <summary>Edit comment.</summary>
        /// <param name="id">Id- Id of comment.</param>
        /// <param name="model">CommentEditInputModel - new comment data.
        /// <para>Text - text of children comment.</para>
        /// </param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CommentEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.commentService.Update(id, model);
            return this.Ok();
        }

        /// <summary>Delete comment.</summary>
        /// <param name="id">Id- Id of comment.</param>
        /// <returns>Ok.</returns>
        [Authorize(Roles = GlobalConstants.Roles.UserRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.commentService.Delete(id);
            return this.Ok();
        }
    }
}
