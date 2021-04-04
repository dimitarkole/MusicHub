using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using MusicHub.Web.ViewModels.MusicReactionModels;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Web.ViewModels.MusicModels;
using MusicHub.Common;

namespace MusicHub.Services.Data.Tests
{
    public class MusicReactionServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateMusic_WithValidData_ShouldWorkCorrect()
        {
            var musicReactionService = await this.CreateMusicReactionService(new List<MusicReaction>());
            var model = MusicReactionTestsData.CreateModel;
            var userId = MusicTestsData.CreateUserId;

            await musicReactionService.Create(model, userId);

            Assert.True(this.context.MusicReactions.Any(mr => mr.MusicId == model.MusicId
                && mr.UserId == userId
                && mr.Reaction == mr.Reaction));
        }

        [Fact]
        public async Task DeleteMusicReaction_WithValidData_ShouldWorkCorrect()
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);
            var musicReactionId = musicReactions[0].Id;

            await musicReactionService.Delete(musicReactionId);

            Assert.False(this.context.MusicReactions.Any(c => c.Id == musicReactionId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAll_WithValidData_ShouldeReturnLikedMusics(int page)
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);
            var musicReactionInfo = musicReactions.GroupBy(mr => mr.UserId)
                .ToDictionary(mr => mr.Key, mr => mr)
                .OrderBy(mr => mr.Value.Count())
                .FirstOrDefault();
            var userId = musicReactionInfo.Key;
            var allLikedMusicByUser = musicReactionInfo.Value
                .Where(mr => mr.Reaction == Reaction.Like)
                .OrderByDescending(s => s.CreatedOn)
                .To<LikedMusicViewModel>();
            var entitesPerPage = PaginationData.LikedMusicsPerPage;
            var countPages = this.GetMusicReactionsPagesCount(allLikedMusicByUser.Count(), entitesPerPage);

            var expectedResult = allLikedMusicByUser.GetPage(page, entitesPerPage)
               .ToList();

            var result = musicReactionService.All<LikedMusicViewModel>(page, entitesPerPage, userId);

            this.ChecLikedMusicAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Filter_WithValidData_ShouldeReturnLikedMusics(int page)
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);
            var userId = musicReactions[0].UserId;

            var allLikedMusicByUser = this.context.MusicReactions
                .Where(s => s.UserId == userId
                && s.Reaction == Reaction.Like);

            var filter = MusicReactionTestsData.Filter;
            allLikedMusicByUser = FilteringMusics(filter, allLikedMusicByUser);
            allLikedMusicByUser = OrederSearchMusics(filter.OrderMethod, allLikedMusicByUser);

            var allLikedMusicByUserList = allLikedMusicByUser.ToList();

            var entitesPerPage = PaginationData.LikedMusicsPerPage;
            var countPages = this.GetMusicReactionsPagesCount(allLikedMusicByUser.Count(), entitesPerPage);

            var expectedResult = allLikedMusicByUser.GetPage(page, entitesPerPage)
                .To<LikedMusicViewModel>()
                .ToList();

            var result = musicReactionService.Filter<LikedMusicViewModel>(page, entitesPerPage, userId, filter);

            this.ChecLikedMusicAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterWithCategory_WithValidData_ShouldeReturnLikedMusics(int page)
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);
            var userId = musicReactions[0].UserId;

            var allLikedMusicByUser = this.context.MusicReactions
                .Where(s => s.UserId == userId
                && s.Reaction == Reaction.Like);

            var filter = MusicReactionTestsData.FilterWithCategory;
            allLikedMusicByUser = FilteringMusics(filter, allLikedMusicByUser);
            allLikedMusicByUser = OrederSearchMusics(filter.OrderMethod, allLikedMusicByUser);

            var allLikedMusicByUserList = allLikedMusicByUser.ToList();

            var entitesPerPage = PaginationData.LikedMusicsPerPage;
            var countPages = this.GetMusicReactionsPagesCount(allLikedMusicByUser.Count(), entitesPerPage);

            var expectedResult = allLikedMusicByUser.GetPage(page, entitesPerPage)
                .To<LikedMusicViewModel>()
                .ToList();

            var result = musicReactionService.Filter<LikedMusicViewModel>(page, entitesPerPage, userId, filter);

            this.ChecLikedMusicAllViewModelsIsEqual(page, countPages, expectedResult, result);
        }

        [Fact]
        public async Task IsOwnedMusicReaction_WithValidData_ShouldeReturnTrue()
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);

            var userId = musicReactions[0].UserId;
            var musicReactionId = musicReactions[0].Id;

            Assert.True(musicReactionService.IsOwned(musicReactionId, userId));
        }

        [Fact]
        public async Task IsOwnedMusicReaction_WithValidData_ShouldeReturnFalse()
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);

            var userId = musicReactions[0].UserId;
            var musicReactionId = musicReactions
                .Where(pd => pd.UserId != userId)
                .FirstOrDefault()
                .Id;

            Assert.False(musicReactionService.IsOwned(musicReactionId, userId));
        }

        [Fact]
        public async Task GetOwnReaction__WithValidData_ShouldWorkCorrect()
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);
            var userId = musicReactions[0].UserId;
            var musicId = musicReactions[0].MusicId;
            var expectedRusult = this.context.MusicReactions
              .Where(r => r.UserId == userId
                  && r.MusicId == musicId)
              .To<MusicReactionViewModel>()
              .FirstOrDefault();
            var result = musicReactionService.GetOwnReaction(musicId, userId);

            Assert.True(expectedRusult.Id == result.Id
                    && expectedRusult.Reaction == result.Reaction);
        }

        [Fact]
        public async Task UpdateMusicReaction_WithValidData_ShouldWorkCorrect()
        {
            var musicReactions = MusicReactionTestsData.MusicReactions();
            var musicReactionService = await this.CreateMusicReactionService(musicReactions);
            var model = MusicReactionTestsData.UpdateModel;
            var musicReactionId = musicReactions[0].Id;

            await musicReactionService.Update(model, musicReactionId);

            Assert.True(this.context.MusicReactions.Any(mr => mr.Id == musicReactionId
                && mr.Reaction == model.Reaction));
        }

        private static IQueryable<MusicReaction> FilteringMusics(MusicFilter filter, IQueryable<MusicReaction> musicReactions)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                musicReactions = musicReactions.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                musicReactions = musicReactions.Where(s => s.Music.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                musicReactions = musicReactions.Where(s => s.Music.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                musicReactions = musicReactions.Where(s => s.Music.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return musicReactions;
        }

        private static IQueryable<MusicReaction> OrederSearchMusics(OrderMethod orderMethod, IQueryable<MusicReaction> musicReactions)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                musicReactions = musicReactions.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                musicReactions = musicReactions.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                musicReactions = musicReactions.OrderBy(s => s.Music.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                musicReactions = musicReactions.OrderByDescending(s => s.Music.Name);
            }

            return musicReactions;
        }

        private void ChecLikedMusicAllViewModelsIsEqual(int page, int countPages, List<LikedMusicViewModel> expectedResult, LikedMusicsAllViewModel<LikedMusicViewModel> result)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultLikedMusics = result.LinkedMusics.ToList();
            Assert.Equal(expectedResult.Count, resultLikedMusics.Count);

            for (int i = 0; i < expectedResult.Count(); i++)
            {
                var expectedMusic = expectedResult[i];
                var resultMusic = resultLikedMusics[i];
                Assert.True(this.ChecktLikedMusicViewModelIsEqual(expectedMusic, resultMusic));
            }
        }

        private bool ChecktLikedMusicViewModelIsEqual(LikedMusicViewModel expectedMusic, LikedMusicViewModel resultMusic)
            => expectedMusic.Id == resultMusic.Id
                && expectedMusic.CreatedOn == expectedMusic.CreatedOn
                && this.CheckMusicViewModelIsEqual(expectedMusic.Music, resultMusic.Music);

        private bool CheckMusicViewModelIsEqual(MusicViewModel expectedMusic, MusicViewModel resultMusic)
           => expectedMusic.Name == resultMusic.Name
              && expectedMusic.Category.Name == resultMusic.Category.Name
              && expectedMusic.Category.Id == resultMusic.Category.Id
              && expectedMusic.ImageUrl == resultMusic.ImageUrl
              && expectedMusic.VisibleStatus == resultMusic.VisibleStatus
              && expectedMusic.MusicLicenseType == resultMusic.MusicLicenseType
              && expectedMusic.AudioUrl == resultMusic.AudioUrl
              && expectedMusic.UserUserName == resultMusic.UserUserName;

        private int GetMusicReactionsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<MusicReactionService> CreateMusicReactionService(List<MusicReaction> musicReactions)
        {
            var users = MusicReactionTestsData.Users;
            var categories = MusicReactionTestsData.Categories;
            var musics = MusicReactionTestsData.Musics;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Musics.AddRangeAsync(musics);
            await this.context.MusicReactions.AddRangeAsync(musicReactions);
            await this.context.SaveChangesAsync();
            var service = new MusicReactionService(this.context, new PaginationService());

            return service;
        }
    }
}
