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
    }
}
