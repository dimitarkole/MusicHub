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
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
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
        public void FilterMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
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
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
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
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
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
