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
using FluentAssertions;

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
            var entitesPerPage = PaginationData.PlayedMusicsPerPage;
            var actual = playedMusicService.All<PlayedMusicViewModel>(page, entitesPerPage, userId);

            this.CheckFilterModel(page, userId, entitesPerPage, new PlayedMusicFilterInputModels(), actual);
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
            var actual = playedMusicService.Filter<PlayedMusicViewModel>(page, entitesPerPage, filter, userId);

            this.CheckFilterModel(page, userId, entitesPerPage, filter, actual);
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

        private void CheckFilterModel(int page, string userId, int entitesPerPage, PlayedMusicFilterInputModels filter, PlayedMusicAllViewModel<PlayedMusicViewModel> actual)
        {
            IQueryable<PlayedMusic> playedMusics = this.context.PlayedMusics
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedOn);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                playedMusics = playedMusics
                    .Where(s => s.UserId == userId
                        && s.Music.Name.Contains(filter.Name));
            }

            var playedMusicT = playedMusics.To<PlayedMusicViewModel>();
            var expected = new PlayedMusicAllViewModel<PlayedMusicViewModel>()
            {
                PlayedMusic = playedMusicT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlayedMusicsPagesCount(playedMusicT.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

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
