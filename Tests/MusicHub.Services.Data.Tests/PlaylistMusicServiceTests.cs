using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.ViewModels.MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using MusicHub.Web.ViewModels.PlaylistModels;

namespace MusicHub.Services.Data.Tests
{
    public class PlaylistMusicServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePlaylistMusic_WithValidData_ShouldWorkCorrect()
        {
            var playlistMusicService = await this.CreatePlaylistMusicService(new List<PlaylistMusic>());
            var model = PlaylistMusicTestsData.CreateModel;

            var musicsInPlaylist = this.context.PlaylistMusics
              .Where(pm => pm.PlaylistId == model.PlaylistId);
            var expectedPosition = musicsInPlaylist.Count() == 0 ? 0 : musicsInPlaylist.Max(pm => pm.Position);

            await playlistMusicService.Create(model);

            Assert.True(this.context.PlaylistMusics.Any(pm => pm.MusicId == model.MusicId
                && pm.PlaylistId == model.PlaylistId
                && pm.Position == expectedPosition));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllMusic_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var playlistMusicService = await this.CreatePlaylistMusicService(PlaylistMusicTestsData.PlaylistMusics());
            var id = PlaylistMusicTestsData.PlaylistId;
            var expextedResult = this.context.PlaylistMusics
              .Where(p => p.PlaylistId == id)
              .OrderByDescending(pm => pm.Position)
              .To<PlaylistMusicViewModel>();

            var entitesPerPage = PaginationData.PlaylistsMusicsPerPage;
            var result = playlistMusicService.All<PlaylistMusicViewModel>(page, entitesPerPage, id);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            CheckPlaylistMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages);
        }

        [Fact]
        public async Task DeletePlaylistMusic_WithValidData_ShouldWorkCorrect()
        {
            var playlistMusicService = await this.CreatePlaylistMusicService(PlaylistMusicTestsData.PlaylistMusics());
            var playlistMusicId = PlaylistMusicTestsData.DeletePlaylistMusicId;

            await playlistMusicService.Delete(playlistMusicId);

            Assert.False(this.context.PlayedMusics.Any(pm => pm.Id == playlistMusicId));
        }

        [Fact]
        public async Task UpdateLastPosition_WithValidData_ShouldeReturnPublic()
        {
            var playlistMusicService = await this.CreatePlaylistMusicService(PlaylistMusicTestsData.PlaylistMusics());
            var playlistMusicId = PlaylistMusicTestsData.UpdateLastPositionPlaylistMusicId();
            var playlistMusicNewPosition = PlaylistMusicTestsData.UpdateLastPositionNewPosition();
            var model = new PlaylistMusicEditInputModel()
            {
                Position = playlistMusicNewPosition,
            };

            var expextedPlaylistMusicsPositions = this.ChangePlaylistMusicPostion(playlistMusicId, playlistMusicNewPosition);
            await playlistMusicService.Update(model, playlistMusicId);

            var playlistMusic = this.context.PlaylistMusics.Find(playlistMusicId);
            var playlistId = playlistMusic.PlaylistId;
            var result = this.context.PlaylistMusics
                .Where(pm => pm.PlaylistId == playlistId)
                .ToList();

            CheckPlaylistMusicsIsEqual(expextedPlaylistMusicsPositions, result);
        }

        [Fact]
        public async Task UpdateFirstPosition_WithValidData_ShouldeReturnPublic()
        {
            var playlistMusicService = await this.CreatePlaylistMusicService(PlaylistMusicTestsData.PlaylistMusics());
            var playlistMusicId = PlaylistMusicTestsData.UpdateFistPositionPlaylistMusicId;
            var playlistMusicNewPosition = PlaylistMusicTestsData.UpdateFirstPositionNewPosition();
            var model = new PlaylistMusicEditInputModel()
            {
                Position = playlistMusicNewPosition,
            };

            var expextedPlaylistMusicsPositions = this.ChangePlaylistMusicPostion(playlistMusicId, playlistMusicNewPosition);
            await playlistMusicService.Update(model, playlistMusicId);

            var playlistMusic = this.context.PlaylistMusics.Find(playlistMusicId);
            var playlistId = playlistMusic.PlaylistId;
            var result = this.context.PlaylistMusics
                .Where(pm => pm.PlaylistId == playlistId)
                .ToList();

            CheckPlaylistMusicsIsEqual(expextedPlaylistMusicsPositions, result);
        }

        private List<PlaylistMusic> ChangePlaylistMusicPostion(string playlistMusicId, int playlistMusicNewPosition)
        {
            var playlistMusic = this.context.PlaylistMusics.Find(playlistMusicId);
            var playlistId = playlistMusic.PlaylistId;
            var oldPosition = playlistMusic.Position;
            var playlistMusicsInPlaylist = this.context.PlaylistMusics
                .Where(pm => pm.PlaylistId == playlistId)
                .ToList();

            if (oldPosition >= playlistMusicNewPosition)
            {
                for (int i = playlistMusicNewPosition + 1; i < oldPosition; i++)
                {
                    playlistMusicsInPlaylist[i].Position++;
                }
            }
            else
            {
                for (int i = oldPosition + 1; i < playlistMusicNewPosition; i++)
                {
                    playlistMusicsInPlaylist[i].Position--;
                }
            }

            playlistMusicsInPlaylist
                .Where(pm => pm.Id == playlistMusicId)
                .FirstOrDefault()
                .Position = playlistMusicNewPosition;
            return playlistMusicsInPlaylist;
        }

        private static void CheckPlaylistMusicViewModelsIsEqual(int page, List<PlaylistMusicViewModel> expextedResult, PlaylistMusicsAllViewModel<PlaylistMusicViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultMusics = result.PlaylistMusics.ToList();
            Assert.Equal(resultMusics.Count, expextedResult.Count);

            for (int i = 0; i < expextedResult.Count(); i++)
            {
                var expectedMusic = expextedResult[i];
                var resultMusic = resultMusics[i];
                Assert.True(CheckPlaylistMusicViewModelIsEqual(expectedMusic, resultMusic));
            }
        }

        private static void CheckPlaylistMusicsIsEqual(List<PlaylistMusic> expextedResult,
            List<PlaylistMusic> result)
        {
            Assert.Equal(expextedResult.Count(), result.Count());

            for (int i = 0; i < expextedResult.Count(); i++)
            {
                var expectedPlaylistMusic = expextedResult[i];
                var resultPlaylistMusic = result[i];
                Assert.True(CheckPlaylistMusicModelsIsEqual(expectedPlaylistMusic, resultPlaylistMusic));
            }
        }

        private static bool CheckPlaylistMusicModelsIsEqual(PlaylistMusic expectedPlaylistMusic, PlaylistMusic resultPlaylistMusic)
           => expectedPlaylistMusic.Id == resultPlaylistMusic.Id
                && expectedPlaylistMusic.MusicId == resultPlaylistMusic.MusicId
                && expectedPlaylistMusic.PlaylistId == resultPlaylistMusic.PlaylistId
                && expectedPlaylistMusic.Position == resultPlaylistMusic.Position
                && expectedPlaylistMusic.CreatedOn == resultPlaylistMusic.CreatedOn;

        private static bool CheckPlaylistMusicViewModelIsEqual(PlaylistMusicViewModel expectedMusic, PlaylistMusicViewModel resultMusic)
           => expectedMusic.Id == resultMusic.Id
              && CheckMusicViewModelIsEqual(expectedMusic.Music, resultMusic.Music);

        private static bool CheckMusicViewModelIsEqual(MusicViewModel expectedMusic, MusicViewModel resultMusic)
            => expectedMusic.Name == resultMusic.Name
               && expectedMusic.Category.Name == resultMusic.Category.Name
               && expectedMusic.Category.Id == resultMusic.Category.Id
               && expectedMusic.ImageUrl == resultMusic.ImageUrl
               && expectedMusic.VisibleStatus == resultMusic.VisibleStatus
               && expectedMusic.MusicLicenseType == resultMusic.MusicLicenseType
               && expectedMusic.AudioUrl == resultMusic.AudioUrl
               && expectedMusic.UserUserName == resultMusic.UserUserName;

        private int GetMusicsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<PlaylistMusicService> CreatePlaylistMusicService(List<PlaylistMusic> playlistMusics)
        {
            await this.context.Users.AddRangeAsync(PlaylistMusicTestsData.Users);
            await this.context.Categories.AddRangeAsync(PlaylistMusicTestsData.Categories);
            await this.context.Playlists.AddRangeAsync(PlaylistMusicTestsData.Playlists);
            await this.context.Musics.AddRangeAsync(PlaylistMusicTestsData.Musics);
            await this.context.PlaylistMusics.AddRangeAsync(playlistMusics);
            await this.context.SaveChangesAsync();
            var service = new PlaylistMusicService(this.context, new PaginationService());

            return service;
        }
    }
}
