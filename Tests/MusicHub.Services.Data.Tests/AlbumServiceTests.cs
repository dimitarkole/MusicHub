namespace MusicHub.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FluentAssertions;
    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.AlbumModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class AlbumServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateAlbum_WithValidData_ShouldWorkCorrect()
        {
            var albumService = await this.CreateService(new List<Album>());
            var model = AlbumTestsData.CreateModel;
            var userId = AlbumTestsData.CreateUserId;
            await albumService.Create(model, userId);
            Assert.True(this.context.Albums.Any(a => a.Name == model.Name
                && a.UserId == userId
                && a.ImageUrl == model.ImageUrl
                && a.Privacy == model.Privacy));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllAlbums_WithValidData_ShouldeReturnPublicAlbums(int page)
        {
            var albumService = await this.CreateService(new List<Album>());
            var entitesPerPage = PaginationData.AlbumsPerPage;
            var actual = albumService.All<AlbumViewModel>(page, entitesPerPage);
            this.CheckFilterData(actual, page, entitesPerPage, new AlbumFilterInputModel());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllOwnedAlbums_WithValidData_ShouldeReturnAlbums(int page)
        {
            var albumsService = await this.CreateService(new List<Album>());
            var userId = AlbumTestsData.AllOwnedUserId;
            var entitesPerPage = PaginationData.AlbumsPerPage;
            var actual = albumsService.All<AlbumViewModel>(page, entitesPerPage, userId);
            this.CheckFilterData(actual, page, entitesPerPage, new AlbumFilterInputModel(), userId);
        }

        [Fact]
        public async Task DeleteAlbum_WithValidData_ShouldWorkCorrect()
        {
            var albumsService = await this.CreateService(AlbumTestsData.Albums);
            var albumId = AlbumTestsData.DeleteAlbumId;
            await albumsService.Delete(albumId);
            Assert.False(this.context.Albums.Any(a => a.Id == albumId));
        }

        [Fact]
        public async Task UpdateAlbum_WithValidData_ShouldWorkCorrect()
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var model = AlbumTestsData.UpdateModel;
            var albumId = AlbumTestsData.UpdateAlbumId;
            await albumService.Update(model, albumId);
            Assert.True(this.context.Albums.Any(a => a.Id == albumId
                && a.Name == model.Name
                && a.ImageUrl == model.ImageUrl
                && a.Privacy == model.Privacy));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterAlbums_WithValidData_ShouldeReturnPublicAlbums(int page)
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var entitesPerPage = PaginationData.AlbumsPerPage;
            var filter = AlbumTestsData.FilterModel;
            var actual = albumService.Filter<AlbumViewModel>(page, entitesPerPage, filter);
            this.CheckFilterData(actual, page, entitesPerPage, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterOwned_WithValidData_ShouldeReturn(int page)
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var userId = AlbumTestsData.AllOwnedUserId;
            var entitesPerPage = PaginationData.AlbumsPerPage;
            var filter = AlbumTestsData.FilterModel;
            var actual = albumService.Filter<AlbumViewModel>(page, entitesPerPage, filter, userId);
            this.CheckFilterData(actual, page, entitesPerPage, filter, userId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterAlbumsWithUserId_WithValidData_ShouldeReturnPublicAlbums(int page)
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var entitesPerPage = PaginationData.AlbumsPerPage;
            var filter = AlbumTestsData.FilterModelWithUserId;
            var actual = albumService.Filter<AlbumViewModel>(page, entitesPerPage, filter);
            this.CheckFilterData(actual, page, entitesPerPage, filter);
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var albumId = AlbumTestsData.GetById;
            var actual = albumService.GetById<AlbumViewModel>(albumId);
            var expected = this.context.Albums
                .Where(a => a.Id == albumId)
                .To<AlbumViewModel>()
                .FirstOrDefault();
            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task IsOwnedAlbum_WithValidData_ShouldeReturnTrue()
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var albumId = AlbumTestsData.IsOwnedAlbumTrueAlbumId;
            var userId = AlbumTestsData.IsOwnedAlbumTrueUserId;
            Assert.True(albumService.IsOwned(albumId, userId));
        }

        [Fact]
        public async Task IsOwnedAlbum_WithValidData_ShouldeReturnFalse()
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var albumId = AlbumTestsData.IsOwnedAlbumFalseAlbumId;
            var userId = AlbumTestsData.IsOwnedAlbumFalseUserId;
            Assert.False(albumService.IsOwned(albumId, userId));
        }

        [Fact]
        public async Task GetOwnedForAddingMusic_WithValidData_ShouldeReturn()
        {
            var albumService = await this.CreateService(AlbumTestsData.Albums);
            var userId = AlbumTestsData.AllOwnedUserId;
            var musicId = AlbumTestsData.GetOwnedForAddingMusicMusicId;
            var expected = this.context.Albums
                    .Where(a => a.UserId == userId
                         && a.AlbumMusics.Any(am => am.MusicId == musicId) == false)
                    .OrderByDescending(ps => ps.Name)
                    .To<AlbumViewModel>()
                    .ToList();
            var actual = albumService.GetOwnedForAddingMusic<AlbumViewModel>(musicId, userId);
            expected.Should().BeEquivalentTo(actual);
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private IQueryable<Album> FilteringAlbums(AlbumFilterInputModel filter, IQueryable<Album> albums)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                albums = albums.Where(a => a.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                albums = albums.Where(a => a.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return albums;
        }

        private IQueryable<Album> OrderSearchAlbums(OrderMethod orderMethod, IQueryable<Album> albums)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                albums = albums.OrderBy(a => a.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                albums = albums.OrderByDescending(a => a.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                albums = albums.OrderBy(a => a.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                albums = albums.OrderByDescending(a => a.Name);
            }

            return albums;
        }

        private void CheckFilterData(AlbumPageModel<AlbumViewModel> actual, int page, int entitesPerPage, AlbumFilterInputModel filter, string userId = null)
        {
            IQueryable<Album> albums = this.context.Albums
                    .Where(p => p.Privacy == Privacy.Public
                        && p.AlbumMusics.Count > 0);

            if (userId != null)
            {
                albums = albums.Where(p => p.UserId == userId);
            }

            albums = this.FilteringAlbums(filter, albums);
            albums = this.OrderSearchAlbums(filter.OrderMethod, albums);
            var expected = new AlbumPageModel<AlbumViewModel>()
            {
                Albums = albums
                    .GetPage(page, entitesPerPage)
                    .To<AlbumViewModel>()
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(albums.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private async Task<AlbumService> CreateService(List<Album> albums)
        {
            await this.context.Users.AddRangeAsync(AlbumTestsData.Users);
            await this.context.Albums.AddRangeAsync(albums);
            await this.context.Musics.AddRangeAsync(AlbumTestsData.Musics);
            await this.context.AlbumMusics.AddRangeAsync(AlbumTestsData.AlbumMusics);
            await this.context.SaveChangesAsync();
            var service = new AlbumService(this.context, new PaginationService());

            return service;
        }
    }
}
