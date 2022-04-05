using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.ReportMusicModels;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class ReportMusicControllerTests
    {

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllReportedMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var userId = ReportMusicTestsData.UserId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetAllReportedMusic(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportMusicPageModel<ReportMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllReportedMusic<ReportMusicViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterReportedMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var filter = ReportMusicTestsData.FilterModel;
            var userId = ReportMusicTestsData.UserId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterReportedMusic(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportMusicPageModel<ReportMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.FilterReported<ReportMusicViewModel>(filter, page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterReportedMusics_FilterModelWithCategory_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var filter = ReportMusicTestsData.FilterModelWithCategory;
            var userId = ReportMusicTestsData.UserId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterReportedMusic(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportMusicPageModel<ReportMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.FilterReported<ReportMusicViewModel>(filter, page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterReportedMusics_FilterModelWhitUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var filter = ReportMusicTestsData.FilterModelWhitUserId;
            var userId = ReportMusicTestsData.UserId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterReportedMusic(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportMusicPageModel<ReportMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.FilterReported<ReportMusicViewModel>(filter, page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllReports_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var userId = ReportMusicTestsData.UserId;
            var musicId = reportedMusics[0].MusicId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetAllReported(musicId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportPageModel<ReportViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new ReportMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<ReportViewModel>(musicId, page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterReports_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var userId = ReportMusicTestsData.UserId;
            var musicId = reportedMusics[0].MusicId;
            var filter = ReportMusicTestsData.FilterModelReportWithTypeUserIdAndUsername;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterReported(musicId, page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportPageModel<ReportViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new ReportMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<ReportViewModel>(musicId, filter, page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterReports_FilterModelReportWithTypeUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var userId = ReportMusicTestsData.UserId;
            var musicId = reportedMusics[0].MusicId;
            var filter = ReportMusicTestsData.FilterModelReportWithTypeUserId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterReported(musicId, page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportPageModel<ReportViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new ReportMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<ReportViewModel>(musicId, filter, page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterReports_FilterModelReportWithType_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var userId = ReportMusicTestsData.UserId;
            var musicId = reportedMusics[0].MusicId;
            var filter = ReportMusicTestsData.FilterModelReportWithType;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FilterReported(musicId, page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportPageModel<ReportViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new ReportMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<ReportViewModel>(musicId, filter, page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var categories = ReportMusicTestsData.Categories;
            var musics = ReportMusicTestsData.Musics;
            var users = ReportMusicTestsData.Users;
            var model = ReportMusicTestsData.CreateModel;
            var userId = ReportMusicTestsData.UserId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(categories)
                .WithData(users)
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
                            Assert.True(From.Services<ApplicationDbContext>().ReportMusics
                                .Any(m => m.Id == property.GetValue(obj).ToString()));
                        }
                    }));
        }

        [Fact]
        public void CreateMusics_WithEmptyModel_ShouldReturnBadRequest()
        {
            var categories = ReportMusicTestsData.Categories;
            var musics = ReportMusicTestsData.Musics;
            var users = ReportMusicTestsData.Users;
            var model = ReportMusicTestsData.CreateEmptyModel;
            var userId = ReportMusicTestsData.UserId;
            MyController<ReportMusicController>
                .Instance()
                .WithData(categories)
                .WithData(users)
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
                    if (string.IsNullOrEmpty(model.Description))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Description field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().ReportMusics
                .Any(rm => rm.Description == model.Description
                    && rm.UserId == userId
                    && rm.Type == model.Type));
        }

        [Fact]
        public void DeleteMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var categories = ReportMusicTestsData.Categories;
            var musics = ReportMusicTestsData.Musics;
            var users = ReportMusicTestsData.Users;
            var userId = ReportMusicTestsData.UserId;
            var deleteMusicId = musics[0].Id;
            MyController<ReportMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.DeleteMusic(deleteMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Musics
                .Any(m => m.Id == deleteMusicId));
        }

        [Fact]
        public void DeleteReport_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var categories = ReportMusicTestsData.Categories;
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var users = ReportMusicTestsData.Users;
            var userId = ReportMusicTestsData.UserId;
            var deleteReportMusicId = reportedMusics[0].Id;
            MyController<ReportMusicController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteReportMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().ReportMusics
                .Any(m => m.Id == deleteReportMusicId));
        }

        [Fact]
        public void GetById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = ReportMusicTestsData.Musics;
            var reportedMusics = ReportMusicTestsData.GetReportMusic();
            var categories = ReportMusicTestsData.Categories;
            var userId = ReportMusicTestsData.UserId;
            var reportMusicId = reportedMusics[0].Id;
            MyController<ReportMusicController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .WithData(reportedMusics)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(reportMusicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<ReportDetatilsModel>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.ReportedMusicsPerPage;
                        var service = new ReportMusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<ReportDetatilsModel>(reportMusicId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }
    }
}
