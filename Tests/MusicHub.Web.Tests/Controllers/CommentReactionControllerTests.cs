using FluentAssertions;
using MusicHub.Data;
using MusicHub.Data.Models;
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

namespace MusicHub.Web.Tests.Controllers
{
    public class CommentReactionControllerTests
    {
        [Fact]
        public void Create_WhitCurrectData_ShouldReturnOk()
         => MyController<CommentReactionController>
            .Instance()
            .WithData(CommentReactionTestsData.Comments)
            .WithUser(user => user
                .WithClaim(ClaimTypes.NameIdentifier, "testUser"))
            .Calling(c => c.Post(CommentReactionTestsData.CreateModel))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void EditOwnedCommentReaction_WhitCurrectData_ShouldReturnOk()
        {
            var comments = CommentReactionTestsData.Comments;
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReaction = commentReactions[0];
            var model = CommentReactionTestsData.UpdateModel;
            MyController<CommentReactionController>
                .Instance()
                .WithData(comments)
                .WithData(commentReactions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, commentReaction.UserId))
                .Calling(c => c.Put(commentReaction.Id, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().CommentReactions
              .Any(m => m.Id == commentReaction.Id
                  && m.ModifiedOn != null));
        }

        [Fact]
        public void EditNotOwnedCommentReaction_WhitCurrectData_ShouldReturnForbid()
        {
            var comments = CommentReactionTestsData.Comments;
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReaction = commentReactions[0];
            var model = CommentReactionTestsData.UpdateModel;
            var userId = commentReactions
                .Where(cr => cr.UserId!= commentReaction.UserId)
                .FirstOrDefault()
                .UserId;

            MyController<CommentReactionController>
                .Instance()
                .WithData(comments)
                .WithData(commentReactions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(commentReaction.Id, model))
                .ShouldReturn()
                .Forbid();
        }

        [Fact]
        public void Delete_WithDataInTheDb_ShouldReturnOk()
        {
            var comments = CommentReactionTestsData.Comments;
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReaction = commentReactions[0];
            var userId = commentReaction.UserId;

            MyController<CommentReactionController>
                .Instance()
                .WithData(comments)
                .WithData(commentReactions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(commentReaction.Id))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void Delete_WithDataInTheDb_ShouldReturnBadRequest()
        {
            var comments = CommentReactionTestsData.Comments;
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReaction = commentReactions[0];
            var userId = comments
                .Where(c => c.UserId != commentReaction.UserId)
                .FirstOrDefault()
                .UserId;

            MyController<CommentReactionController>
                .Instance()
                .WithData(comments)
                .WithData(commentReactions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(commentReaction.Id))
                .ShouldReturn()
                .Forbid();
        }

        [Fact]
        public void GetOwnReaction_WhitCurrectData_ShouldReturnOk()
        {
            var comments = CommentReactionTestsData.Comments;
            var commentReactions = CommentReactionTestsData.CommentReactions();
            var commentReaction = commentReactions[0];
            var userId = commentReaction.UserId;
            var expected = new CommentReactionViewModel()
            {
                Id = commentReaction.Id,
                Reaction = commentReaction.Reaction,
            };

            MyController<CommentReactionController>
              .Instance()
                .WithData(comments)
                .WithData(commentReactions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.Get(commentReaction.CommentId))
              .ShouldReturn()
              .Ok(result => result
                .WithModelOfType<CommentReactionViewModel>()
                .Passing(actual =>
                {  
                    expected.Should().BeEquivalentTo(actual);
                }));
        }    
    }
}
