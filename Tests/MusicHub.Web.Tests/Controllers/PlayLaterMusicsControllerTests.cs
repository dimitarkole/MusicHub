using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.PlayLaterMusicModels;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class PlayLaterMusicsControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllPlayLaterMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var categories = PlayLaterMusicTestsData.Categories;
            var userId = playLaterMusics[0].UserId;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(categories)
                .WithData(musics)
                .WithData(playLaterMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlayLaterMusicPageModel<PlayLaterMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlayLaterMusicsPerPage;
                        var service = new PlayLaterMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PlayLaterMusicViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterPlayLaterMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var categories = PlayLaterMusicTestsData.Categories;
            var userId = playLaterMusics[0].UserId;
            var filter = PlayLaterMusicTestsData.FilterModel;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(users)
                .WithData(categories)
                .WithData(musics)
                .WithData(playLaterMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlayLaterMusicPageModel<PlayLaterMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlayLaterMusicsPerPage;
                        var service = new PlayLaterMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PlayLaterMusicViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreatePlayLaterMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var categories = PlayLaterMusicTestsData.Categories;
            var userId = users[0].Id;
            var model = PlayLaterMusicTestsData.CreateModel;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(users)
                .WithData(categories)
                .WithData(musics)
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
                            if (property.Name == "playLaterMusicId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().PlayLaterMusics
                                    .Any(m => m.Id == property.GetValue(obj).ToString()));
                            }
                        }
                    }));
        }

        [Fact]
        public void CreatePlayLaterMusic_WithEmptyModel_ShouldReturnBadRequest()
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var categories = PlayLaterMusicTestsData.Categories;
            var users = PlayLaterMusicTestsData.Users;
            var model = PlayLaterMusicTestsData.CreateEmptyModel;
            var userId = users[0].Id;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(categories)
                .WithData(musics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.MusicId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The MusicId field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().PlayLaterMusics
                .Any(pm => pm.MusicId == model.MusicId
                    && pm.UserId == userId));
        }

        [Fact]
        public void DeletePlayLaterMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var categories = PlayLaterMusicTestsData.Categories;
            var userId = playLaterMusics[0].UserId;
            var deletePlayLaterMusicId = playLaterMusics[0].Id;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(users)
                .WithData(categories)
                .WithData(musics)
                .WithData(playLaterMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deletePlayLaterMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().PlayLaterMusics
                .Any(m => m.Id == deletePlayLaterMusicId));
        }

        [Fact]
        public void DeleteNotOwnedPlayLaterMusic_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var categories = PlayLaterMusicTestsData.Categories;
            var deletePlayLaterMusicId = playLaterMusics[0].Id;
            var userId = playLaterMusics.Where(pm => pm.UserId != playLaterMusics[0].UserId)
                .FirstOrDefault()
                .UserId;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(users)
                .WithData(categories)
                .WithData(musics)
                .WithData(playLaterMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deletePlayLaterMusicId))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().PlayLaterMusics
                .Any(m => m.Id == deletePlayLaterMusicId));
        }

        [Fact]
        public void EditPlayLaterMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var categories = PlayLaterMusicTestsData.Categories;
            var userId = playLaterMusics[0].UserId;
            var editPlayLaterMusicId = playLaterMusics[0].Id;
            var model = PlayLaterMusicTestsData.EditModel;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(users)
                .WithData(categories)
                .WithData(musics)
                .WithData(playLaterMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editPlayLaterMusicId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().PlayLaterMusics
                .Any(plm => plm.Id == editPlayLaterMusicId && plm.Position == model.Position));
        }

        [Fact]
        public void EditPlayLaterMusic_PlayLaterMusicIsNotOwn_WithDataInTheDb_ShouldReturnForbidenh()
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var categories = PlayLaterMusicTestsData.Categories;
            var editPlayLaterMusicId = playLaterMusics[0].Id;
            var owner = playLaterMusics[0].UserId;
            var userId = playLaterMusics.Where(plm => plm.UserId != owner)
                .FirstOrDefault()
                .UserId;
            var oldPosition = playLaterMusics.Where(plm => plm.Id == editPlayLaterMusicId)
                .FirstOrDefault()
                .Position;
            var model = PlayLaterMusicTestsData.EditModel;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(users)
                .WithData(categories)
                .WithData(musics)
                .WithData(playLaterMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editPlayLaterMusicId, model))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().PlayLaterMusics
                .Any(plm => plm.Id == editPlayLaterMusicId 
                    && plm.Position == oldPosition));
        }

        [Fact]
        public void EditPlayLaterMusic_WithEmptyModel_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlayLaterMusicTestsData.Musics;
            var users = PlayLaterMusicTestsData.Users;
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var categories = PlayLaterMusicTestsData.Categories;
            var userId = playLaterMusics[0].UserId;
            var editPlayLaterMusicId = playLaterMusics[0].Id;
            var model = PlayLaterMusicTestsData.EditEmptyModel;
            MyController<PlayLaterMusicsController>
                .Instance()
                .WithData(users)
                .WithData(categories)
                .WithData(musics)
                .WithData(playLaterMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editPlayLaterMusicId, model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (model.Position < 0)
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("Position")).Any());
                    }
                }));
        }
    }
}
