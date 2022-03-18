using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.ViewModels.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using MusicHub.Web.ViewModels.MusicModels;
using FluentAssertions;

namespace MusicHub.Services.Data.Tests
{
    public class CommentServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateComment_WithValidData_ShouldWorkCorrect()
        {
            var commentService = await this.CreateCommentService(new List<Comment>());
            var model = CommentTestsData.CreateModel;
            var userId = CommentTestsData.CreateUserId;

            await commentService.Create(model, userId);

            Assert.True(this.context.Comments.Any(c => c.Text == model.Text
                && c.MusicId == model.MusicId
                && c.ParentComment == null
                && c.UserId == userId));
        }

        [Fact]
        public async Task CreateCommentWithParent_WithValidData_ShouldWorkCorrect()
        {
            var commentService = await this.CreateCommentService(CommentTestsData.GetComments);
            var model = CommentTestsData.CreateChildModel;
            var userId = CommentTestsData.CreateUserId;
            var parentCommentId = CommentTestsData.CreateParentCommentId;

            await commentService.CreateChildrenComment(model, parentCommentId, userId);

            Assert.True(this.context.Comments.Any(c => c.Text == model.Text
                && c.ParentCommentId == parentCommentId
                && c.UserId == userId));
        }

        [Fact]
        public async Task DeleteCommment_WithValidData_ShouldWorkCorrect()
        {
            var commentService = await this.CreateCommentService(CommentTestsData.GetComments);
            var commentId = CommentTestsData.DeleteCommentId;

            await commentService.Delete(commentId);

            Assert.False(this.context.Comments.Any(c => c.Id == commentId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetAllComment_WithValidData_ShouldeReturn(int page)
        {
            var commentService = await this.CreateCommentService(CommentTestsData.GetComments);
            var musicId = CommentTestsData.MusicId;
            var entitesPerPage = PaginationData.CommentsPerPage;

            var comments = this.context.Comments
              .Where(c => c.MusicId == musicId)
              .OrderByDescending(c => c.CreatedOn)
              .To<CommentViewModel>();
            var expected = new CommentPageModel<CommentViewModel>()
            {
                Comments = comments
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetCommentsPagesCount(comments.Count(), entitesPerPage),
            };

            var actual = commentService.All<CommentViewModel>(page, entitesPerPage, musicId);

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task UpdateComment_WithValidData_ShouldWorkCorrect()
        {
            var commentService = await this.CreateCommentService(CommentTestsData.GetComments);
            var model = CommentTestsData.UpdateModel;
            var commentId = CommentTestsData.UpdateCommentId;

            await commentService.Update(commentId, model);

            Assert.True(this.context.Comments.Any(c => c.Id == commentId
                && c.Text == model.Text));
        }

        private int GetCommentsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        [Fact]
        public async Task IsOwnedComment_WithValidData_ShouldeReturnTrue()
        {
            var commentService = await this.CreateCommentService(CommentTestsData.GetComments);
            var commentId = CommentTestsData.IsOwnedCommentTrueCommentId;
            var userId = CommentTestsData.IsOwedCommentTrueUserId;

            Assert.True(commentService.IsOwned(commentId, userId));
        }

        [Fact]
        public async Task IsOwnedComment_WithValidData_ShouldeReturnFalse()
        {
            var commentService = await this.CreateCommentService(CommentTestsData.GetComments);
            var commentId = CommentTestsData.IsOwnedCommentFalseCommentId;
            var userId = CommentTestsData.IsOwnedCommentFalseUserId;
            Assert.True(commentService.IsOwned(commentId, userId));
        }

        private async Task<CommentService> CreateCommentService(List<Comment> comments)
        {
            var users = CommentTestsData.Users;
            var categories = CommentTestsData.Categories;
            var musics = CommentTestsData.Musics;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Musics.AddRangeAsync(musics);
            await this.context.Comments.AddRangeAsync(comments);
            await this.context.CommentReactions.AddRangeAsync(new List<CommentReaction>());
            await this.context.SaveChangesAsync();
            var service = new CommentService(this.context, new PaginationService());

            return service;
        }
    }
}
