using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.MusicModels;
using MusicHub.Web.ViewModels.PlaylistModels;
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
    public class PlaylistControllerTests
    {

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllPlaylists_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;

            MyController<PlaylistController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.GetPlaylists(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistPageModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PlaylistViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterPlaylists_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var filter = PlaylistTestsData.FilterModel;

            MyController<PlaylistController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.PlaylistFilter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistPageModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PlaylistViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterPlaylistsWithUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var filter = PlaylistTestsData.FilterModelWithUserId;

            MyController<PlaylistController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.PlaylistFilter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistPageModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PlaylistViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }


        [Fact]
        public void GetPlaylistById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var playlistId = playlists[0].Id;

            MyController<PlaylistController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.GetPlaylit(playlistId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistPlayModel>()
                    .Passing(actual =>
                    {
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<PlaylistPlayModel>(playlistId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllOwnedPlaylists_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var userId = PlaylistTestsData.CreateUserId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwn(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistPageModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlayedMusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllOwned<PlaylistViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetOwnedPlaylistsForAddingMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var userId = PlaylistTestsData.CreateUserId;
            var musicId = PlaylistTestsData.GetOwnedForAddingMusicMusicId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwnForAddingMusic(musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<IEnumerable<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetOwnedForAddingMusic<PlaylistViewModel>(musicId, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterAllOwnedPlaylists_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var userId = PlaylistTestsData.CreateUserId;
            var filter = PlaylistTestsData.FilterModel;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterOwn(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistPageModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlayedMusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PlaylistViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void DeletetPlaylist_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var userId = PlaylistTestsData.CreateUserId;
            var deletePlaylistId = PlaylistTestsData.DeletePlaylistId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deletePlaylistId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Playlists
                .Any(m => m.Id == deletePlaylistId));
        }

        [Fact]
        public void DeleteNotOwnedPlaylist_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var userId = PlaylistTestsData.IsOwnedPlaylistFalseUserId;
            var deletePlaylistId = PlaylistTestsData.IsOwnedPlaylistFalsePlaylistId;
            MyController<PlaylistController>
              .Instance()
              .WithData(musics)
              .WithData(categories)
              .WithData(playlists)
              .WithUser(user => user
                  .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.Delete(deletePlaylistId))
              .ShouldReturn()
              .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Playlists
                .Any(m => m.Id == deletePlaylistId));
        }

        [Fact]
        public void IsPlaylistOwned_WithDataInTheDb_ShouldReturnOkWithTrue()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var userId = PlaylistTestsData.IsOwnedPlaylistTrueUserId;
            var playlistId = PlaylistTestsData.IsOwnedPlaylistTruePlaylistId;
            MyController<PlaylistController>
              .Instance()
              .WithData(musics)
              .WithData(categories)
              .WithData(playlists)
              .WithUser(user => user
                  .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.IsOwned(playlistId))
              .ShouldReturn()
               .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void IsPlaylistOwned_WithDataInTheDb_ShouldReturnOkWithFalse()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var userId = PlaylistTestsData.IsOwnedPlaylistFalseUserId;
            var playlistId = PlaylistTestsData.IsOwnedPlaylistFalsePlaylistId;
            MyController<PlaylistController>
              .Instance()
              .WithData(musics)
              .WithData(categories)
              .WithData(playlists)
              .WithUser(user => user
                  .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.IsOwned(playlistId))
              .ShouldReturn()
               .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

        [Fact]
        public void CreatePlaylist_WithDataInTheDb_ShouldReturnOkWithPlaylistId()
        {
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var model = PlaylistTestsData.CreateModel;
            var userId = PlaylistTestsData.CreateUserId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
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
                            if(property.Name == "playlistId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().Playlists
                                    .Any(p => p.Id == property.GetValue(obj).ToString()
                                        && p.Name == model.Name
                                        && p.UserId == userId
                                        && p.Privacy == model.Privacy
                                        && p.ImageUrl == model.ImageUrl));
                            }
                        }
                    }));
        }

        [Fact]
        public void CreatePlaylist_WithEmptyData_ShouldReturnBadRequest()
        {
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var model = PlaylistTestsData.CreateEmptyModel;
            var userId = PlaylistTestsData.CreateUserId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Name))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Name field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.ImageUrl))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ImageUrl field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().Playlists
                                    .Any(p => p.Name == model.Name
                                        && p.UserId == userId
                                        && p.Privacy == model.Privacy
                                        && p.ImageUrl == model.ImageUrl));
        }

        [Fact]
        public void EditPlaylist_WithDataInTheDb_ShouldReturnOk()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var model = PlaylistTestsData.UpdateModel;
            var playlistId = PlaylistTestsData.IsOwnedPlaylistTruePlaylistId;
            var userId = PlaylistTestsData.IsOwnedPlaylistTrueUserId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(playlists)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(playlistId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Playlists
             .Any(m => m.Id == playlistId
                 && m.ModifiedOn != null));
        }

        [Fact]
        public void EditPlaylist_WithEmptyModel_ShouldReturnBadRequest()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var model = PlaylistTestsData.UpdateEmptyModel;
            var playlistId = PlaylistTestsData.IsOwnedPlaylistTruePlaylistId;
            var userId = PlaylistTestsData.IsOwnedPlaylistTrueUserId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(playlists)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(playlistId, model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Name))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Name field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.ImageUrl))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ImageUrl field is required")).Any());
                    }
                }));

            Assert.True(From.Services<ApplicationDbContext>().Playlists
             .Any(p => p.Id == playlistId
                 && p.ModifiedOn == null));
        }

        [Fact]
        public void EditNotOwnedPlaylist_WithDataInTheDb_ShouldReturnForbid()
        {
            var musics = PlaylistTestsData.Musics;
            var playlists = PlaylistTestsData.Playlists;
            var categories = PlaylistTestsData.Categories;
            var model = PlaylistTestsData.UpdateModel;
            var playlistId = PlaylistTestsData.IsOwnedPlaylistFalsePlaylistId;
            var userId = PlaylistTestsData.IsOwnedPlaylistFalseUserId;
            MyController<PlaylistController>
                .Instance()
                .WithData(musics)
                .WithData(playlists)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(playlistId, model))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Playlists
             .Any(m => m.Id == playlistId
                 && m.ModifiedOn == null));
        }
    }
}
