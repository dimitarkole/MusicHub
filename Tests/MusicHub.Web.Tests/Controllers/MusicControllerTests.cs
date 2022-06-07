using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.MusicModels;
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
    public class MusicControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;

            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<MusicViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetMusicById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = MusicTestsData.GetMusics;
            var users = MusicTestsData.Users;
            var categories = MusicTestsData.Categories;
            var musicId = musics[0].Id;

            MyController<MusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Get(musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicDetailModel>()
                    .Passing(actual =>
                    {
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<MusicDetailModel>(musicId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModel;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWhitUserId;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitUserIdAndCategoryId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWhitUserIdAndCategoryId;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitUserName_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWhitUserName;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitCategory_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWithCategory;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void SuggestMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var musicId = MusicTestsData.SuggestMusic.Id;

            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.SuggestMusics(page, musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.SuggestMusicPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.SuggestMusics<MusicViewModel>(page, entitesPerPage, musicId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllMusicsOwn_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var userId = MusicTestsData.CreateUserId;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwn(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllOwn<MusicViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterOwnMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModel;
            var userId = MusicTestsData.CreateUserId;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterOwn(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage,filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWithCategory_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWithCategory;
            var userId = MusicTestsData.CreateUserId;
            MyController<MusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterOwn(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicPageModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var categories = MusicTestsData.Categories;
            var model = MusicTestsData.CreateModel;
            var userId = MusicTestsData.CreateUserId;
            MyController<MusicController>
                .Instance()
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
                            Assert.True(From.Services<ApplicationDbContext>().Musics
                                .Any(m => m.Id == property.GetValue(obj).ToString()));
                        }
                    }));
        }

        [Fact]
        public void CreateMusics_WithEmptyModel_ShouldReturnBadRequest()
        {
            var categories = MusicTestsData.Categories;
            var model = MusicTestsData.CreateEmptyModel;
            var userId = MusicTestsData.CreateUserId;
            MyController<MusicController>
                .Instance()
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
                    if (string.IsNullOrEmpty(model.CategoryId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The CategoryId field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.ImageUrl))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ImageUrl field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.AudioUrl))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The AudioUrl field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.Description))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Description field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().Musics
                .Any(m => m.CategoryId == model.CategoryId
                    && m.Name == model.Name
                    && m.ImageUrl == model.ImageUrl
                    && m.AudioUrl == model.AudioUrl
                    && m.Description == model.Description));
        }

        [Fact]
        public void EditMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var categories = MusicTestsData.Categories;
            var musics = MusicTestsData.GetMusics;
            var users = MusicTestsData.Users;
            var model = MusicTestsData.UpdateModel;
            var userId = MusicTestsData.CreateUserId;
            var editMusicId = MusicTestsData.UpdateMusicId;
            MyController<MusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editMusicId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Musics
                .Any(m => m.Id == editMusicId
                    && m.ModifiedOn != null));
        }

        [Fact]
        public void EditMusic_WithEmptyModel_ShouldReturnBadRequest()
        {
            var categories = MusicTestsData.Categories;
            var musics = MusicTestsData.GetMusics;
            var users = MusicTestsData.Users;
            var model = MusicTestsData.UpdateEmptyModel;
            var userId = MusicTestsData.CreateUserId;
            var editMusicId = MusicTestsData.UpdateMusicId;
            MyController<MusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editMusicId, model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Name))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Name field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.CategoryId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The CategoryId field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.ImageUrl))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The ImageUrl field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.AudioUrl))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The AudioUrl field is required")).Any());
                    }
                    if (string.IsNullOrEmpty(model.Description))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Description field is required")).Any());
                    }
                }));

            Assert.True(From.Services<ApplicationDbContext>().Musics
                .Any(m => m.Id == editMusicId
                    && m.ModifiedOn == null));
        }

        [Fact]
        public void EditNotOwnedMusic_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var categories = MusicTestsData.Categories;
            var musics = MusicTestsData.GetMusics;
            var users = MusicTestsData.Users;
            var model = MusicTestsData.UpdateModel;
            var userId = MusicTestsData.IsOwnedMusicFalseUserId;
            var editMusicId = MusicTestsData.IsOwnedFalseMusicId;
            MyController<MusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editMusicId, model))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Musics
                .Any(m => m.Id == editMusicId
                    && m.ModifiedOn == null));
        }

        [Fact]
        public void DeletetMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var categories = MusicTestsData.Categories;
            var musics = MusicTestsData.GetMusics;
            var users = MusicTestsData.Users;
            var userId = MusicTestsData.CreateUserId;
            var deleteMusicId = MusicTestsData.DeleteMusicId;
            MyController<MusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Musics
                .Any(m => m.Id == deleteMusicId));
        }

        [Fact]
        public void DeleteNotOwnedMusic_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var categories = MusicTestsData.Categories;
            var musics = MusicTestsData.GetMusics;
            var users = MusicTestsData.Users;
            var userId = MusicTestsData.IsOwnedMusicFalseUserId;
            var deleteMusicId = MusicTestsData.DeleteMusicId;
            MyController<MusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteMusicId))
                .ShouldReturn()
                .Forbid();  

            Assert.True(From.Services<ApplicationDbContext>().Musics
                .Any(m => m.Id == deleteMusicId));
        }
    }
}
