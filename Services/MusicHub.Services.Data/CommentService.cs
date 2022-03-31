namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.CommentModels;

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public CommentService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.paginationService = paginationService;
            this.context = context;
        }

        public CommentPageModel<T> All<T>(int page, int entitesPerPage, string musicId)
        {
            var comments = this.context.Comments
                .Where(c => c.MusicId == musicId)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>();
            return new CommentPageModel<T>()
            {
                Comments = comments
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetCommentsPagesCount(comments.Count(), entitesPerPage),
            };
        }

        public CommentPageModel<T> GetChildrenComments<T>(int page, int entitesPerPage, string parentCommentId)
        {
            var comments = this.context.Comments
                .Where(c => c.ParentCommentId == parentCommentId)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>();
            return new CommentPageModel<T>()
            {
                Comments = comments
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetCommentsPagesCount(comments.Count(), entitesPerPage),
            };
        }

        public async Task Create(CommentCreatreInputModel model, string userId)
        {
            Comment comment = model.To<Comment>();
            comment.UserId = userId;
            await this.context.Comments.AddAsync(comment);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateChildComment(CommentChildrenCreatreInputModel model, string parentCommentId, string userId)
        {
            Comment parentComment = this.context.Comments.Find(parentCommentId);
            string musicId = parentComment.MusicId;

            Comment comment = model.To<Comment>();
            comment.UserId = userId;
            comment.MusicId = musicId;
            comment.ParentCommentId = parentCommentId;
            await this.context.Comments.AddAsync(comment);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            Comment comment = this.context.Comments.Find(id);
            this.context.Comments.Remove(comment);
            await this.context.SaveChangesAsync();
        }

        public bool IsOwned(string id, string userId)
            => this.context.Comments
                .Any(c => c.Id == id
                && c.UserId == userId);

        public async Task Update(string id, CommentEditInputModel model)
        {
            Comment comment = this.context.Comments.Find(id);
            comment.Text = model.Text;

            this.context.Comments.Update(comment);
            await this.context.SaveChangesAsync();
        }

        private int GetCommentsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
