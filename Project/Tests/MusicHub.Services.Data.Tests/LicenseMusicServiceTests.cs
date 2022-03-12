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
            var musicLicenseService = await this.CreateLicenseMusicService(new List<SongLicense>());
            var model = new LicenseMusicCreateModel()
            {
                SongId = this.context.Songs.FirstOrDefault().Id,
                LicenseId = this.context.Licenses.FirstOrDefault().Id,
            };

            var userId = LicenseMusicTestsData.CreateUserId;

            await musicLicenseService.Create(model);

            Assert.True(this.context.SongLicenses.Any(ml => ml.LicenseId == model.LicenseId
                && ml.SongId == model.SongId));
        }

        [Fact]
        public async Task DeleteMusicLicense_WithValidData_ShouldWorkCorrect()
        {
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicLicenseService = await this.CreateLicenseMusicService(LicenseMusicTestsData.MusicLicenses());

            var musicLicensesId = musicLicenses[0].Id;

            await musicLicenseService.Delete(musicLicensesId);

            Assert.False(this.context.SongLicenses.Any(ml => ml.Id == musicLicensesId));
        }

        [Fact]
        public async Task DeleteAllMusicLicense_WithValidData_ShouldWorkCorrect()
        {
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicLicenseService = await this.CreateLicenseMusicService(LicenseMusicTestsData.MusicLicenses());

            var musicId = musicLicenses[0].SongId;

            await musicLicenseService.DeleteAllMusicLicenses(musicId);

            Assert.False(this.context.SongLicenses.Any(ml => ml.SongId == musicId));
        }

        [Fact]
        public async Task GetAllMusicLicenses_WithValidData_ShouldeWorkCurrect()
        {
            var musicLicenses = LicenseMusicTestsData.MusicLicenses();
            var musicLicenseService = await this.CreateLicenseMusicService(LicenseMusicTestsData.MusicLicenses());
            var musicId = musicLicenses[0].SongId;

            var expectedResult = this.context.SongLicenses
                    .Where(ml => ml.SongId == musicId)
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

        private async Task<LicenseMusicService> CreateLicenseMusicService(List<SongLicense> musicLicenses)
        {
            var users = LicenseMusicTestsData.Users;
            var categories = LicenseMusicTestsData.Categories;
            var musics = LicenseMusicTestsData.Musics;
            var licenses = LicenseMusicTestsData.Licenses;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Licenses.AddRangeAsync(licenses);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Songs.AddRangeAsync(musics);
            await this.context.SongLicenses.AddRangeAsync(musicLicenses);
            await this.context.SaveChangesAsync();
            var service = new LicenseMusicService(this.context);// , new PaginationService());

            return service;
        }
    }
}
