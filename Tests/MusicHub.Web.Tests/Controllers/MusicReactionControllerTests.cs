using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.MusicModels;
using MusicHub.Web.ViewModels.MusicReactionModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class MusicReactionControllerTests
    {
        [Fact]
        public void GetOwnReaction_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = MusicReactionTestsData.Musics;
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var categories = MusicReactionTestsData.Categories;
            var userId = musicReactions[0].UserId;
            var musicId = musicReactions[0].MusicId;
            MyController<MusicReactionController>
                .Instance()
                .WithData(musics)
                .WithData(musicReactions)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwnReaction(musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicReactionViewModel>()
                    .Passing(async actual =>
                    {
                        var service = new MusicReactionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetOwnReaction(musicId, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllLikedMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicReactionTestsData.Musics;
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var categories = MusicReactionTestsData.Categories;
            var userId = MusicReactionTestsData.CreateUserId;
            MyController<MusicReactionController>
                .Instance()
                .WithData(musics)
                .WithData(musicReactions)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetLikedMusics(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LikedMusicPageModel<LikedMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LikedMusicsPerPage;
                        var service = new MusicReactionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<LikedMusicViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterAllLikedMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicReactionTestsData.Musics;
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var categories = MusicReactionTestsData.Categories;
            var filter = MusicReactionTestsData.Filter;
            var userId = MusicReactionTestsData.CreateUserId;
            MyController<MusicReactionController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(musicReactions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterLikedMusics(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LikedMusicPageModel<LikedMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LikedMusicsPerPage;
                        var service = new MusicReactionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LikedMusicViewModel>(page, entitesPerPage, userId, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterAllLikedMusicsByCategory_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicReactionTestsData.Musics;
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var categories = MusicReactionTestsData.Categories;
            var filter = MusicReactionTestsData.FilterWithCategory;
            var userId = MusicReactionTestsData.CreateUserId;
            MyController<MusicReactionController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(musicReactions)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterLikedMusics(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LikedMusicPageModel<LikedMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LikedMusicsPerPage;
                        var service = new MusicReactionService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LikedMusicViewModel>(page, entitesPerPage, userId, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateLikedMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = MusicReactionTestsData.Musics;
            var categories = MusicReactionTestsData.Categories;
            var model = MusicReactionTestsData.CreateModel;
            var userId = MusicReactionTestsData.CreateUserId;
            MyController<MusicReactionController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().MusicReactions
                .Any(mr => mr.MusicId == model.MusicId
                    && mr.Reaction == model.Reaction
                    && mr.UserId == userId));
        }

        [Fact]
        public void EditMusicReaction_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = MusicReactionTestsData.Musics;
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var categories = MusicReactionTestsData.Categories;
            var users = MusicReactionTestsData.Users;
            var model = MusicReactionTestsData.UpdateModel;
            var userId = musicReactions[0].UserId;
            var musicReactionId = musicReactions[0].Id;
            MyController<MusicReactionController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(musicReactions)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(musicReactionId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().MusicReactions
                .Any(m => m.Id == musicReactionId
                    && m.ModifiedOn != null));
        }

        [Fact]
        public void DeletetMusicReaction_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = MusicReactionTestsData.Musics;
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var categories = MusicReactionTestsData.Categories;
            var users = MusicReactionTestsData.Users;
            var userId = musicReactions[0].UserId;
            var musicReactionId = musicReactions[0].Id;
            MyController<MusicReactionController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(musicReactions)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(musicReactionId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().MusicReactions
                .Any(m => m.Id == musicReactionId));
        }

        [Fact]
        public void DeleteNotOwnedMusic_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var musics = MusicReactionTestsData.Musics;
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var categories = MusicReactionTestsData.Categories;
            var users = MusicReactionTestsData.Users;
            var userId = musicReactions[0].UserId;
            var musicReactionId = musicReactions[0].Id;
            var notOwnerUserId = users
              .Where(mr => mr.Id != userId)
              .FirstOrDefault()
              .Id;

            MyController<MusicReactionController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(musicReactions)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, notOwnerUserId))
                .Calling(c => c.Delete(musicReactionId))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().MusicReactions
                .Any(m => m.Id == musicReactionId));
        }
    }
}
