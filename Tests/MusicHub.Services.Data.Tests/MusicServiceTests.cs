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
    using FluentAssertions;

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
                && m.Privacy == model.Privacy
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
                && m.Privacy == model.Privacy
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
            Assert.False(musicService.IsOwned(musicId, userId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllMusic_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var actual = musicService.All<MusicViewModel>(page, entitesPerPage);

            var musics = this.context.Musics
                   .Where(m => m.Privacy == Privacy.Public)
                   .OrderByDescending(m => m.CreatedOn)
                   .ThenBy(m => m.MusicReactions.Count(r => r.Reaction == Reaction.Like))
                   .ThenBy(m => m.Name)
                   .To<MusicViewModel>();

            var expected = new MusicPageModel<MusicViewModel>()
            {
                Musics = musics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(musics.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterMusic_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModel;
            var actual = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            this.CheckFilter(actual, page, musicService, entitesPerPage, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterMusicWithCategory_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWithCategory;

            var actual = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            this.CheckFilter(actual, page, musicService, entitesPerPage, filter);
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
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWithCategory;

            var actual = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter, userId);

            this.CheckFilter(actual, page, musicService, entitesPerPage, filter, userId);
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
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWithCategory;

            var actual = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter, userId);

            this.CheckFilter(actual, page, musicService, entitesPerPage, filter, userId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterMusicWithUserId_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWhitUserId;

            var actual = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            this.CheckFilter(actual, page, musicService, entitesPerPage, filter);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetFilterModelWhitUserIdAndCategoryId_WithValidData_ShouldeReturnPublicMusics(int page)
        {
            var getMusics = MusicTestsData.GetMusics;
            var musicService = await this.CreateMusicService(getMusics);
            var entitesPerPage = PaginationData.MusicsPerPage;
            var filter = MusicTestsData.FilterModelWhitUserIdAndCategoryId;

            var actual = musicService.Filter<MusicViewModel>(page, entitesPerPage, filter);

            this.CheckFilter(actual, page, musicService, entitesPerPage, filter);
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
            var entitesPerPage = PaginationData.MusicsPerPage;
           
            var musics = this.context.Musics
                               .Where(s => s.UserId == userId)
                               .OrderByDescending(s => s.CreatedOn)
                               .To<MusicViewModel>();
            var expected = new MusicPageModel<MusicViewModel>()
            {
                Musics = musics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(musics.Count(), entitesPerPage),
            };

            var actual = musicService.AllOwn<MusicViewModel>(page, entitesPerPage, userId);

            expected.Should().BeEquivalentTo(actual);
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
                  && m.CreatedOn == result.CreatedOn
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
            var entitesPerPage = PaginationData.MusicsPerPage;

            var suggestMusics = this.context.Musics
                .Where(s => s.Id != music.Id && s.Privacy == Privacy.Public)
                .OrderByDescending(s => s.CategoryId == music.CategoryId)
                .ThenByDescending(s => s.CreatedOn)
                .ThenByDescending(s => s.PlayedMusics.Count);

            var expected = new MusicPageModel<MusicViewModel>()
            {
                Musics = suggestMusics
                       .To<MusicViewModel>()
                       .GetPage(page, entitesPerPage)
                       .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(suggestMusics.Count(), entitesPerPage),
            };

            var actual = musicService.SuggestMusics<MusicViewModel>(page, entitesPerPage, music.Id);

            expected.Should().BeEquivalentTo(actual);
        }

        private void CheckFilter(MusicPageModel<MusicViewModel> actual, int page, MusicService musicService, int entitesPerPage, MusicFilter filter, string userId = null)
        {
            IQueryable<Music> musics = this.context.Musics
                  .Where(m => m.Privacy == Privacy.Public)
                  .OrderByDescending(m => m.CreatedOn);

            if (!string.IsNullOrEmpty(userId))
            {
                musics = musics.Where(m => m.UserId == userId);
            }
            else
            {
                musics = musics.Where(m => m.Privacy == Privacy.Public);
            }

            musics = this.FilteringMusics(filter, musics);

            musics = this.OrederSearchMusics(filter.OrderMethod, musics);

            var expected = new MusicPageModel<MusicViewModel>()
            {
                Musics = musics
                    .GetPage(page, entitesPerPage)
                    .To<MusicViewModel>()
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(musics.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private int GetMusicsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private IQueryable<Music> OrederSearchMusics(OrderMethod orderMethod, IQueryable<Music> musics)
        {
            musics = musics.OrderByDescending(m => (m.PlayedMusics.Count / 2)
                + ((m.MusicReactions.Count(r => r.Reaction == Reaction.Like)
                - m.MusicReactions.Count(r => r.Reaction == Reaction.Dislike)) * 2)
                + m.Comments.Count());
            /*
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
            }  */

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
