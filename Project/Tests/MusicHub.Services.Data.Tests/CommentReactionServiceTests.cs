namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.CommentModels;
    using Xunit;

    public class CommentReactionServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateCommentReaction_WithValidData_ShouldWorkCorrect()
        {
            var commentReactionService = await this.CreateCommentReactionService(new List<CommentReaction>());
            var model = CommentReactionTestsData.CreateModel;
            var userId = CommentReactionTestsData.CreateUserId;

            await commentReactionService.Create(model, userId);

            Assert.True(this.context.CommentReactions.Any(cr => cr.CommentId == model.CommentId
                && cr.UserId == userId
                && cr.Reaction == cr.Reaction));
        }

        [Fact]
        public async Task DeleteCommentReaction_WithValidData_ShouldWorkCorrect()
        {
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReactionService = await this.CreateCommentReactionService(commentReactions);
            var commentReactionId = commentReactions[0].Id;

            await commentReactionService.Delete(commentReactionId);

            Assert.False(this.context.CommentReactions.Any(c => c.Id == commentReactionId));
        }

        [Fact]
        public async Task UpdateCommentReaction_WithValidData_ShouldeReturnFalse()
        {
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReactionService = await this.CreateCommentReactionService(commentReactions);
            var model = CommentReactionTestsData.UpdateModel;
            var commentReactionsId = commentReactions[0].Id;

            await commentReactionService.Update(model, commentReactionsId);
            Assert.True(this.context.CommentReactions.Any(cr => cr.Reaction == cr.Reaction));
        }

        [Fact]
        public async Task GetOwnReaction__WithValidData_ShouldWorkCorrect()
        {
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReactionService = await this.CreateCommentReactionService(commentReactions);
            var userId = commentReactions[0].UserId;
            var commentId = commentReactions[0].CommentId;
            var expectedRusult = this.context.CommentReactions
              .Where(r => r.UserId == userId
                  && r.CommentId == commentId)
              .To<CommentReactionViewModel>()
              .FirstOrDefault();
            var result = commentReactionService.GetOwnReaction(commentId, userId);

            Assert.True(expectedRusult.Id == result.Id
                    && expectedRusult.Reaction == result.Reaction);
        }

        private async Task<CommentReactionService> CreateCommentReactionService(List<CommentReaction> musicReactions)
        {
            var users = CommentReactionTestsData.Users;
            var categories = CommentReactionTestsData.Categories;
            var musics = CommentReactionTestsData.Musics;
            var comments = CommentReactionTestsData.Comments;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Songs.AddRangeAsync(musics);
            await this.context.Comments.AddRangeAsync(comments);
            await this.context.CommentReactions.AddRangeAsync(musicReactions);
            await this.context.SaveChangesAsync();
            var service = new CommentReactionService(this.context);

            return service;
        }
    }
}
