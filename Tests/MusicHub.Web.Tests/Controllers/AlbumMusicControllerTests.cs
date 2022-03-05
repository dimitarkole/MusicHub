using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.AlbumMusicModels;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class AlbumMusicControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetMusicInAlbums_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = AlbumMusicTestsData.Users;
            var musics = AlbumMusicTestsData.Musics;
            var album = AlbumMusicTestsData.Albums;
            var categories = AlbumMusicTestsData.Categories;
            var albumMusics = AlbumMusicTestsData.AlbumMusics();
            var userId = AlbumTestsData.CreateUserId;
            var albumId = AlbumTestsData.GetById;

            MyController<AlbumMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithData(albumMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(albumId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<AlbumMusicsPageModel<AlbumMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.AlbumMusicsPerPage;
                        var service = new AlbumMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<AlbumMusicViewModel>(page, entitesPerPage, albumId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateAlbumMusic_WithOwnedMusic_WithDataInTheDb_ShouldReturnOkWith()
        {
            var users = AlbumMusicTestsData.Users;
            var musics = AlbumMusicTestsData.Musics;
            var album = AlbumMusicTestsData.Albums;
            var categories = AlbumMusicTestsData.Categories;
            var userId = AlbumMusicTestsData.Users[0].Id;
            var model = AlbumMusicTestsData.CreateModelWithOwnedMusic;

            MyController<AlbumMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Create(model))
                .ShouldReturn()
                .StatusCode(StatusCodes.Status201Created);

            Assert.True(From.Services<ApplicationDbContext>().AlbumMusics
                              .Any(am => am.MusicId == model.MusicId
                                  && am.AlbumId == model.AlbumId));
        }

        [Fact]
        public void CreateAlbumMusic_NotOwnedMusic_WithDataInTheDb_ShouldReturnOkWith()
        {
            var users = AlbumMusicTestsData.Users;
            var musics = AlbumMusicTestsData.Musics;
            var album = AlbumMusicTestsData.Albums;
            var categories = AlbumMusicTestsData.Categories;
            var userId = AlbumMusicTestsData.Users[0].Id;
            var model = AlbumMusicTestsData.CreateModelWithNotOwnedMusic;

            MyController<AlbumMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Create(model))
                .ShouldReturn()
                .BadRequest(result => result
                    .WithErrorOfType<string>()
                    .Passing(actualContacts =>
                    {
                        Assert.Contains($"This music is not owned!", actualContacts);
                    }));

               Assert.False(From.Services<ApplicationDbContext>().AlbumMusics
                              .Any(am => am.MusicId == model.MusicId
                                  && am.AlbumId == model.AlbumId));
        }

        [Fact]
        public void DeletetAlbumMusic_WithDataInTheDb_ShouldReturnOk()
        {
            var users = AlbumMusicTestsData.Users;
            var musics = AlbumMusicTestsData.Musics;
            var album = AlbumMusicTestsData.Albums;
            var categories = AlbumMusicTestsData.Categories;
            var albumMusics = AlbumMusicTestsData.AlbumMusics();
            var albumMusicId = albumMusics[0].Id;
            var userId = album
                    .Where(p => p.Id == albumMusics[0].AlbumId)
                    .FirstOrDefault()
                    .UserId;

            MyController<AlbumMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithData(albumMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(albumMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().AlbumMusics
                .Any(m => m.Id == albumMusicId));
        }

        [Fact]
        public void DeleteNotOwnedAlbumMusic_WithDataInTheDb_ShouldReturnForbid()
        {
            var musics = AlbumMusicTestsData.Musics;
            var users = AlbumMusicTestsData.Users;
            var album = AlbumMusicTestsData.Albums;
            var categories = AlbumMusicTestsData.Categories;
            var albumMusics = AlbumMusicTestsData.AlbumMusics();
            var albumMusicId = albumMusics[0].Id;
            var ownerUserId = album
                    .Where(p => p.Id == albumMusics[0].AlbumId)
                    .FirstOrDefault()
                    .UserId;
            var userId = users
                    .Where(u => u.Id != ownerUserId)
                    .FirstOrDefault()
                    .Id;

            MyController<AlbumMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithData(albumMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.Delete(albumMusicId))
              .ShouldReturn()
              .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().AlbumMusics
                .Any(m => m.Id == albumMusicId));
        }
    }
}
