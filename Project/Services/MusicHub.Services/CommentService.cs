namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.CommentModels;
    using MusicHub.Common.Extensions;

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

        public CommentsAllViewModel<T> All<T>(int page, int entitesPerPage, string songId)
        {
            var comments = this.context.Comments
                .Where(c => c.SongId == songId)
                .To<T>();
            return new CommentsAllViewModel<T>()
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
            var songId = model.SongId;
            Comment comment = model.To<Comment>();
            Song song = this.context.Songs.Find(songId);
            ApplicationUser user = this.context.Users.Find(userId);

            comment.UserId = userId;
            comment.SongId = songId;
            comment.User = user;
            comment.Song = song;
            await this.context.Comments.AddAsync(comment);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateChildrenComment(CommentChildrenCreatreInputModel model, string parentCommentId, string userId)
        {
            Comment parentComment = this.context.Comments.Find(parentCommentId);
            string songId = parentComment.SongId;

            Comment comment = model.To<Comment>();
            comment.UserId = userId;
            comment.SongId = songId;
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

        public bool IsOwn(string id, string userId)
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
