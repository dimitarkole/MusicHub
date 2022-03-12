namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data;
    using MusicHub.Services.Data.Tests;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class PlaylistServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePlaylist_WithValidData_ShouldWorkCorrect()
        {
            var playlistService = await this.CreatePlaylistService(new List<Playlist>());
            var model = PlaylistTestsData.CreateModel;
            var userId = PlaylistTestsData.CreateUserId;

            await playlistService.Create(model, userId);

            Assert.True(this.context.Playlists.Any(p => p.Name == model.Name
                && p.UserId == userId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllPlaylists_WithValidData_ShouldeReturnPublicPlaylist(int page)
        {
            var playlistService = await this.CreatePlaylistService(new List<Playlist>());
            var expextedResult = this.context.Playlists
                    .Where(p => p.VisibleStatus == VisibleStatus.Public && p.PlaylistSongs.Count > 0)
                    .OrderByDescending(p => p.CreatedOn)
                    .ThenBy(p => p.Name)
                    .To<PlaylistViewModel>();
            var entitesPerPage = PaginationData.PlaylistsPerPage;
            var result = playlistService.All<PlaylistViewModel>(page, entitesPerPage);
            var countPages = this.GetPlaylistPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            CheckPlaylistViewModelsIsEqual(page, expectedMusicsList, result, countPages);
        }

        [Fact]
        public async Task DeleteCategory_WithValidData_ShouldWorkCorrect()
        {
            var playlistService = await this.CreatePlaylistService(PlaylistTestsData.Playlists) ;

            var playlistId = PlaylistTestsData.DeletePlaylistId;

            await playlistService.Delete(playlistId);

            Assert.False(this.context.Playlists.Any(c => c.Id == playlistId));
        }

        [Fact]
        public async Task UpdatePlaylist_WithValidData_ShouldWorkCorrect()
        {
            var playlistService = await this.CreatePlaylistService(PlaylistTestsData.Playlists);

            var model = PlaylistTestsData.UpdateModel;
            var playlistId = PlaylistTestsData.UpdatePlaylistId;

            await playlistService.Update(model, playlistId);

            Assert.True(this.context.Playlists.Any(p => p.Id == playlistId
                && p.Name == model.Name));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterPlaylists_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var playlistService = await this.CreatePlaylistService(PlaylistTestsData.Playlists);
            IQueryable<Playlist> expextedResult = this.context.Playlists
                    .Where(p => p.VisibleStatus == VisibleStatus.Public
                        && p.PlaylistSongs.Count > 0);
            var entitesPerPage = PaginationData.PlaylistsPerPage;
            var filter = PlaylistTestsData.FilterModel;

            var result = playlistService.Filter<PlaylistViewModel>(page, entitesPerPage, filter);

            expextedResult = this.FilteringPlaylists(filter, expextedResult);
            expextedResult = this.OrederSearchPlaylist(filter.OrderMethod, expextedResult);
            var countPages = this.GetPlaylistPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<PlaylistViewModel>()
               .ToList();

            CheckPlaylistViewModelsIsEqual(page, expectedMusicsList, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterPlaylistsWithUserId_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var playlistService = await this.CreatePlaylistService(PlaylistTestsData.Playlists);
            IQueryable<Playlist> expextedResult = this.context.Playlists
                    .Where(p => p.VisibleStatus == VisibleStatus.Public
                        && p.PlaylistSongs.Count > 0);
            var entitesPerPage = PaginationData.PlaylistsPerPage;
            var filter = PlaylistTestsData.FilterModelWithUserId;

            var result = playlistService.Filter<PlaylistViewModel>(page, entitesPerPage, filter);

            expextedResult = this.FilteringPlaylists(filter, expextedResult);
            expextedResult = this.OrederSearchPlaylist(filter.OrderMethod, expextedResult);
            var countPages = this.GetPlaylistPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<PlaylistViewModel>()
               .ToList();

            CheckPlaylistViewModelsIsEqual(page, expectedMusicsList, result, countPages);
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var playlistService = await this.CreatePlaylistService(PlaylistTestsData.Playlists);
            var playlistId = PlaylistTestsData.GetById;

            var result = playlistService.GetById<PlaylistViewModel>(playlistId);
            var expextedResult = this.context.Playlists
                .Where(p => p.Id == playlistId)
                .To<PlaylistViewModel>()
                .FirstOrDefault();

            Assert.True(CheckPlaylistcViewModelIsEqual(expextedResult, result));
        }

        private static void CheckPlaylistViewModelsIsEqual(int page, List<PlaylistViewModel> expectedResults, PlaylistsAllViewModel<PlaylistViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultPlaylists = result.Playlists.ToList();
            Assert.Equal(resultPlaylists.Count, expectedResults.Count);

            for (int i = 0; i < expectedResults.Count(); i++)
            {
                var expextedResult = expectedResults[i];
                var resultPlaylist = resultPlaylists[i];
                Assert.True(CheckPlaylistcViewModelIsEqual(expextedResult, resultPlaylist));
            }
        }

        private static bool CheckPlaylistcViewModelIsEqual(PlaylistViewModel expectedPlaylist, PlaylistViewModel resultPlaylist)
            => expectedPlaylist.Name == resultPlaylist.Name
               && expectedPlaylist.Id == resultPlaylist.Id
               && expectedPlaylist.User.UserName == resultPlaylist.User.UserName
               && expectedPlaylist.User.Id == resultPlaylist.User.Id
               && expectedPlaylist.User.ImageUrl == resultPlaylist.User.ImageUrl
               && expectedPlaylist.CreatedOn == resultPlaylist.CreatedOn;

        private int GetPlaylistPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private IQueryable<Playlist> FilteringPlaylists(PlaylistFilterInputModel filter, IQueryable<Playlist> playlists)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                playlists = playlists.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                playlists = playlists.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return playlists;
        }

        private IQueryable<Playlist> OrederSearchPlaylist(OrderMethod orderMethod, IQueryable<Playlist> playlists)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                playlists = playlists.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                playlists = playlists.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                playlists = playlists.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                playlists = playlists.OrderByDescending(s => s.Name);
            }

            return playlists;
        }

        private async Task<PlaylistService> CreatePlaylistService(List<Playlist> playlists)
        {
            await this.context.Users.AddRangeAsync(PlaylistTestsData.Users);
            await this.context.Playlists.AddRangeAsync(playlists);
            await this.context.Songs.AddRangeAsync(PlaylistTestsData.Musics);
            await this.context.PlaylistSongs.AddRangeAsync(PlaylistTestsData.PlaylistSongs);
            await this.context.SaveChangesAsync();
            var service = new PlaylistService(this.context, new PaginationService());

            return service;
        }
    }
}
