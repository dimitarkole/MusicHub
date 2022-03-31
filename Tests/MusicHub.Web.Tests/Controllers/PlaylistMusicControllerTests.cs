using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.PlaylistModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Services.Mapping;

namespace MusicHub.Web.Tests.Controllers
{
    public class PlaylistMusicControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetMusicInPlaylists_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = PlaylistMusicTestsData.Users;
            var musics = PlaylistMusicTestsData.Musics;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var userId = PlaylistTestsData.CreateUserId;
            var playlistId = PlaylistTestsData.GetById;

            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(playlistId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistMusicsPageModel<PlaylistMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlaylistsMusicsPerPage;
                        var service = new PlaylistMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PlaylistMusicViewModel>(page, entitesPerPage, playlistId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreatePlaylistMusic_WithDataInTheDb_ShouldReturnOkWith()
        {
            var users = PlaylistMusicTestsData.Users;
            var musics = PlaylistMusicTestsData.Musics;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var userId = PlaylistMusicTestsData.Users[0].Id;
            var model = PlaylistMusicTestsData.CreateModel;

            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Create(model))
                .ShouldReturn()
                .StatusCode(StatusCodes.Status201Created);

            Assert.True(From.Services<ApplicationDbContext>().PlaylistMusics
                              .Any(pm => pm.MusicId == model.MusicId
                                  && pm.PlaylistId == model.PlaylistId));
        }

        [Fact]
        public void CreatePlaylistMusic_WithEmptyModel_ShouldReturnBadRequest()
        {
            var users = PlaylistMusicTestsData.Users;
            var musics = PlaylistMusicTestsData.Musics;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var userId = PlaylistMusicTestsData.Users[0].Id;
            var model = PlaylistMusicTestsData.CreateEmptyModel;

            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Create(model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.PlaylistId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The PlaylistId field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.MusicId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The MusicId field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().PlaylistMusics
                 .Any(p => p.PlaylistId == model.PlaylistId
                     && p.MusicId == model.MusicId));
        }

        [Fact]
        public void DeletetPlaylistMusic_WithDataInTheDb_ShouldReturnOk()
        {
            var users = PlaylistMusicTestsData.Users;
            var musics = PlaylistMusicTestsData.Musics;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var playlistMusicId = playlistMusics[0].Id;
            var userId = playlists
                    .Where(p => p.Id == playlistMusics[0].PlaylistId)
                    .FirstOrDefault()
                    .UserId;

            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(playlistMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().PlaylistMusics
                .Any(m => m.Id == playlistMusicId));
        }

        [Fact]
        public void DeleteNotOwnedPlaylistMusic_WithDataInTheDb_ShouldReturnForbid()
        {
            var musics = PlaylistMusicTestsData.Musics;
            var users = PlaylistMusicTestsData.Users;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var playlistMusicId = playlistMusics[0].Id;
            var ownerUserId = playlists
                    .Where(p => p.Id == playlistMusics[0].PlaylistId)
                    .FirstOrDefault()
                    .UserId;
            var userId = users
                    .Where(u => u.Id != ownerUserId)
                    .FirstOrDefault()
                    .Id;

            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.Delete(playlistMusicId))
              .ShouldReturn()
              .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().PlaylistMusics
                .Any(m => m.Id == playlistMusicId));
        }

        [Fact]
        public void IsAddedToPlaylist_WithDataInTheDb_ShouldReturnTrue()
        {
            var musics = PlaylistMusicTestsData.Musics;
            var users = PlaylistMusicTestsData.Users;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var playlistMusic = playlistMusics[0];
            var userId = PlaylistMusicTestsData.Users[0].Id;
            var model = new IsAddedToPlaylistCheckInputModel() { MusicId = playlistMusic.MusicId, PlaylistId = playlistMusic.PlaylistId };
            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.IsAddedToPlaylist(model))
              .ShouldReturn()
              .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void IsAddedToPlaylist_WithDataInTheDb_ShouldReturnFalse()
        {
            var musics = PlaylistMusicTestsData.Musics;
            var users = PlaylistMusicTestsData.Users;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var userId = PlaylistMusicTestsData.Users[0].Id;
            var playlistId = playlistMusics[0].PlaylistId;
            var musicId = playlistMusics.Where(pm => pm.PlaylistId != playlistId)
                .FirstOrDefault()
                .MusicId;
            var model = new IsAddedToPlaylistCheckInputModel() { MusicId = musicId, PlaylistId = playlistId };
            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.IsAddedToPlaylist(model))
              .ShouldReturn()
              .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

        [Fact]
        public void GetPlaylistMusicId_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = PlaylistMusicTestsData.Users;
            var musics = PlaylistMusicTestsData.Musics;
            var playlists = PlaylistMusicTestsData.Playlists;
            var categories = PlaylistMusicTestsData.Categories;
            var playlistMusics = PlaylistMusicTestsData.PlaylistMusics();
            var userId = PlaylistTestsData.CreateUserId;
            var playlistMusic = playlistMusics[0];
            var model = new PlaylistMusicCreateInputModel() { MusicId = playlistMusic.MusicId, PlaylistId = playlistMusic.PlaylistId };
            MyController<PlaylistMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(playlists)
                .WithData(playlistMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetPlaylistMusicId(model))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistMusicViewModel>()
                    .Passing(actual =>
                    {
                        Assert.Equal(playlistMusic.Id, actual.Id);
                    }));
        }
    }
}
