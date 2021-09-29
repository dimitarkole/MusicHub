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
    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

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
            var entitesPerPage = PaginationData.PlaylistsMusicsPerPage;

            var playlistMusics = this.context.PlaylistMusics
                .Where(p => p.PlaylistId == id)
                .OrderByDescending(p => p.Position)
                .To<PlaylistMusicViewModel>();

            var expected = new PlaylistMusicsAllViewModel<PlaylistMusicViewModel>()
            {
                PlaylistMusics = playlistMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(playlistMusics.Count(), entitesPerPage),
            };

            var actual = playlistMusicService.All<PlaylistMusicViewModel>(page, entitesPerPage, id);

            expected.Should().BeEquivalentTo(actual);
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

            var expected = this.ChangePlaylistMusicPostion(playlistMusicId, playlistMusicNewPosition);
            await playlistMusicService.Update(model, playlistMusicId);

            var playlistMusic = this.context.PlaylistMusics.Find(playlistMusicId);
            var playlistId = playlistMusic.PlaylistId;
            var actual = this.context.PlaylistMusics
                .Where(pm => pm.PlaylistId == playlistId)
                .ToList();

            expected.Should().BeEquivalentTo(actual);
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

            var expected = this.ChangePlaylistMusicPostion(playlistMusicId, playlistMusicNewPosition);
            await playlistMusicService.Update(model, playlistMusicId);

            var playlistMusic = this.context.PlaylistMusics.Find(playlistMusicId);
            var playlistId = playlistMusic.PlaylistId;
            var actual = this.context.PlaylistMusics
                .Where(pm => pm.PlaylistId == playlistId)
                .ToList();

            expected.Should().BeEquivalentTo(actual);
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

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
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
