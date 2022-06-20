using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.LicenseMusicModels;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;

namespace MusicHub.Web.Tests.Controllers
{
    public class LicenseMusicControllerTests
    {
        [Fact]
        public void GetLicenseMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = LicenseMusicTestsData.Musics;
            var licenses = LicenseMusicTestsData.Licenses;
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicId = musics[0].Id;
            MyController<LicenseMusicController>
                .Instance()
                .WithData(musics)
                .WithData(licenses)
                .WithData(musicLicenses)
                .Calling(c => c.Get(musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<LicenseMusicViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new LicenseMusicService(From.Services<ApplicationDbContext>());
                        var expected = service.All<LicenseMusicViewModel>(musicId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateLicenseMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = LicenseMusicTestsData.Musics;
            var licenses = LicenseMusicTestsData.Licenses;
            var model = new LicenseMusicCreateModel()
            {
                LicenseId = licenses[0].Id,
                MusicId = musics[0].Id,
            };
            var userId = LicenseMusicTestsData.CreateUserId;
            MyController<LicenseMusicController>
                .Instance()
                .WithData(musics)
                .WithData(licenses)
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
                            Assert.True(From.Services<ApplicationDbContext>().MusicLicenses
                                .Any(m => m.Id == property.GetValue(obj).ToString()));
                        }
                    }));
        }

        [Fact]
        public void CreateLicenseMusic_WithEmptyModel_ShouldReturnBadRequest()
        {
            var musics = LicenseMusicTestsData.Musics;
            var licenses = LicenseMusicTestsData.Licenses;
            var model = new LicenseMusicCreateModel()
            {
                LicenseId = "",
                MusicId = "",
            };
            var userId = LicenseMusicTestsData.CreateUserId;
            MyController<LicenseMusicController>
                .Instance()
                .WithData(musics)
                .WithData(licenses)
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
                    if (string.IsNullOrEmpty(model.LicenseId))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The LicenseId field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().MusicLicenses
                .Any(ml => ml.MusicId == model.MusicId
                    && ml.LicenseId == model.LicenseId));
        }

        [Fact]
        public void DeleteLicenseMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = LicenseMusicTestsData.Musics;
            var licenses = LicenseMusicTestsData.Licenses;
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicId = musics[0].Id;
            var userId = LicenseMusicTestsData.CreateUserId;
            var deletelicenseMusicId = musicLicenses[0].Id;
            MyController<LicenseMusicController>
                .Instance()
                .WithData(musics)
                .WithData(licenses)
                .WithData(musicLicenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deletelicenseMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().MusicLicenses
                .Any(ml => ml.Id == deletelicenseMusicId));
        }

        [Fact]
        public void AllDeleteLicenseMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = LicenseMusicTestsData.Musics;
            var licenses = LicenseMusicTestsData.Licenses;
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicId = musics[0].Id;
            var userId = LicenseMusicTestsData.CreateUserId;
            var deletelicenseMusicId = musicLicenses[0].MusicId;
            MyController<LicenseMusicController>
                .Instance()
                .WithData(musics)
                .WithData(licenses)
                .WithData(musicLicenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.DeleteAllMusicLicenses(deletelicenseMusicId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().MusicLicenses
                .Any(ml => ml.MusicId == deletelicenseMusicId));
        }
    }
}
