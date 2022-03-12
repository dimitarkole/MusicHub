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
                && c.SongId == model.SongId
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
            var expextedResult = this.context.Comments
                .Where(c => c.SongId == musicId)
                .To<CommentViewModel>();
            var entitesPerPage = PaginationData.CommentsPerPage;
            var expectedCommentsList = expextedResult.GetPage(page, entitesPerPage)
                .ToList();

            var result = commentService.All<CommentViewModel>(page, entitesPerPage, musicId);
            var countPages = this.GetCommentsPagesCount(expextedResult.Count(), entitesPerPage);

            CheckCommentViewModelsIsEqual(page, expectedCommentsList, result, countPages);
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

        private static void CheckCommentViewModelsIsEqual(int page, List<CommentViewModel> expectedCommentList, CommentsAllViewModel<CommentViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultComments = result.Comments.ToList();
            Assert.Equal(resultComments.Count, expectedCommentList.Count);

            for (int i = 0; i < expectedCommentList.Count(); i++)
            {
                var expectedComment = expectedCommentList[i];
                var resultComment = resultComments[i];
                Assert.True(CheckCommentViewModelIsEqual(expectedComment, resultComment));
            }
        }

        private static bool CheckCommentViewModelIsEqual(CommentViewModel expectedComment, CommentViewModel resultComment)
            => expectedComment.Id == resultComment.Id
                && expectedComment.Text == resultComment.Text
                && expectedComment.User.Id == resultComment.User.Id
                && expectedComment.User.ImageUrl == resultComment.User.ImageUrl
                && expectedComment.User.UserName == resultComment.User.UserName
                && expectedComment.CreatedOn == resultComment.CreatedOn;
                //&& expectedComment.CommentsChildren == resultComment.CommentsChildren;

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

            Assert.True(commentService.IsOwn(commentId, userId));
        }

        [Fact]
        public async Task IsOwnedComment_WithValidData_ShouldeReturnFalse()
        {
            var commentService = await this.CreateCommentService(CommentTestsData.GetComments);
            var commentId = CommentTestsData.IsOwnedCommentFalseCommentId;
            var userId = CommentTestsData.IsOwnedCommentFalseUserId;
            Assert.True(commentService.IsOwn(commentId, userId));
        }

        private async Task<CommentService> CreateCommentService(List<Comment> comments)
        {
            var users = CommentTestsData.Users;
            var categories = CommentTestsData.Categories;
            var musics = CommentTestsData.Musics;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Songs.AddRangeAsync(musics);
            await this.context.Comments.AddRangeAsync(comments);
            await this.context.CommentReactions.AddRangeAsync(new List<CommentReaction>());
            await this.context.SaveChangesAsync();
            var service = new CommentService(this.context, new PaginationService());

            return service;
        }
    }
}
