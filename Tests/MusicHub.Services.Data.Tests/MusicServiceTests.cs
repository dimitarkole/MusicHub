namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Services.Mapping;
    using Xunit;
    using MusicHub.Common.Extensions;
    using static MusicHub.Common.GlobalConstants;
    using MusicHub.Common;

    public class MusicServiceTests : BaseTestClass
    {

        [Fact]
        public async Task CreateMusic_WithValidData_ShouldWorkCorrect()
        {
            var musicService = await this.CreateMusicService(new List<Music>());
            var model = MusicTestsData.CreateModel;
            var userId = MusicTestsData.CreateUserId;

            await musicService.Create(model, userId);

            Assert.True(this.context.Musics.Any(m => m.Name == model.Name
                && m.UserId == userId
                && m.Description == model.Description
                && m.CategoryId == model.CategoryId
                && m.ImageUrl == model.ImageUrl
                && m.VisibleStatus == model.VisibleStatus
                && m.MusicLicenseType == model.MusicLicenseType
                && m.AudioUrl == model.AudioUrl));
        }

        [Fact]
        public async Task UpdateMusic_WithValidData_ShouldWorkCorrect()
        {
            var musicService = await this.CreateMusicService(MusicTestsData.GetMusics);
            var model = MusicTestsData.UpdateModel;
            var musicId = MusicTestsData.UpdateMusicId;

            await musicService.Update(musicId, model);

            Assert.True(this.context.Musics.Any(m => m.Name == model.Name
                && m.Description == model.Description
                && m.CategoryId == model.CategoryId
                && m.ImageUrl == model.ImageUrl
                && m.VisibleStatus == model.VisibleStatus
                && m.MusicLicenseType == model.MusicLicenseType
                && m.AudioUrl == model.AudioUrl));
        }

        [Fact]
        public async Task DeleteMusic_WithValidData_ShouldWorkCorrect()
        {
            var getMusic = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusic);
            var musicId = MusicTestsData.DeleteMusicId;

            await musicService.Delete(musicId);

            Assert.False(this.context.Musics.Any(c => c.Id == musicId));
        }

        [Fact]
        public async Task IsOwnedMusic_WithValidData_ShouldeReturnTrue()
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var musicId = MusicTestsData.IsOwnedTrueMusicId;
            var userId = MusicTestsData.IsOwedMusicTrueUserId;

            Assert.True(musicService.IsOwned(musicId, userId));
        }

        [Fact]
        public async Task IsOwnedMusic_WithValidData_ShouldeReturnFalse()
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var musicId = MusicTestsData.IsOwnedFalseMusicId;
            var userId = MusicTestsData.IsOwnedMusicFalseUserId;
            var r = musicService.IsOwned(musicId, userId);
            Assert.False(r);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetAllMusic_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var expextedResult = this.context.Musics
                    .Where(m => m.VisibleStatus == VisibleStatus.Public)
                    .OrderByDescending(m => m.CreatedOn)
                    .ThenBy(m => m.MusicReactions.Count(r => r.Reaction == Reaction.Like))
                    .ThenBy(m => m.Name)
                    .To<MusicViewModel>();
            var entitesPerPage = PaginationData.MusicsPerPage;
            var result = musicService.All<MusicViewModel>(page, entitesPerPage);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterMusic_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            IQueryable<Music> expextedResult = this.context.Musics
                    .Where(m => m.VisibleStatus == VisibleStatus.Public);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModel;

            var result = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            expextedResult = this.FilteringMusics(filter, expextedResult);
            expextedResult = this.OrederSearchMusics(filter.OrderMethod, expextedResult);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<MusicViewModel>()
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterMusicWithCategory_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            IQueryable<Music> expextedResult = this.context.Musics
                    .Where(m => m.VisibleStatus == VisibleStatus.Public);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWithCategory;

            var result = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            expextedResult = this.FilteringMusics(filter, expextedResult);
            expextedResult = this.OrederSearchMusics(filter.OrderMethod, expextedResult);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<MusicViewModel>()
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterOwnedMusic_WithValidData_ShouldeReturnMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var userId = MusicTestsData.FilterUserId;
            IQueryable<Music> expextedResult = this.context.Musics
                    .Where(m => m.UserId == userId);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModel;

            var result = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter, userId);

            expextedResult = this.FilteringMusics(filter, expextedResult);
            expextedResult = this.OrederSearchMusics(filter.OrderMethod, expextedResult);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<MusicViewModel>()
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterOwnedMusicWithCategory_WithValidData_ShouldeReturnMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var userId = MusicTestsData.FilterUserId;
            IQueryable<Music> expextedResult = this.context.Musics
                    .Where(m => m.UserId == userId);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWithCategory;

            var result = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter, userId);

            expextedResult = this.FilteringMusics(filter, expextedResult);
            expextedResult = this.OrederSearchMusics(filter.OrderMethod, expextedResult);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<MusicViewModel>()
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterMusicWithUserId_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            IQueryable<Music> expextedResult = this.context.Musics
                    .Where(m => m.VisibleStatus == VisibleStatus.Public);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWhitUserId;

            var result = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            expextedResult = this.FilteringMusics(filter, expextedResult);
            expextedResult = this.OrederSearchMusics(filter.OrderMethod, expextedResult);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<MusicViewModel>()
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterModelWhitUserIdAndCategoryId_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            IQueryable<Music> expextedResult = this.context.Musics
                    .Where(m => m.VisibleStatus == VisibleStatus.Public);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWhitUserIdAndCategoryId;

            var result = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            expextedResult = this.FilteringMusics(filter, expextedResult);
            expextedResult = this.OrederSearchMusics(filter.OrderMethod, expextedResult);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);
            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .To<MusicViewModel>()
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetOwnedMusic_WithValidData_ShouldeReturnOwnedMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var userId = MusicTestsData.GetOwnedUserId;
            var expextedResult = this.context.Musics
                    .Where(m => m.UserId == userId)
                    .OrderByDescending(m => m.CreatedOn)
                    .To<MusicViewModel>();
            var entitesPerPage = PaginationData.MusicsPerPage;
            var result = musicService.AllOwn<MusicViewModel>(page, entitesPerPage,userId);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        [Fact]
        public async Task DeleteSong_WithValidData_ShouldWorkCorrect()
        {
            var getMuisics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMuisics);
            var musicId = MusicTestsData.DeleteMusicId;

            await musicService.Delete(musicId);

            Assert.False(this.context.Musics.Any(c => c.Id == musicId));
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var getMuisics = MusicTestsData.GetMusics;
            var songService = await this.CreateMusicService(getMuisics);
            var musicId = MusicTestsData.GetById;

            var result = songService.GetById<MusicViewModel>(musicId);

            Assert.True(this.context.Musics.Any(m => m.Id == musicId
                  && m.Name == result.Name
                  && m.AudioUrl == result.AudioUrl
                  && m.ImageUrl == result.ImageUrl
                  && m.CategoryId == result.Category.Id
                  && m.Description == result.Description
                  && m.CreatedOn == result.CreatedOn
                  && m.MusicLicenseType == result.MusicLicenseType
                  && m.VisibleStatus == result.VisibleStatus
                  && m.Category.Name == result.Category.Name));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task SuggestMusics_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var music = MusicTestsData.SuggestMusic;
            var expextedResult = this.context.Musics
                    .Where(s => s.Id != music.Id && s.VisibleStatus == VisibleStatus.Public)
                    .OrderByDescending(s => s.CategoryId == music.CategoryId)
                    .ThenByDescending(s => s.CreatedOn)
                    .ThenByDescending(s => s.PlayedMusics.Count)
                    .To<MusicViewModel>();
            var entitesPerPage = PaginationData.MusicsPerPage;
            var result = musicService.SuggestMusics<MusicViewModel>(page, entitesPerPage, music.Id);
            var countPages = this.GetMusicsPagesCount(expextedResult.Count(), entitesPerPage);

            var expectedMusicsList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            CheckMusicViewModelsIsEqual(page, expectedMusicsList, result, countPages, expectedMusicsList);
        }

        private static void CheckMusicViewModelsIsEqual(int page, List<MusicViewModel> expextedResult, MusicsAllViewModel<MusicViewModel> result, int countPages, List<MusicViewModel> expectedMusicsList)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultMusics = result.Musics.ToList();
            Assert.Equal(resultMusics.Count, expectedMusicsList.Count);

            for (int i = 0; i < expextedResult.Count(); i++)
            {
                var expectedMusic = expectedMusicsList[i];
                var resultMusic = resultMusics[i];
                Assert.True(CheckMusicViewModelIsEqual(expectedMusic, resultMusic));
            }
        }

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

        private IQueryable<Music> OrederSearchMusics(OrderMethod orderMethod, IQueryable<Music> musics)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                musics = musics.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                musics = musics.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                musics = musics.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                musics = musics.OrderByDescending(s => s.Name);
            }

            return musics;
        }

        private IQueryable<Music> FilteringMusics(MusicFilter filter, IQueryable<Music> musics)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                musics = musics.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                musics = musics.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                musics = musics.Where(s => s.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                musics = musics.Where(s => s.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return musics;
        }

        private async Task<MusicService> CreateMusicService(List<Music> musics)
        {
            var users = MusicTestsData.Users;
            var categories = MusicTestsData.Categories;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Musics.AddRangeAsync(musics);
            await this.context.MusicLicenses.AddRangeAsync(new List<MusicLicense>());
            await this.context.SaveChangesAsync();
            var service = new MusicService(this.context, new PaginationService());

            return service;
        }
    }
}
