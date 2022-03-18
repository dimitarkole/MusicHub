using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Common.Extensions;
using MusicHub.Services.Mapping;
using MusicHub.Common;
using MusicHub.Web.ViewModels.LicenseModels;
using MusicHub.Data.Models.Enums;
using static MusicHub.Common.GlobalConstants;
using FluentAssertions;

namespace MusicHub.Services.Data.Tests
{
    public class LicenseServiceTests : BaseTestClass
    {
        public int EntitesPerPage = PaginationData.LicensesPerPage;

        [Fact]
        public async Task CreateLicense_WithValidData_ShouldWorkCorrect()
        {
            var licenseService = await this.CreateLicenseService(new List<License>());
            var model = LicenseTestsData.CreateModel;
            var userId = LicenseTestsData.CreateUserId;

            await licenseService.Create(model, userId);

            Assert.True(this.context.Licenses.Any(l => l.Name == model.Name
                && l.Status == model.Status
                && l.UserId == userId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAll_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            var licenseLargeViewModel = this.context.Licenses
                .OrderByDescending(l => l.Status == LicenseStatus.WaitToBeView)
                .ThenByDescending(l => l.CreatedOn)
                .To<LicenseLargeViewModel>();

            var expected = new LicensePageModel<LicenseLargeViewModel>()
            {
                Licenses = licenseLargeViewModel
                    .GetPage(page, this.EntitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLicensesPagesCount(licenses.Count(), this.EntitesPerPage),
            };

            var actual = licenseService.All<LicenseLargeViewModel>(page, this.EntitesPerPage);
            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetOwned_WithValidData_ShouldeReturnWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = licenses[0].UserId;

            var licenseLargeViewModel = this.context.Licenses
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Status == LicenseStatus.WaitToBeView)
                .ThenByDescending(l => l.CreatedOn)
                .ThenByDescending(l => l.Name)
                .To<LicenseLargeViewModel>();

            var expected = new LicensePageModel<LicenseLargeViewModel>()
            {
                Licenses = licenseLargeViewModel
                    .GetPage(page, this.EntitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLicensesPagesCount(licenseLargeViewModel.Count(), this.EntitesPerPage),
            };

            var actual = licenseService.AllOwned<LicenseLargeViewModel>(page, this.EntitesPerPage, userId);

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task AllOwnApproved_WithValidData_ShouldeWorkCurrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = licenses[0].UserId;
            var expected = this.context.Licenses
                .Where(l => l.UserId == userId
                    && l.Status == LicenseStatus.Approve)
                .OrderByDescending(l => l.Name)
                .To<LicenseLargeViewModel>()
                .ToList();

            var actual = licenseService.AllOwnedApproved<LicenseLargeViewModel>(userId)
                .ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilter_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var filter = LicenseTestsData.LicenseFilter;
            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter);

            this.CheckFilterResult(page, actual, licenseService, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserName_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var filter = LicenseTestsData.LicenseFilterWithUserName;

            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter);

            this.CheckFilterResult(page, actual, licenseService, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var filter = LicenseTestsData.LicenseFilterWithStatus;

            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter);

            this.CheckFilterResult(page, actual, licenseService, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserNameAndStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var filter = LicenseTestsData.LicenseFilterWithUserNameAndStatus;

            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter);

            this.CheckFilterResult(page, actual, licenseService, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserId_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var filter = LicenseTestsData.LicenseFilterWithUserId;

            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter);

            this.CheckFilterResult(page, actual, licenseService, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserIdAndStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var filter = LicenseTestsData.LicenseFilterWithUserIdAndStatus;

            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter);

            this.CheckFilterResult(page, actual, licenseService, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task OwnedLicenseFilter_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = LicenseTestsData.LicenseFilterUserId;
            var filter = LicenseTestsData.LicenseFilter;

            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterResult(page, actual, licenseService, filter, userId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task OwnedLicenseFilterWithStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = LicenseTestsData.LicenseFilterUserId;
            var filter = LicenseTestsData.LicenseFilterWithStatus;

            var actual = licenseService.Filter<LicenseLargeViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterResult(page, actual, licenseService, filter, userId);
        }

        [Fact]
        public async Task IsOwnedLicense_WithValidData_ShouldeReturnTrue()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            var userId = licenses[0].UserId;
            var licenseId = licenses[0].Id;

            Assert.True(licenseService.IsOwned(licenseId, userId));
        }

        [Fact]
        public async Task IsOwnedLicense_WithValidData_ShouldeReturnFalse()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            var userId = licenses[0].UserId;
            var licenseId = licenses
                .Where(pd => pd.UserId != userId)
                .FirstOrDefault()
                .Id;

            Assert.False(licenseService.IsOwned(licenseId, userId));
        }

        [Fact]
        public async Task DeleteLicense_WithValidData_ShouldWorkCorrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var licenseId = licenses[0].Id;

            await licenseService.Delete(licenseId);

            Assert.False(this.context.Licenses.Any(c => c.Id == licenseId));
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var licenseId = licenses[0].Id;

            var actual = licenseService.GetById<LicenseLargeViewModel>(licenseId);

            var expected = this.context.Licenses
                .Where(l => l.Id == licenseId)
                .To<LicenseLargeViewModel>()
                .FirstOrDefault();

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task ChangeStatus_WithValidData_ShouldWorkCorrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var licenseId = licenses[0].Id;
            var model = LicenseTestsData.ChangeStatusModel;

            await licenseService.ChangeStatus(licenseId, model);

            Assert.True(this.context.Licenses.Any(l => l.Id == licenseId
                && l.Status == model.Status));
        }

        [Fact]
        public async Task UpdateLicense_WithValidData_ShouldWorkCorrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var licenseId = licenses[0].Id;
            var model = LicenseTestsData.UpdateModel;

            await licenseService.Update(licenseId, model);

            Assert.True(this.context.Licenses.Any(l => l.Id == licenseId
                && l.Name == model.Name));
            Assert.True(this.context.Licenses.Any(l => l.Id == licenseId
              && l.Status == LicenseStatus.WaitToBeView));
        }

        [Fact]
        public async Task CreateLicenseFile_WithValidData_ShouldWorkCorrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var model = LicenseTestsData.CreateFileModel;
            var licenseId = licenses[0].Id;
            model.LicensеId = licenseId;
            await licenseService.CreateFile(model);

            Assert.True(this.context.LicenseFiles.Any(lf => lf.LicensеId == model.LicensеId
                && lf.FileUrl == model.FileUrl));
        }

        [Fact]
        public async Task DeleteLicenseFile_WithValidData_ShouldWorkCorrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var licenseFileId = this.context.LicenseFiles.FirstOrDefault().Id;

            await licenseService.DeleteFile(licenseFileId);

            Assert.False(this.context.LicenseFiles.Any(lf => lf.Id == licenseFileId));
        }

        [Fact]
        public async Task AllLicenseFile_WithValidData_ShouldWorkCorrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var licenseId = this.context.LicenseFiles.FirstOrDefault().Id;

            var expected = this.context.LicenseFiles
                .Where(lf => lf.LicensеId == licenseId)
                .OrderBy(lf => lf.CreatedOn)
                .To<LicenceFileViewModel>()
                .ToList();

            var actual = licenseService.AllFiles<LicenceFileViewModel>(licenseId).ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        private IQueryable<License> FilteringLicenses(LicenseFilter filter, IQueryable<License> licenses)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                licenses = licenses.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                licenses = licenses.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                licenses = licenses.Where(s => s.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return licenses;
        }

        private List<LicenseFile> GenerateLicenseFiles(List<License> licenses)
        {
            var result = new List<LicenseFile>();
            var random = new Random();
            var count = 1;
            const int maxLicenseFileCount = 10;
            foreach (var license in licenses)
            {
                var countLicenseFiles = random.Next(1, maxLicenseFileCount - 1);
                if (countLicenseFiles > maxLicenseFileCount * 0.4)
                {
                    for (int i = 0; i < countLicenseFiles; i++)
                    {
                        var licenseFile = new LicenseFile()
                        {
                            Id = "LicenseFileId" + count,
                            FileUrl = "LicenseFileUrl",
                            LicensеId = license.Id,
                        };

                        result.Add(licenseFile);
                        count++;
                    }
                }
            }

            return result;
        }

        private IQueryable<License> OrederSearchMusics(OrderMethod orderMethod, IQueryable<License> licenses)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                licenses = licenses.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                licenses = licenses.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                licenses = licenses.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                licenses = licenses.OrderByDescending(s => s.Name);
            }

            return licenses;
        }

        private int GetLicensesPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private void CheckFilterResult(int page, LicensePageModel<LicenseLargeViewModel> actual, LicenseService licenseService, LicenseFilter filter, string userId = null)
        {
            IQueryable<License> licenses = this.context.Licenses;
            if (!string.IsNullOrEmpty(userId))
            {
                licenses = licenses.Where(s => s.UserId == userId);
            }

            licenses = this.FilteringLicenses(filter, licenses);

            licenses = this.OrederSearchMusics(filter.OrderMethod, licenses);
            var expected = new LicensePageModel<LicenseLargeViewModel>()
            {
                Licenses = licenses
                        .To<LicenseLargeViewModel>()
                        .GetPage(page, this.EntitesPerPage)
                        .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLicensesPagesCount(licenses.Count(), this.EntitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private async Task<LicenseService> CreateLicenseService(List<License> licenses)
        {
            var users = LicenseTestsData.Users;
            var licenseFiles = GenerateLicenseFiles(licenses);
            await this.context.Users.AddRangeAsync(users);
            await this.context.Licenses.AddRangeAsync(licenses);
            await this.context.LicenseFiles.AddRangeAsync(licenseFiles);
            await this.context.SaveChangesAsync();
            var service = new LicenseService(this.context, new PaginationService());

            return service;
        }
    }
}
