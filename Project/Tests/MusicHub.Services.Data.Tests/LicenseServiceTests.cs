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
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Services.Data.Tests
{
    public class LicenseServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateLicense_WithValidData_ShouldWorkCorrect()
        {
            var licenseService = await this.CreateLicenseService(new List<License>());
            var model = LicenseTestsData.CreateModel;
            var userId = LicenseTestsData.CreateUserId;

            await licenseService.Create(model, userId);

            Assert.True(this.context.Licenses.Any(l => l.Name == model.Name
                && l.UserId == userId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAll_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            var allLicenses = this.context.Licenses
                .OrderByDescending(l => l.Status == LicenseStatus.WaitToBeView)
                .ThenByDescending(l => l.CreatedOn);

            var entitesPerPage = PaginationData.LicensesPerPage;
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);
            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.All<LicenseLargeViewModel>(page, entitesPerPage);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetOwned_WithValidData_ShouldeReturnWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = licenses[0].UserId;
            var allUserLicenses = this.context.Licenses
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.CreatedOn)
                .To<LicenseLargeViewModel>();

            var entitesPerPage = PaginationData.LicensesPerPage;
            var countPages = this.GetLicensesPagesCount(allUserLicenses.Count(), entitesPerPage);
            var expectedResult = allUserLicenses
                .GetPage(page, entitesPerPage)
                .ToList();

            var result = licenseService.AllOwn<LicenseLargeViewModel>(page, entitesPerPage, userId);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Fact]
        public async Task AllOwnApproved_WithValidData_ShouldeWorkCurrect()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = licenses[0].UserId;
            var result = licenseService.AllOwnApproved<LicenseLargeViewModel>(userId)
                .ToList();
            var approvedLicenses = this.context.Licenses
                .Where(l => l.UserId == userId
                    && l.Status == Common.LicenseStatus.Approve)
                .OrderByDescending(l => l.Name)
                .To<LicenseLargeViewModel>()
                .ToList();

            Assert.Equal(approvedLicenses.Count, result.Count);
            for (int i = 0; i < approvedLicenses.Count(); i++)
            {
                var expectedLicense = approvedLicenses[i];
                var resultLicense = result[i];
                Assert.True(this.CheckLicenseViewModelIsEqual(expectedLicense, resultLicense));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilter_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            IQueryable<License> allLicenses = this.context.Licenses;

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilter;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserName_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            IQueryable<License> allLicenses = this.context.Licenses;

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilterWithUserName;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            IQueryable<License> allLicenses = this.context.Licenses;

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilterWithStatus;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserNameAndStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            IQueryable<License> allLicenses = this.context.Licenses;

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilterWithUserNameAndStatus;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserId_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            IQueryable<License> allLicenses = this.context.Licenses;

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilterWithUserId;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task LicenseFilterWithUserIdAndStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            IQueryable<License> allLicenses = this.context.Licenses;

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilterWithUserIdAndStatus;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task OwnedLicenseFilter_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = LicenseTestsData.LicenseFilterUserId;
            IQueryable<License> allLicenses = this.context.Licenses
                .Where(l => l.UserId == userId);

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilter;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter, userId);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task OwnedLicenseFilterWithStatus_WithValidData_ShouldeWorkCurrect(int page)
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);
            var userId = LicenseTestsData.LicenseFilterUserId;
            IQueryable<License> allLicenses = this.context.Licenses
                .Where(l => l.UserId == userId);

            var entitesPerPage = PaginationData.LicensesPerPage;
            var filter = LicenseTestsData.LicenseFilterWithStatus;
            allLicenses = this.FilteringLicenses(filter, allLicenses);

            allLicenses = this.OrederSearchMusics(filter.OrderMethod, allLicenses);
            var countPages = this.GetLicensesPagesCount(allLicenses.Count(), entitesPerPage);

            var expectedResult = allLicenses
                .GetPage(page, entitesPerPage)
                .To<LicenseLargeViewModel>()
                .ToList();

            var result = licenseService.Filter<LicenseLargeViewModel>(page, entitesPerPage, filter, userId);

            this.ChecLicenseAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Fact]
        public async Task IsOwnedLicense_WithValidData_ShouldeReturnTrue()
        {
            var licenses = LicenseTestsData.Licenses();
            var licenseService = await this.CreateLicenseService(licenses);

            var userId = licenses[0].UserId;
            var licenseId = licenses[0].Id;

            Assert.True(licenseService.IsOwn(licenseId, userId));
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

            Assert.False(licenseService.IsOwn(licenseId, userId));
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

            var result = licenseService.GetById<LicenseLargeViewModel>(licenseId);

            var expectedResult = this.context.Licenses
                .Where(l => l.Id == licenseId)
                .To<LicenseLargeViewModel>()
                .FirstOrDefault();

            Assert.True(this.CheckLicenseViewModelIsEqual(expectedResult, result));
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
                && lf.Path == model.Path));
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

        private void ChecLicenseAllViewModelsIsEqual(int page, int countPages, List<LicenseLargeViewModel> expectedResult, LicenseAllViewModel<LicenseLargeViewModel> result)
        {
            Assert.Equal(page, result.CurrentPage);
            Assert.Equal(countPages, result.NumberOfPages);
            var resultLicenses = result.Licenses.ToList();
            Assert.Equal(expectedResult.Count, resultLicenses.Count());

            for (int i = 0; i < expectedResult.Count(); i++)
            {
                var expectedLicense = expectedResult[i];
                var resultLicense = resultLicenses[i];
                Assert.True(this.CheckLicenseViewModelIsEqual(expectedLicense, resultLicense));
            }
        }

        private bool CheckLicenseViewModelIsEqual(LicenseLargeViewModel expectedLicense, LicenseLargeViewModel resultLicense)
            => expectedLicense.CreatedOn == resultLicense.CreatedOn
                && expectedLicense.Id == resultLicense.Id
                && expectedLicense.Name == resultLicense.Name
                && expectedLicense.Status == resultLicense.Status
                // && this.CheckLicenseFilesViewModelIsEqual(expectedLicense.LicenseFiles, resultLicense.LicenseFiles)
                && this.CheckLicenseUserViewModelIsEqual(expectedLicense.User, expectedLicense.User);

        private bool CheckLicenseFilesViewModelIsEqual(List<LicenceFileViewModel> expectedLicenseFiles, List<LicenceFileViewModel> resultLicenseFiles)
        {
            if (expectedLicenseFiles.Count != resultLicenseFiles.Count)
            {
                return false;
            }

            for (int i = 0; i < expectedLicenseFiles.Count; i++)
            {
                var expectedLicenseFile = expectedLicenseFiles[i];
                var resultLicenseFile = resultLicenseFiles[i];
                var result = this.CheckLicenseFileViewModelIsEqual(expectedLicenseFile, resultLicenseFile);
                if (!result)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckLicenseUserViewModelIsEqual(LicenseUserViewModel expectedUser, LicenseUserViewModel resultUser)
            => expectedUser.Id == resultUser.Id
                && expectedUser.ImageUrl == resultUser.ImageUrl
                && expectedUser.UserName == resultUser.UserName;

        private bool CheckLicenseFileViewModelIsEqual(LicenceFileViewModel expectedFile, LicenceFileViewModel resultFile)
            => expectedFile.Id == resultFile.Id
                && expectedFile.Path == resultFile.Path
                && expectedFile.CreatedOn == resultFile.CreatedOn;

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
                            Path = "LicenseFileUrl",
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
