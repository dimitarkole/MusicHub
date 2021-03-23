namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.LicenseModels;
    using MusicHub.Web.ViewModels.LicenseMusicModels;
    using Xunit;

    public class LicenseMusicServiceTests : BaseTestClass
    {

        [Fact]
        public async Task CreateMusicLicense_WithValidData_ShouldWorkCorrect()
        {
            var musicLicenseService = await this.CreateLicenseMusicService(new List<MusicLicense>());
            var model = new LicenseMusicCreateModel()
            {
                MusicId = this.context.Musics.FirstOrDefault().Id,
                LicenseId = this.context.Licenses.FirstOrDefault().Id,
            };

            var userId = LicenseMusicTestsData.CreateUserId;

            await musicLicenseService.Create(model);

            Assert.True(this.context.MusicLicenses.Any(ml => ml.LicenseId == model.LicenseId
                && ml.MusicId == model.MusicId));
        }

        [Fact]
        public async Task DeleteMusicLicense_WithValidData_ShouldWorkCorrect()
        {
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicLicenseService = await this.CreateLicenseMusicService(LicenseMusicTestsData.MusicLicenses());

            var musicLicensesId = musicLicenses[0].Id;

            await musicLicenseService.Delete(musicLicensesId);

            Assert.False(this.context.MusicLicenses.Any(ml => ml.Id == musicLicensesId));
        }

        [Fact]
        public async Task DeleteAllMusicLicense_WithValidData_ShouldWorkCorrect()
        {
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicLicenseService = await this.CreateLicenseMusicService(LicenseMusicTestsData.MusicLicenses());

            var musicId = musicLicenses[0].MusicId;

            await musicLicenseService.DeleteAllMusicLicenses(musicId);

            Assert.False(this.context.MusicLicenses.Any(ml => ml.MusicId == musicId));
        }

        [Fact]
        public async Task GetAllMusicLicenses_WithValidData_ShouldeWorkCurrect()
        {
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicLicenseService = await this.CreateLicenseMusicService(LicenseMusicTestsData.MusicLicenses());
            var musicId = musicLicenses[0].MusicId;

            var expectedResult = this.context.MusicLicenses
                    .Where(ml => ml.MusicId == musicId)
                    .To<LicenseMusicViewModel>()
                    .ToList();

            var result = musicLicenseService.All<LicenseMusicViewModel>(musicId);

            this.CheckMusicLicenseViewModelsIsEqual(expectedResult, result);
        }

        private void CheckMusicLicenseViewModelsIsEqual(List<LicenseMusicViewModel> expectedResult, List<LicenseMusicViewModel> result)
        {
            Assert.Equal(expectedResult.Count, result.Count());
            for (int i = 0; i < expectedResult.Count(); i++)
            {
                var expectedMusicLicense = expectedResult[i];
                var resultMusicLicense = result[i];
                Assert.True(this.CheckNotificationViewModelIsEqual(expectedMusicLicense, resultMusicLicense));
            }
        }

        private bool CheckNotificationViewModelIsEqual(LicenseMusicViewModel expectedMusicLicense, LicenseMusicViewModel resultMusicLicense)
            => expectedMusicLicense.Id == resultMusicLicense.Id
                && this.CheckLicenseViewModelIsEqual(expectedMusicLicense.License, resultMusicLicense.License);

        private bool CheckLicenseViewModelIsEqual(LicenceViewModel expected, LicenceViewModel result)
           => expected.CreatedOn == result.CreatedOn
               && expected.Id == result.Id
               && expected.Name == result.Name
               && expected.Status == result.Status;

        private async Task<LicenseMusicService> CreateLicenseMusicService(List<MusicLicense> musicLicenses)
        {
            var users = LicenseMusicTestsData.Users;
            var categories = LicenseMusicTestsData.Categories;
            var musics = LicenseMusicTestsData.Musics;
            var licenses = LicenseMusicTestsData.Licenses;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Licenses.AddRangeAsync(licenses);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Musics.AddRangeAsync(musics);
            await this.context.MusicLicenses.AddRangeAsync(musicLicenses);
            await this.context.SaveChangesAsync();
            var service = new LicenseMusicService(this.context);// , new PaginationService());

            return service;
        }
    }
}
