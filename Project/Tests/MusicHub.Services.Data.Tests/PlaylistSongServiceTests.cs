using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
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
using MusicHub.Web.ViewModels.SongModels;

namespace MusicHub.Services.Data.Tests
{
    public class PlaylistSongServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePlaylistSong_WithValidData_ShouldWorkCorrect()
        {
            var PlaylistSongService = await this.CreatePlaylistSongService(new List<PlaylistSong>());
            var model = PlaylistSongTestsData.CreateModel;

            var musicsInPlaylist = this.context.PlaylistSongs
              .Where(pm => pm.PlaylistId == model.PlaylistId);

            await PlaylistSongService.Create(model);

            Assert.True(this.context.PlaylistSongs.Any(pm => pm.SongId == model.SongId
                && pm.PlaylistId == model.PlaylistId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllMusic_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var PlaylistSongService = await this.CreatePlaylistSongService(PlaylistSongTestsData.PlaylistSongs());
            var id = PlaylistSongTestsData.PlaylistId;
            var expextedResult = this.context.PlaylistSongs
              .Where(p => p.PlaylistId == id)
              .ToList();
            var entitesPerPage = PaginationData.PlaylistsSongsPerPage;
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<PlaylistSongViewModel>()
               .ToList();

            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);
           
            var result = PlaylistSongService.All<PlaylistSongViewModel>(page, entitesPerPage, id);

            CheckPlaylistSongViewModelsIsEqual(page, expectedMusicsList, result, countPages);
        }

        [Fact]
        public async Task DeletePlaylistSong_WithValidData_ShouldWorkCorrect()
        {
            var PlaylistSongService = await this.CreatePlaylistSongService(PlaylistSongTestsData.PlaylistSongs());
            var PlaylistSongId = PlaylistSongTestsData.DeletePlaylistSongId;

            await PlaylistSongService.Delete(PlaylistSongId);

            Assert.False(this.context.PlaylistSongs.Any(pm => pm.Id == PlaylistSongId));
        }

        private static void CheckPlaylistSongViewModelsIsEqual(int page, List<PlaylistSongViewModel> expextedResult, PlaylistSongsAllViewModel<PlaylistSongViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultMusics = result.PlaylistSongs.ToList();
            Assert.Equal(resultMusics.Count, expextedResult.Count);

            for (int i = 0; i < expextedResult.Count(); i++)
            {
                var expectedMusic = expextedResult[i];
                var resultMusic = resultMusics[i];
                Assert.True(CheckPlaylistSongViewModelIsEqual(expectedMusic, resultMusic));
            }
        }

        private static void CheckPlaylistSongsIsEqual(List<PlaylistSong> expextedResult,
            List<PlaylistSong> result)
        {
            Assert.Equal(expextedResult.Count(), result.Count());

            for (int i = 0; i < expextedResult.Count(); i++)
            {
                var expectedPlaylistSong = expextedResult[i];
                var resultPlaylistSong = result[i];
                Assert.True(CheckPlaylistSongModelsIsEqual(expectedPlaylistSong, resultPlaylistSong));
            }
        }

        private static bool CheckPlaylistSongModelsIsEqual(PlaylistSong expectedPlaylistSong, PlaylistSong resultPlaylistSong)
           => expectedPlaylistSong.Id == resultPlaylistSong.Id
                && expectedPlaylistSong.SongId == resultPlaylistSong.SongId
                && expectedPlaylistSong.PlaylistId == resultPlaylistSong.PlaylistId
                && expectedPlaylistSong.CreatedOn == resultPlaylistSong.CreatedOn;

        private static bool CheckPlaylistSongViewModelIsEqual(PlaylistSongViewModel expectedMusic, PlaylistSongViewModel resultMusic)
           => expectedMusic.Id == resultMusic.Id
              && CheckMusicViewModelIsEqual(expectedMusic.Song, resultMusic.Song);

        private static bool CheckMusicViewModelIsEqual(SongViewModel expectedMusic, SongViewModel resultMusic)
            => expectedMusic.Name == resultMusic.Name
               && expectedMusic.VisibleStatus == resultMusic.VisibleStatus
               && expectedMusic.MusicLicenseType == resultMusic.MusicLicenseType
               && expectedMusic.UserUserName == resultMusic.UserUserName;

        private int GetMusicsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<PlaylistSongService> CreatePlaylistSongService(List<PlaylistSong> PlaylistSongs)
        {
            await this.context.Users.AddRangeAsync(PlaylistSongTestsData.Users);
            await this.context.Categories.AddRangeAsync(PlaylistSongTestsData.Categories);
            await this.context.Playlists.AddRangeAsync(PlaylistSongTestsData.Playlists);
            await this.context.Songs.AddRangeAsync(PlaylistSongTestsData.Musics);
            await this.context.PlaylistSongs.AddRangeAsync(PlaylistSongs);
            await this.context.SaveChangesAsync();
            var service = new PlaylistSongService(this.context, new PaginationService());

            return service;
        }
    }
}
