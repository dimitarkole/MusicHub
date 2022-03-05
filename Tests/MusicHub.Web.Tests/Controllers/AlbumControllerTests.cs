using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.AlbumModels;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class AlbumControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllOwnedAlbums_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var userId = AlbumTestsData.CreateUserId;
            MyController<AlbumController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwn(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<AlbumPageModel<AlbumViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.AlbumsPerPage;
                        var service = new AlbumService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<AlbumViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetOwnedAlbumsForAddingMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var userId = AlbumTestsData.CreateUserId;
            var musicId = AlbumTestsData.GetOwnedForAddingMusicMusicId;
            MyController<AlbumController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwnForAddingMusic(musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<IEnumerable<AlbumViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new AlbumService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetOwnedForAddingMusic<AlbumViewModel>(musicId, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterAllOwnedAlbums_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var userId = AlbumTestsData.CreateUserId;
            var filter = AlbumTestsData.FilterModel;
            MyController<AlbumController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterOwn(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<AlbumPageModel<AlbumViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.PlayedMusicsPerPage;
                        var service = new AlbumService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<AlbumViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void DeletetAlbum_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var userId = AlbumTestsData.CreateUserId;
            var deleteAlbumId = AlbumTestsData.DeleteAlbumId;
            MyController<AlbumController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(album)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteAlbumId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Albums
                .Any(m => m.Id == deleteAlbumId));
        }

        [Fact]
        public void DeleteNotOwnedAlbum_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var userId = AlbumTestsData.IsOwnedAlbumFalseUserId;
            var deleteAlbumId = AlbumTestsData.IsOwnedAlbumFalseAlbumId;
            MyController<AlbumController>
              .Instance()
              .WithData(musics)
              .WithData(categories)
              .WithData(album)
              .WithUser(user => user
                  .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.Delete(deleteAlbumId))
              .ShouldReturn()
              .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Albums
                .Any(m => m.Id == deleteAlbumId));
        }

        [Fact]
        public void IsAlbumOwned_WithDataInTheDb_ShouldReturnOkWithTrue()
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var userId = AlbumTestsData.IsOwnedAlbumTrueUserId;
            var albumId = AlbumTestsData.IsOwnedAlbumTrueAlbumId;
            MyController<AlbumController>
              .Instance()
              .WithData(musics)
              .WithData(categories)
              .WithData(album)
              .WithUser(user => user
                  .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.IsOwned(albumId))
              .ShouldReturn()
               .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void IsAlbumOwned_WithDataInTheDb_ShouldReturnOkWithFalse()
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var userId = AlbumTestsData.IsOwnedAlbumFalseUserId;
            var albumId = AlbumTestsData.IsOwnedAlbumFalseAlbumId;
            MyController<AlbumController>
              .Instance()
              .WithData(musics)
              .WithData(categories)
              .WithData(album)
              .WithUser(user => user
                  .WithClaim(ClaimTypes.NameIdentifier, userId))
              .Calling(c => c.IsOwned(albumId))
              .ShouldReturn()
               .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

        [Fact]
        public void CreateAlbum_WithDataInTheDb_ShouldReturnOkWithAlbumId()
        {
            var musics = AlbumTestsData.Musics;
            var categories = AlbumTestsData.Categories;
            var model = AlbumTestsData.CreateModel;
            var userId = AlbumTestsData.CreateUserId;
            MyController<AlbumController>
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
                            if (property.Name == "albumId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().Albums
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
        public void EditAlbum_WithDataInTheDb_ShouldReturnOk()
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var model = AlbumTestsData.UpdateModel;
            var albumId = AlbumTestsData.IsOwnedAlbumTrueAlbumId;
            var userId = AlbumTestsData.IsOwnedAlbumTrueUserId;
            MyController<AlbumController>
                .Instance()
                .WithData(musics)
                .WithData(album)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(albumId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Albums
             .Any(m => m.Id == albumId
                 && m.ModifiedOn != null));
        }

        [Fact]
        public void EditNotOwnedAlbum_WithDataInTheDb_ShouldReturnForbid()
        {
            var musics = AlbumTestsData.Musics;
            var album = AlbumTestsData.Albums;
            var categories = AlbumTestsData.Categories;
            var model = AlbumTestsData.UpdateModel;
            var albumId = AlbumTestsData.IsOwnedAlbumFalseAlbumId;
            var userId = AlbumTestsData.IsOwnedAlbumFalseUserId;
            MyController<AlbumController>
                .Instance()
                .WithData(musics)
                .WithData(album)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(albumId, model))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Albums
             .Any(m => m.Id == albumId
                 && m.ModifiedOn == null));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllAlbums_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var albums = AlbumTestsData.Albums;
            var musics = AlbumTestsData.Musics;
            var categories = AlbumTestsData.Categories;

            MyController<AlbumController>
                .Instance()
                .WithData(albums)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.GetAlbums(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<AlbumPageModel<AlbumViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new AlbumService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<AlbumViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterAlbums_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var albums = AlbumTestsData.Albums;
            var musics = AlbumTestsData.Musics;
            var categories = AlbumTestsData.Categories;
            var filter = AlbumTestsData.FilterModel;

            MyController<AlbumController>
                .Instance()
                .WithData(albums)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.AlbumsFilter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<AlbumPageModel<AlbumViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new AlbumService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<AlbumViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterAlbumsWithUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var albums = AlbumTestsData.Albums;
            var musics = AlbumTestsData.Musics;
            var categories = AlbumTestsData.Categories;
            var filter = AlbumTestsData.FilterModelWithUserId;

            MyController<AlbumController>
                .Instance()
                .WithData(albums)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.AlbumsFilter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<AlbumPageModel<AlbumViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new AlbumService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<AlbumViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }


        [Fact]
        public void GetAlbumById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var albums = AlbumTestsData.Albums;
            var musics = AlbumTestsData.Musics;
            var categories = AlbumTestsData.Categories;
            var albumId = albums[0].Id;

            MyController<AlbumController>
                .Instance()
                .WithData(albums)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.GetAlbum(albumId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<AlbumPlayModel>()
                    .Passing(actual =>
                    {
                        var service = new AlbumService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<AlbumPlayModel>(albumId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }
    }
}
