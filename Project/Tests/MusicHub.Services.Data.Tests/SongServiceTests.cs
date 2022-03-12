namespace MusicHub.Services.Data.Tests
{
    using MusicHub.Data.Models;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.SongModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class SongServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateSong_WithValidData_ShouldWorkCorrect()
        {
            var categories = SongTestData.Categories;
            var users = SongTestData.Users;
            var songService = await this.CreateSongService(new List<Song>(), new List<Comment>(), categories, users);
            var model = SongTestData.CreateModel;
            var userId = SongTestData.CreateUserId;

            await songService.Create(model, userId);

            Assert.True(this.context.Songs.Any(s => s.Name == model.Name
                && s.UserId == userId
                && s.Text == model.Text
                && s.CategoryId == model.CategoryId
                && s.ImageFilePath == model.ImageFilePath
                && s.AudioFilePath == model.AudioFilePath));
        }

      /*  [Fact]
        public async Task AllSongs_WithValidData_ShouldWorkCorrect()
        {
            var getSongs = SongTestData.GetSongs;
            var songService = await this.CreateSongService(getSongs, new List<Comment>(), new List<Category>(), new List<ApplicationUser>());
            int expextedCount = this.context.Songs.Count();

            var results = songService.All<SongViewModel>().ToList();

            Assert.Equal(expextedCount, results.Count);
            foreach (var result in results)
            {
                Assert.True(this.context.Songs.Any(s => s.Name == result.Name
                    && s.User.UserName == result.UserUserName
                    && s.CategoryId == result.CategoryId
                    && s.ImageFilePath == result.ImageFilePath
                    && s.AudioFilePath == result.AudioFilePath
                    && s.Category.Name == result.CategoryName));
            }
        }

        [Fact]
        public async Task AllOwnSongs_WithValidData_ShouldWorkCorrect()
        {
            var getSongs = SongTestData.GetSongs;
            var songService = await this.CreateSongService(getSongs, new List<Comment>(), new List<Category>(), new List<ApplicationUser>());
            string userId = SongTestData.GetOwnSongsUserId;
            int expextedCount = this.context.Songs.Where(s => s.UserId == userId).Count();
            var results = songService.AllOwn<SongViewModel>(userId).ToList();

            Assert.Equal(expextedCount, results.Count);
            foreach (var result in results)
            {
                Assert.True(this.context.Songs.Any(s => s.Name == result.Name
                    && s.User.UserName == result.UserUserName
                    && s.CategoryId == result.CategoryId
                    && s.ImageFilePath == result.ImageFilePath
                    && s.AudioFilePath == result.AudioFilePath
                    && s.Category.Name == result.CategoryName));
            }
        }*/

        [Fact]
        public async Task DeleteSong_WithValidData_ShouldWorkCorrect()
        {
            var getSongs = SongTestData.GetSongs;
            var songService = await this.CreateSongService(getSongs, new List<Comment>(), new List<Category>(), new List<ApplicationUser>());
            var songId = SongTestData.DeleteSongId;

            await songService.Delete(songId);

            Assert.False(this.context.Songs.Any(c => c.Id == songId));
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var getSongs = SongTestData.GetSongs;
            var getComments = SongTestData.Comments;
            var songService = await this.CreateSongService(getSongs, getComments,  new List<Category>(), new List<ApplicationUser>());
            var songId = SongTestData.GetById;

            var result = songService.GetById<SongViewModel>(songId);

            Assert.True(this.context.Songs.Any(s => s.Id == songId
                  && s.Name == result.Name
                  && s.ImageFilePath == result.ImageFilePath
                  && s.AudioFilePath == result.AudioFilePath
                  && s.CategoryId == result.CategoryId
                  //&& s.Text == result.Text
                  && s.Category.Name == result.CategoryName));
        }

        [Fact]
        public async Task IsOwnSong_WithValidData_ShouldeReturnTrue()
        {
            var getSongs = SongTestData.GetSongs;
            var songService = await this.CreateSongService(getSongs, new List<Comment>(), new List<Category>(), new List<ApplicationUser>());
            var songId = SongTestData.IsOwnSongIdTrue;
            var userId = SongTestData.IsOwnSongUserIdTrue;

            Assert.True(songService.IsOwn(songId, userId));
        }

        [Fact]
        public async Task IsOwnSong_WithValidData_ShouldeReturnFalse()
        {
            var getSongs = SongTestData.GetSongs;
            var songService = await this.CreateSongService(getSongs, new List<Comment>() ,new List<Category>(), new List<ApplicationUser>());
            var songId = SongTestData.IsOwnSongIdFalse;
            var userId = SongTestData.IsOwnSongUserIdFalse;
            var r = songService.IsOwn(songId, userId);
            Assert.False(r);
        }

        private async Task<SongService> CreateSongService(List<Song> songs, List<Comment> comments, List<Category> categories, List<ApplicationUser> users)
        {
            await this.context.Users.AddRangeAsync(users);
            await this.context.Categories.AddRangeAsync(categories);
            await this.context.Songs.AddRangeAsync(songs);
            await this.context.Comments.AddRangeAsync(comments);
            await this.context.SaveChangesAsync();
            var service = new SongService(this.context, new PaginationService());

            return service;
        }
    }
}
