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
using static MusicHub.Common.GlobalConstants;
using MusicHub.Web.ViewModels.SongViewHistoryModels;
using MusicHub.Web.ViewModels.SongModels;

namespace MusicHub.Services.Data.Tests
{
    public class PlayedMusicServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePlayedMusic_WithValidData_ShouldWorkCorrect()
        {
            var playdMusicService = await this.CreatePlayedMusicService(new List<SongViewHistory>());
            var userId = PlayedMusicTestsData.CreateUserId;
            var model = PlayedMusicTestsData.CreateSongId;

            await playdMusicService.Create(model, userId);

            Assert.True(this.context.SongViewHistories.Any(pm => pm.SongId == model.SongId
                && pm.UserId == userId));
        }

        [Fact]
        public async Task DeletePlayedMusic_WithValidData_ShouldWorkCorrect()
        {
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var playedMusicService = await this.CreatePlayedMusicService(playedMusics);
            var playedMusicId = playedMusics[0].Id;

            await playedMusicService.Delete(playedMusicId);

            Assert.False(this.context.SongViewHistories.Any(c => c.Id == playedMusicId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAll_WithValidData_ShouldWorkCorrect(int page)
        {
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var playedMusicService = await this.CreatePlayedMusicService(playedMusics);
            var userId = playedMusics[0].UserId;
            var expextedResult = this.context.SongViewHistories
                    .Where(s => s.UserId == userId)
                    .OrderByDescending(s => s.CreatedOn)
                    .To<SongViewHistoryViewModelModels>();
            var entitesPerPage = PaginationData.SongsViewHistoryPerPage;

            var result = playedMusicService.All<SongViewHistoryViewModelModels>(page, entitesPerPage, userId);

            var expectedCountPages = this.GetPlayedMusicsPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedPlayedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            this.CheckPlayedMusicViewModelsIsEqual(page, expectedCountPages, expectedPlayedMusicsList, result);
        }

        private void CheckPlayedMusicViewModelsIsEqual(int page, int expectedCountPages, List<SongViewHistoryViewModelModels> expectedPlayedMusics, SongViewHistoryAllViewModel<SongViewHistoryViewModelModels> result)
        {
            Assert.Equal(page, result.CurrentPage);
            Assert.Equal(expectedCountPages, result.NumberOfPages);
            var resultPlayedMusics = result.SongViewHistory.ToList();
            Assert.Equal(expectedPlayedMusics.Count, resultPlayedMusics.Count);

            for (int i = 0; i < resultPlayedMusics.Count(); i++)
            {
                var expectedPlayedMusic = expectedPlayedMusics[i];
                var resultPlayedMusic = resultPlayedMusics[i];
                Assert.True(this.CheckPlayedMusicViewModelIsEqual(expectedPlayedMusic, resultPlayedMusic));
            }
        }

        private bool CheckPlayedMusicViewModelIsEqual(SongViewHistoryViewModelModels expectedPlayedMusic, SongViewHistoryViewModelModels resultPlayedMusic)
            => expectedPlayedMusic.Id == resultPlayedMusic.Id
                && expectedPlayedMusic.CreatedOn == resultPlayedMusic.CreatedOn
                && CheckMusicViewModelIsEqual(expectedPlayedMusic.Song, resultPlayedMusic.Song);

        private bool CheckMusicViewModelIsEqual(SongViewModel expectedMusic, SongViewModel resultMusic)
            => expectedMusic.Name == resultMusic.Name
               && expectedMusic.VisibleStatus == resultMusic.VisibleStatus
               && expectedMusic.MusicLicenseType == resultMusic.MusicLicenseType
               && expectedMusic.UserUserName == resultMusic.UserUserName;

        private int GetPlayedMusicsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<SongViewHistoryService> CreatePlayedMusicService(List<SongViewHistory> playedMusics)
        {
            var users = PlayedMusicTestsData.Users;
            var categories = PlayedMusicTestsData.Categories;
            var musics = PlayedMusicTestsData.Musics;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Songs.AddRangeAsync(musics);
            await this.context.SongViewHistories.AddRangeAsync(playedMusics);
            await this.context.SongLicenses.AddRangeAsync(new List<SongLicense>());
            await this.context.SaveChangesAsync();
            var service = new SongViewHistoryService(this.context, new PaginationService());

            return service;
        }
    }
}
