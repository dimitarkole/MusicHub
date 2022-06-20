using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.LicenseModels;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace LicenseHub.Web.Tests.Controllers
{
    public class LicenseControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllLicenses_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenseLargeViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<LicenseLargeViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllOwnedLicenses_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                 .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwn(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenceViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllOwned<LicenceViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllOwnedApprovedLicenses_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwnApproved())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<LicenseSmallViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllOwnedApproved<LicenseSmallViewModel>(userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetFiles_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseFiles = LicenseTestsData.LicenseFiles(licenses);
            var licenseId = licenses[0].Id;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .Calling(c => c.GetFiles(licenseId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<LicenceFileViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllFiles<LicenceFileViewModel>(licenseId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterLicenses_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var filter = LicenseTestsData.LicenseFilter;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenseLargeViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void LicenseFilterWithStatus_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var filter = LicenseTestsData.LicenseFilterWithStatus;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenseLargeViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void LicenseFilterWithUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var filter = LicenseTestsData.LicenseFilterWithUserId;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenseLargeViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterOwnLicenses_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var filter = LicenseTestsData.LicenseFilter;
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenseLargeViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void LicenseFilterOwnWithStatus_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var filter = LicenseTestsData.LicenseFilterWithStatus;
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenseLargeViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void LicenseFilterOwnWithUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var filter = LicenseTestsData.LicenseFilterWithUserId;
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicensePageModel<LicenseLargeViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.LicensesPerPage;
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseId = licenses[0].Id;
            MyController<LicenseController>
                .Instance()
                .WithData(licenses)
                .Calling(c => c.GetById(licenseId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<LicenseLargeViewModel>()
                    .Passing(actual =>
                    {
                        var service = new LicenseService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<LicenseLargeViewModel>(licenseId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreateLicense_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var model = LicenseTestsData.CreateModel;
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
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
                            Assert.True(From.Services<ApplicationDbContext>().Licenses
                                .Any(m => m.Id == property.GetValue(obj).ToString()));
                        }
                    }));
        }

        [Fact]
        public void CreateLicenses_WithEmptyModel_ShouldReturnBadRequest()
        {
            var model = LicenseTestsData.CreateEmptyModel;
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
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
                }));

            Assert.False(From.Services<ApplicationDbContext>().Licenses
                .Any(l => l.Name == model.Name));
        }

        [Fact]
        public void DeletetLicense_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var users = LicenseTestsData.Users;
            var userId = licenses[0].UserId;
            var deleteLicenseId = licenses[0].Id;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteLicenseId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Licenses
                .Any(m => m.Id == deleteLicenseId));
        }

        [Fact]
        public void DeleteNotOwnedLicense_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var users = LicenseTestsData.Users;
            var ownerUserId = licenses[0].UserId;
            var userId = licenses.Where(l => l.UserId != ownerUserId)
                .FirstOrDefault()
                .UserId;
            var deleteLicenseId = licenses[0].Id;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteLicenseId))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Licenses
                .Any(m => m.Id == deleteLicenseId));
        }

        [Fact]
        public void DeletetLicenseFile_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseFiles = LicenseTestsData.LicenseFiles(licenses);
            var users = LicenseTestsData.Users;
            var userId = licenses[0].UserId;
            var licenseId = licenses[0].Id;
            var deleteLicenseFileId = licenseFiles
                .Where(lf => lf.LicensеId == licenseId)
                .FirstOrDefault()
                .Id;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithData(licenseFiles)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.DeleteFile(deleteLicenseFileId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().LicenseFiles
                .Any(m => m.Id == deleteLicenseFileId));
        }

        [Fact]
        public void EditMusic_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var users = LicenseTestsData.Users;
            var userId = licenses[0].UserId;
            var editLicenseId = licenses[0].Id;
            var model = LicenseTestsData.UpdateModel;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editLicenseId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Licenses
                .Any(m => m.Id == editLicenseId
                    && m.ModifiedOn != null));
        }

        [Fact]
        public void EditLicense_WithEmptyModel_ShouldReturnBadRequest()
        {
            var licenses = LicenseTestsData.Licenses();
            var users = LicenseTestsData.Users;
            var userId = licenses[0].UserId;
            var editLicenseId = licenses[0].Id;
            var model = LicenseTestsData.UpdateEmptyModel;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editLicenseId, model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Name))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Name field is required")).Any());
                    }
                }));

            //Assert.True(From.Services<ApplicationDbContext>().Licenses
            //    .Any(m => m.Id == editLicenseId
            //        && m.ModifiedOn == null));
        }

        [Fact]
        public void EditNotOwnedLicense_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var users = LicenseTestsData.Users;
            var ownerUserId = licenses[0].UserId;
            var userId = licenses.Where(l => l.UserId != ownerUserId)
                .FirstOrDefault()
                .UserId;
            var editLicenseId = licenses[0].Id;
            var model = LicenseTestsData.UpdateModel;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(editLicenseId, model))
                .ShouldReturn()
                .Forbid();

            //Assert.True(From.Services<ApplicationDbContext>().Licenses
            //    .Any(m => m.Id == editLicenseId
            //        && m.ModifiedOn == null));
        }

        [Fact]
        public void CreateLicenseFile_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var licenses = LicenseTestsData.Licenses();
            var model = LicenseTestsData.CreateFileModel;
            var users = LicenseTestsData.Users;
            var userId = LicenseTestsData.CreateUserId;
            model.LicensеId = licenses[0].Id;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.PostFiles(model))
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(StatusCodes.Status201Created)
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            Assert.True(From.Services<ApplicationDbContext>().LicenseFiles
                                .Any(m => m.Id == property.GetValue(obj).ToString()));
                        }
                    }));
        }

        [Fact]
        public void CreateLicenseFile_WithEmptyModel_ShouldReturnBadRequest()
        {
            var users = LicenseTestsData.Users;
            var licenses = LicenseTestsData.Licenses();
            var model = LicenseTestsData.CreateFileEmptyModel;
            var userId = LicenseTestsData.CreateUserId;
            MyController<LicenseController>
                .Instance()
                .WithData(users)
                .WithData(licenses)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.PostFiles(model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.FileUrl))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The FileUrl field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().LicenseFiles
                .Any(l => l.FileUrl == model.FileUrl));
        }
    }
}
