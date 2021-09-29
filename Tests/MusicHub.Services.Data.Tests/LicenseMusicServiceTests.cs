namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
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

            var expected = this.context.MusicLicenses
                    .Where(ml => ml.MusicId == musicId)
                    .To<LicenseMusicViewModel>()
                    .ToList();

            var actual = musicLicenseService.All<LicenseMusicViewModel>(musicId);

            expected.Should().BeEquivalentTo(actual);
        }

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
            var service = new LicenseMusicService(this.context);

            // , new PaginationService());
            return service;
        }
    }
}
