namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.PlayLaterMusicModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class PlayLaterMusicServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePlayLaterMusic_WithValidData_ShouldWorkCorrect()
        {
            var playLaterMusicService = await this.CreateService(new List<PlayLaterMusic>());
            var model = PlayLaterMusicTestsData.CreateModel;
            var userId = MusicTestsData.CreateUserId;

            await playLaterMusicService.Create(model, userId);

            Assert.True(this.context.PlayLaterMusics.Any(pm => pm.MusicId == model.MusicId
                && pm.UserId == userId));
        }

        [Fact]
        public async Task DeletePlayLaterMusic_WithValidData_ShouldWorkCorrect()
        {
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var playLaterMusicService = await this.CreateService(playLaterMusics);
            var playLaterMusicId = playLaterMusics[0].Id;

            await playLaterMusicService.Delete(playLaterMusicId);

            Assert.False(this.context.PlayLaterMusics.Any(c => c.Id == playLaterMusicId));
        }

        [Fact]
        public async Task UpdatePlayLaterMusic_WithValidData_ShouldWorkCorrect()
        {
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var playLaterMusicService = await this.CreateService(playLaterMusics);
            var playLaterMusicId = playLaterMusics[0].Id;
            var playLaterMusicNewPosition = PlayLaterMusicTestsData.UpdateFirstPositionNewPosition(playLaterMusics, playLaterMusicId);
            var model = new PlayLaterMusicEditInputModel()
            {
                Position = playLaterMusicNewPosition,
            };

            var expected = this.ChangePlayLaterMusicPostion(playLaterMusicId, playLaterMusicNewPosition);
            await playLaterMusicService.Update(playLaterMusicId, model);
            var playLaterMusic = this.context.PlayLaterMusics.Find(playLaterMusicId);
            var userId = playLaterMusic.UserId;
            var actual = this.context.PlayLaterMusics
                .Where(pm => pm.UserId == userId)
                .ToList();
            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAll_WithValidData_ShouldWorkCorrect(int page)
        {
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var playLaterMusicService = await this.CreateService(playLaterMusics);
            var userId = playLaterMusics[0].UserId;
            var entitesPerPage = PaginationData.PlayLaterMusicsPerPage;
            var actual = playLaterMusicService.All<PlayLaterMusicViewModel>(page, entitesPerPage, userId);

            this.CheckFilterModel(page, userId, entitesPerPage, new PlayLaterMusicFilterInputModels(), actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Filter_WithValidData_ShouldWorkCorrect(int page)
        {
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var playLaterMusicService = await this.CreateService(playLaterMusics);
            var userId = playLaterMusics[0].UserId;
            IQueryable<PlayLaterMusic> expextedResult = this.context.PlayLaterMusics
                    .Where(s => s.UserId == userId)
                    .OrderByDescending(s => s.CreatedOn);
            var entitesPerPage = PaginationData.PlayLaterMusicsPerPage;
            var filter = PlayLaterMusicTestsData.FilterModel;
            var actual = playLaterMusicService.Filter<PlayLaterMusicViewModel>(page, entitesPerPage, filter, userId);

            this.CheckFilterModel(page, userId, entitesPerPage, filter, actual);
        }

        [Fact]
        public async Task IsOwnedPlayLaterMusic_WithValidData_ShouldeReturnTrue()
        {
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var playLaterMusicService = await this.CreateService(playLaterMusics);

            var userId = playLaterMusics[0].UserId;
            var playLaterMusicId = playLaterMusics[0].Id;

            Assert.True(playLaterMusicService.IsOwned(playLaterMusicId, userId));
        }

        [Fact]
        public async Task IsOwnedPlayLaterMusic_WithValidData_ShouldeReturnFalse()
        {
            var playLaterMusics = PlayLaterMusicTestsData.PlayLaterMusics();
            var playLaterMusicService = await this.CreateService(playLaterMusics);

            var userId = playLaterMusics[0].UserId;
            var playLaterMusicId = playLaterMusics
                .Where(pd => pd.UserId != userId)
                .FirstOrDefault()
                .Id;

            Assert.False(playLaterMusicService.IsOwned(playLaterMusicId, userId));
        }

        private void CheckFilterModel(int page, string userId, int entitesPerPage, PlayLaterMusicFilterInputModels filter, PlayLaterMusicPageModel<PlayLaterMusicViewModel> actual)
        {
            IQueryable<PlayLaterMusic> playLaterMusics = this.context.PlayLaterMusics
                .Where(plm => plm.UserId == userId)
                .OrderByDescending(plm => plm.Position);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                playLaterMusics = playLaterMusics
                    .Where(s => s.UserId == userId
                        && s.Music.Name.Contains(filter.Name));
            }

            var playLaterMusicT = playLaterMusics.To<PlayLaterMusicViewModel>();
            var expected = new PlayLaterMusicPageModel<PlayLaterMusicViewModel>()
            {
                PlayLaterMusics = playLaterMusicT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlayLaterMusicsPagesCount(playLaterMusicT.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private int GetPlayLaterMusicsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private List<PlayLaterMusic> ChangePlayLaterMusicPostion(string playLaterMusicId, int playLaterMusicNewPosition)
        {
            var playLaterMusic = this.context.PlayLaterMusics.Find(playLaterMusicId);
            var userId = playLaterMusic.UserId;
            var oldPosition = playLaterMusic.Position;
            var playLaterMusics = this.context.PlayLaterMusics
                .Where(pm => pm.UserId == userId)
                .ToList();

            if (oldPosition >= playLaterMusicNewPosition)
            {
                for (int i = playLaterMusicNewPosition + 1; i < oldPosition; i++)
                {
                    playLaterMusics[i].Position++;
                }
            }
            else
            {
                for (int i = oldPosition + 1; i < playLaterMusicNewPosition; i++)
                {
                    playLaterMusics[i].Position--;
                }
            }

            playLaterMusics
                .Where(pm => pm.Id == playLaterMusicId)
                .FirstOrDefault()
                .Position = playLaterMusicNewPosition;
            return playLaterMusics;
        }

        private async Task<PlayLaterMusicService> CreateService(List<PlayLaterMusic> playLaterMusic)
        {
            var users = PlayLaterMusicTestsData.Users;
            var categories = PlayLaterMusicTestsData.Categories;
            var musics = PlayLaterMusicTestsData.Musics;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Musics.AddRangeAsync(musics);
            await this.context.PlayLaterMusics.AddRangeAsync(playLaterMusic);
            await this.context.MusicLicenses.AddRangeAsync(new List<MusicLicense>());
            await this.context.SaveChangesAsync();
            var service = new PlayLaterMusicService(this.context, new PaginationService());

            return service;
        }
    }
}
