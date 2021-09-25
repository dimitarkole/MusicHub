using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.ViewModels.PlayedMusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Common.Extensions;
using MusicHub.Services.Mapping;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Web.ViewModels.MusicModels;

namespace MusicHub.Services.Data.Tests
{
    public class PlayedMusicServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePlayedMusic_WithValidData_ShouldWorkCorrect()
        {
            var playdMusicService = await this.CreatePlayedMusicService(new List<PlayedMusic>());
            var model = PlayedMusicTestsData.CreateModel;
            var userId = MusicTestsData.CreateUserId;

            await playdMusicService.Create(model, userId);

            Assert.True(this.context.PlayedMusics.Any(pm => pm.MusicId == model.MusicId
                && pm.UserId == userId));
        }

        [Fact]
        public async Task DeletePlayedMusic_WithValidData_ShouldWorkCorrect()
        {
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var playedMusicService = await this.CreatePlayedMusicService(playedMusics);
            var playedMusicId = playedMusics[0].Id;

            await playedMusicService.Delete(playedMusicId);

            Assert.False(this.context.PlayedMusics.Any(c => c.Id == playedMusicId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAll_WithValidData_ShouldWorkCorrect(int page)
        {
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var playedMusicService = await this.CreatePlayedMusicService(playedMusics);
            var userId = playedMusics[0].UserId;
            var expextedResult = this.context.PlayedMusics
                    .Where(s => s.UserId == userId)
                    .OrderByDescending(s => s.CreatedOn)
                    .To<PlayedMusicViewModel>();
            var entitesPerPage = PaginationData.PlayedMusicsPerPage;

            var result = playedMusicService.All<PlayedMusicViewModel>(page, entitesPerPage, userId);

            var expectedCountPages = this.GetPlayedMusicsPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedPlayedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            this.CheckPlayedMusicViewModelsIsEqual(page, expectedCountPages, expectedPlayedMusicsList, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Filter_WithValidData_ShouldWorkCorrect(int page)
        {
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var playedMusicService = await this.CreatePlayedMusicService(playedMusics);
            var userId = playedMusics[0].UserId;
            IQueryable<PlayedMusic> expextedResult = this.context.PlayedMusics
                    .Where(s => s.UserId == userId)
                    .OrderByDescending(s => s.CreatedOn);
            var entitesPerPage = PaginationData.PlayedMusicsPerPage;
            var filter = PlayedMusicTestsData.filterModel;
            if (!string.IsNullOrEmpty(filter.Name))
            {
                expextedResult = expextedResult.Where(s => s.UserId == userId
                        && s.Music.Name.Contains(filter.Name));
            }

            var expextedResultT = expextedResult
                    .To<PlayedMusicViewModel>();
            var expectedCountPages = this.GetPlayedMusicsPagesCount(expextedResultT.Count(), entitesPerPage);
            var expectedPlayedMusicsList = expextedResultT.GetPage(page, entitesPerPage)
               .ToList();

            var result = playedMusicService.Filter<PlayedMusicViewModel>(page, entitesPerPage, filter, userId);

            this.CheckPlayedMusicViewModelsIsEqual(page, expectedCountPages, expectedPlayedMusicsList, result);
        }

        [Fact]
        public async Task IsOwnedPlayedMusic_WithValidData_ShouldeReturnTrue()
        {
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var playedMusicService = await this.CreatePlayedMusicService(playedMusics);

            var userId = playedMusics[0].UserId;
            var playedMusicId = playedMusics[0].Id;

            Assert.True(playedMusicService.IsOwned(playedMusicId, userId));
        }

        [Fact]
        public async Task IsOwnedPlayedMusic_WithValidData_ShouldeReturnFalse()
        {
            var playedMusics = PlayedMusicTestsData.PlayedMusics();
            var playedMusicService = await this.CreatePlayedMusicService(playedMusics);

            var userId = playedMusics[0].UserId;
            var playedMusicId = playedMusics
                .Where(pd => pd.UserId != userId)
                .FirstOrDefault()
                .Id;

            Assert.False(playedMusicService.IsOwned(playedMusicId, userId));
        }

        private void CheckPlayedMusicViewModelsIsEqual(int page, int expectedCountPages, List<PlayedMusicViewModel> expectedPlayedMusics, PlayedMusicAllViewModel<PlayedMusicViewModel> result)
        {
            Assert.Equal(page, result.CurrentPage);
            Assert.Equal(expectedCountPages, result.NumberOfPages);
            var resultPlayedMusics = result.PlayedMusic.ToList();
            Assert.Equal(expectedPlayedMusics.Count, resultPlayedMusics.Count);

            for (int i = 0; i < resultPlayedMusics.Count(); i++)
            {
                var expectedPlayedMusic = expectedPlayedMusics[i];
                var resultPlayedMusic = resultPlayedMusics[i];
                Assert.True(this.CheckPlayedMusicViewModelIsEqual(expectedPlayedMusic, resultPlayedMusic));
            }
        }

        private bool CheckPlayedMusicViewModelIsEqual(PlayedMusicViewModel expectedPlayedMusic, PlayedMusicViewModel resultPlayedMusic)
            => expectedPlayedMusic.Id == resultPlayedMusic.Id
                && expectedPlayedMusic.CreatedOn == resultPlayedMusic.CreatedOn
                && CheckMusicViewModelIsEqual(expectedPlayedMusic.Music, resultPlayedMusic.Music);

        private bool CheckMusicViewModelIsEqual(MusicViewModel expectedMusic, MusicViewModel resultMusic)
            => expectedMusic.Name == resultMusic.Name
               && expectedMusic.Category.Name == resultMusic.Category.Name
               && expectedMusic.Category.Id == resultMusic.Category.Id
               && expectedMusic.ImageUrl == resultMusic.ImageUrl
               && expectedMusic.AudioUrl == resultMusic.AudioUrl
               && expectedMusic.UserUserName == resultMusic.UserUserName;

        private int GetPlayedMusicsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<PlayedMusicService> CreatePlayedMusicService(List<PlayedMusic> playedMusics)
        {
            var users = PlayedMusicTestsData.Users;
            var categories = PlayedMusicTestsData.Categories;
            var musics = PlayedMusicTestsData.Musics;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Musics.AddRangeAsync(musics);
            await this.context.PlayedMusics.AddRangeAsync(playedMusics);
            await this.context.MusicLicenses.AddRangeAsync(new List<MusicLicense>());
            await this.context.SaveChangesAsync();
            var service = new PlayedMusicService(this.context, new PaginationService());

            return service;
        }
    }
}
