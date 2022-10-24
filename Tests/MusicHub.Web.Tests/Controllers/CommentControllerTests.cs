using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using MusicHub.Services.Data;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.CommentModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Services;
using MusicHub.Data;

namespace MusicHub.Web.Tests.Controllers
{
    public class CommentControllerTests
    {
        [Fact]
        public void Create_WhitCurrectData_ShouldReturnOk()
           => MyController<CommentController>
           .Instance()
           .Calling(c => c.Post(CommentTestsData.CreateModel))
           .ShouldReturn()
           .Ok();


        [Fact]
        public void Create_WhitIncurrectData_ShouldReturnBadRequest()
        {
            var model = CommentTestsData.CreateModelIncurrect;
            MyController<CommentController>
           .Instance()
           .Calling(c => c.Post(model))
           .ShouldReturn()
           .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actual =>
                {
                    if (string.IsNullOrWhiteSpace(model.MusicId))
                    {
                        Assert.Contains($"The MusicId field is required.", actual);
                    }
                    if (string.IsNullOrWhiteSpace(model.Text))
                    {
                        Assert.Contains($"The Text field is required.", actual);
                    }
                }));
        }

        [Fact]
        public void CreateChildComment_WhitIncurrectData_ShouldReturnBadRequest()
        {
            var comments = CommentTestsData.GetComments;
            var parentCommentId = comments[0].Id;           
            var model = CommentTestsData.CreateChildIncurrectModel;
            MyController<CommentController>
               .Instance()
               .WithData(comments)
               .Calling(c => c.CreateChildComment(model, parentCommentId))
               .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actual =>
                    {
                        if (string.IsNullOrWhiteSpace(model.Text))
                        {
                            Assert.Contains($"The Text field is required.", actual);
                        }
                    }));
        }

        [Fact]
        public void CreateChildrenComment_WhitCurrectData_ShouldReturnOk()
        {
            var comments = CommentTestsData.GetComments;
            var parentCommentId = comments[0].Id;
            var model = CommentTestsData.CreateChildModel;
            MyController<CommentController>
               .Instance()
               .WithData(comments)
               .Calling(c => c.CreateChildComment(model, parentCommentId))
               .ShouldReturn()
               .Ok();
        } 

        [Fact]
        public void EditComment_WhitCurrectData_ShouldReturnOk()
        {
            var comments = CommentTestsData.GetComments;
            var comment = comments[0];
            var model = CommentTestsData.UpdateModel;
            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, comment.UserId))
                .Calling(c => c.Put(comment.Id, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Comments
              .Any(m => m.Id == comment.Id
                  && m.ModifiedOn != null));
        }

        [Fact]
        public void EditComment_WhitIncurrectData_ShouldReturnBadRequest()
        {
            var comments = CommentTestsData.GetComments;
            var userId = comments[0].UserId;
            var commentId = comments[0].Id;
            var model = CommentTestsData.UpdateIncurrectModel;

            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .WithData(CommentTestsData.Users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(commentId, model))
                .ShouldReturn()
                .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actual =>
                    {
                        if (string.IsNullOrWhiteSpace(model.Text))
                        {
                            Assert.Contains($"The Text field is required.", actual);
                        }
                    }));

            //Assert.True(From.Services<ApplicationDbContext>().Comments
            //  .Any(c => c.Id == commentId
            //      && c.ModifiedOn == null));
        }

        [Fact]
        public void EditComment_WhitCurrectData_CommentIsNotOwn_ShouldReturnForbid()
        {
            var comments = CommentTestsData.GetComments;
            var comment = comments[0];
            var commentId = comment.Id;
            var model = CommentTestsData.UpdateModel;
            var userId = comments
                .Where(c => c.UserId != comment.UserId)
                .OrderByDescending(c => c.UserId)
                .FirstOrDefault()
                .UserId;

            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(commentId, model))
                .ShouldReturn()
                .Forbid();
            
          /*  Assert.True(From.Services<ApplicationDbContext>().Comments
              .Any(m => m.Id == commentId
                 // && m.ModifiedOn == null));*/
        }

        [Fact]
        public void IsOwned_WhitCurrectData_ShouldReturnFalse()
        {
            var comments = CommentTestsData.GetComments;
            var comment = comments[0];
            var commentId = comments[0].Id;
            var userId = comments
                .Where(c => c.UserId != comment.UserId)
                .OrderByDescending(c => c.UserId)
                .FirstOrDefault()
                .UserId;

            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsOwned(commentId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

        [Fact]
        public void IsOwned_WhitCurrectData_ShouldReturnTrue()
        {
            var comments = CommentTestsData.GetComments;
            var comment = comments[0];
            var commentId = comments[0].Id;
            var userId = comment.UserId;

            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsOwned(commentId))
                .ShouldReturn()
               .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void Delete_WithDataInTheDb_ShouldReturnOk()
        {
            var comments = CommentTestsData.GetComments;
            var comment = comments[0];
            var commentId = comments[0].Id;
            var userId = comment.UserId;

            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(commentId))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void Delete_WithDataInTheDb_ShouldReturnBadRequest()
        {
            var comments = CommentTestsData.GetComments;
            var comment = comments[0];
            var commentId = comments[0].Id;
            var userId = comments
                .Where(c => c.UserId != comment.UserId)
                .OrderByDescending(c => c.UserId)
                .FirstOrDefault()
                .UserId; ;

            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(commentId))
                .ShouldReturn()
                .BadRequest(result => result
                    .WithErrorOfType<string>()
                    .Passing(actual =>
                    {
                        Assert.Contains($"This comment is not owned!", actual);
                    })
                );
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void All_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var comments = CommentTestsData.GetComments;
            var musicId = comments[0].MusicId;

            var expectedCommentLists = comments
               .Where(c => c.MusicId == musicId)
               .OrderByDescending(c => c.CreatedOn)
               .To<CommentViewModel>();
            var entitesPerPage = PaginationData.CommentsPerPage;

            var expected = new CommentPageModel<CommentViewModel>()
            {
                Comments = expectedCommentLists
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetCommentsPagesCount(expectedCommentLists.Count(), entitesPerPage),
            };

            MyController<CommentController>
                .Instance()
                .WithData(comments)
                .Calling(c => c.Get(musicId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<CommentPageModel<CommentViewModel>>()
                    .Passing(actual =>
                    {
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        private int GetCommentsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }
    }
}
