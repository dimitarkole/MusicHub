using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Data.Models;
using System.Security.Claims;
using MusicHub.Web.ViewModels.PlayedMusicModels;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Services.Data;
using MusicHub.Data;
using MusicHub.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace MusicHub.Web.Tests.Controllers
{
    public class PlayedMusicControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllPlayedMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = PlayedMusicTestsData.Musics;
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var categories = PlayedMusicTestsData.Categories;
            var userId = playedMusics[0].UserId;
            MyController<PlayedMusicController>
                .Instance()
                .WithData(categories)
                .WithData(musics)
                .WithData(playedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlayedMusicAllViewModel<PlayedMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlayedMusicsPerPage;
                        var service = new PlayedMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PlayedMusicViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterAllPlayedMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = PlayedMusicTestsData.Musics;
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var categories = PlayedMusicTestsData.Categories;
            var filter = PlayedMusicTestsData.filterModel;
            var userId = playedMusics[0].UserId;
            MyController<PlayedMusicController>
                .Instance()
                .WithData(categories)
                .WithData(musics)
                .WithData(playedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlayedMusicAllViewModel<PlayedMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlayedMusicsPerPage;
                        var service = new PlayedMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PlayedMusicViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreatePlayedMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlayedMusicTestsData.Musics;
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var categories = PlayedMusicTestsData.Categories;
            var userId = playedMusics[0].UserId;
            var model = PlayedMusicTestsData.CreateModel;
            MyController<PlayedMusicController>
                .Instance()
                .WithData(categories)
                .WithData(musics)
                .WithData(playedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(StatusCodes.Status201Created)
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if(property.Name == "playedMusicId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().PlayedMusics
                                    .Any(m => m.Id == property.GetValue(obj).ToString()));
                            }
                        }
                    }));
        }

        [Fact]
        public void DeletePlayedMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlayedMusicTestsData.Musics;
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var categories = PlayedMusicTestsData.Categories;
            var userId = playedMusics[0].UserId;
            var deletePlayedMusicId = playedMusics[0].Id;
            MyController<PlayedMusicController>
                .Instance()
                .WithData(categories)
                .WithData(musics)
                .WithData(playedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deletePlayedMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().PlayedMusics
                .Any(m => m.Id == deletePlayedMusicId));
        }

        [Fact]
        public void DeleteNotOwnedPlayedMusic_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var musics = PlayedMusicTestsData.Musics;
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var categories = PlayedMusicTestsData.Categories;
            var deletePlayedMusicId = playedMusics[0].Id;
            var userId = playedMusics.Where(pm => pm.UserId != playedMusics[0].UserId)
                .FirstOrDefault()
                .UserId;

            MyController<PlayedMusicController>
                .Instance()
                .WithData(categories)
                .WithData(musics)
                .WithData(playedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deletePlayedMusicId))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().PlayedMusics
                .Any(m => m.Id == deletePlayedMusicId));
        }
    }
}
