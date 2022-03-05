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
    using MusicHub.Web.ViewModels.AlbumMusicModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class AlbumMusicServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateAlbumMusic_WithValidData_ShouldWorkCorrect()
        {
            var albumMusicService = await this.CreateService(new List<AlbumMusic>());
            var model = AlbumMusicTestsData.CreateModel;
            var musicsInAlbum = this.context.AlbumMusics
              .Where(am => am.AlbumId == model.AlbumId);
            var expectedPosition = musicsInAlbum.Count() == 0 ? 0 : musicsInAlbum.Max(pm => pm.Position) + 1;
            await albumMusicService.Create(model);
            Assert.True(this.context.AlbumMusics.Any(pm => pm.MusicId == model.MusicId
                && pm.AlbumId == model.AlbumId
                && pm.Position == expectedPosition));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllMusicInAlbum_WithValidData(int page)
        {
            var albumMusicService = await this.CreateService(AlbumMusicTestsData.AlbumMusics());
            var id = AlbumMusicTestsData.AlbumId;
            var entitesPerPage = PaginationData.AlbumMusicsPerPage;

            var albumMusics = this.context.AlbumMusics
                .Where(p => p.AlbumId == id)
                .OrderByDescending(p => p.Position)
                .To<AlbumMusicViewModel>();

            var expected = new AlbumMusicsPageModel<AlbumMusicViewModel>()
            {
                AlbumMusics = albumMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(albumMusics.Count(), entitesPerPage),
            };

            var actual = albumMusicService.All<AlbumMusicViewModel>(page, entitesPerPage, id);

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task DeleteAlbumMusic_WithValidData_ShouldWorkCorrect()
        {
            var albumMusicService = await this.CreateService(AlbumMusicTestsData.AlbumMusics());
            var albumMusicId = AlbumMusicTestsData.DeleteAlbumMusicId;
            await albumMusicService.Delete(albumMusicId);
            Assert.False(this.context.PlayedMusics.Any(pm => pm.Id == albumMusicId));
        }

        [Fact]
        public async Task UpdateLastPosition_WithValidData_ShouldeReturnPublic()
        {
            var albumMusicService = await this.CreateService(AlbumMusicTestsData.AlbumMusics());
            var albumMusicId = AlbumMusicTestsData.UpdateLastPositionAlbumMusicId();
            var albumMusicNewPosition = AlbumMusicTestsData.UpdateLastPositionNewPosition();
            var model = new AlbumMusicEditInputModel()
            {
                Position = albumMusicNewPosition,
            };

            var expected = this.ChangeAlbumMusicPostion(albumMusicId, albumMusicNewPosition);
            await albumMusicService.Update(model, albumMusicId);
            var albumMusic = this.context.AlbumMusics.Find(albumMusicId);
            var albumId = albumMusic.AlbumId;
            var actual = this.context.AlbumMusics
                .Where(pm => pm.AlbumId == albumId)
                .ToList();
            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task UpdateFirstPosition_WithValidData_ShouldeReturnPublic()
        {
            var albumMusicService = await this.CreateService(AlbumMusicTestsData.AlbumMusics());
            var albumMusicId = AlbumMusicTestsData.UpdateFistPositionAlbumMusicId;
            var albumMusicNewPosition = AlbumMusicTestsData.UpdateFirstPositionNewPosition();
            var model = new AlbumMusicEditInputModel()
            {
                Position = albumMusicNewPosition,
            };

            var expected = this.ChangeAlbumMusicPostion(albumMusicId, albumMusicNewPosition);
            await albumMusicService.Update(model, albumMusicId);
            var albumMusic = this.context.AlbumMusics.Find(albumMusicId);
            var albumId = albumMusic.AlbumId;
            var actual = this.context.AlbumMusics
                .Where(pm => pm.AlbumId == albumId)
                .ToList();
            expected.Should().BeEquivalentTo(actual);
        }

        private List<AlbumMusic> ChangeAlbumMusicPostion(string albumMusicId, int albumMusicNewPosition)
        {
            var albumMusic = this.context.AlbumMusics.Find(albumMusicId);
            var albumId = albumMusic.AlbumId;
            var oldPosition = albumMusic.Position;
            var albumMusicsInAlbum = this.context.AlbumMusics
                .Where(pm => pm.AlbumId == albumId)
                .ToList();

            if (oldPosition >= albumMusicNewPosition)
            {
                for (int i = albumMusicNewPosition + 1; i < oldPosition; i++)
                {
                    albumMusicsInAlbum[i].Position++;
                }
            }
            else
            {
                for (int i = oldPosition + 1; i < albumMusicNewPosition; i++)
                {
                    albumMusicsInAlbum[i].Position--;
                }
            }

            albumMusicsInAlbum
                .Where(pm => pm.Id == albumMusicId)
                .FirstOrDefault()
                .Position = albumMusicNewPosition;
            return albumMusicsInAlbum;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<AlbumMusicService> CreateService(List<AlbumMusic> albumMusics)
        {
            await this.context.Users.AddRangeAsync(AlbumMusicTestsData.Users);
            await this.context.Categories.AddRangeAsync(AlbumMusicTestsData.Categories);
            await this.context.AlbumMusics.AddRangeAsync(albumMusics);
            await this.context.Musics.AddRangeAsync(AlbumMusicTestsData.Musics);
            await this.context.AlbumMusics.AddRangeAsync(albumMusics);
            await this.context.SaveChangesAsync();
            var service = new AlbumMusicService(this.context, new PaginationService());
            return service;
        }
    }
}