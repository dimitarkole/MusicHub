namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.FollowModels;
    using Xunit;

    public class FollowServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateFollow_WithValidData_ShouldWorkCorrect()
        {
            var users = UserTestsData.Users;
            var followService = await this.CreateFollowService(users, new List<Follower>());
            var model = FollowTestsData.CreateModel;
            var userId = UserTestsData.Users[0].Id;
            await followService.Create(model, userId);

            Assert.True(this.context.Followers.Any(f => f.FollowedId == model.FollowedId
                && f.FollowingId == userId));
        }

        [Fact]
        public async Task AllFollowed_WithValidData_ShouldWorkCorrect()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var followedUserId = FollowTestsData.FollowedUserId;

            var results = followService.AllFollowed<FollowViewModel>(followedUserId).ToList();
            var expextedCount = this.context.Followers.Where(f => f.FollowingId == followedUserId)
                  .Count();

            Assert.Equal(expextedCount, results.Count);
            foreach (var result in results)
            {
                Assert.True(this.context.Followers.Any(f => f.Id == result.Id
                    && f.FollowedId == result.Followed.Id
                    && f.Followed.UserName == result.Followed.UserName
                    && f.Followed.ImageUrl == result.Followed.ImageUrl));
            }
        }

        [Fact]
        public async Task AllFollowers_WithValidData_ShouldWorkCorrect()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var followingUserId = FollowTestsData.FollowingUserId;

            var results = followService.AllFollowed<FollowedViewModel>(followingUserId).ToList();
            var expextedCount = this.context.Followers.Where(f => f.FollowedId == followingUserId)
                  .Count();

            Assert.Equal(expextedCount, results.Count);
            foreach (var result in results)
            {
                Assert.True(this.context.Followers.Any(f => f.Id == result.Id
                    && f.FollowingId == result.Following.Id
                    && f.Following.UserName == result.Following.UserName
                    && f.Following.ImageUrl == result.Following.ImageUrl));
            }
        }

        [Fact]
        public async Task Filter_WithValidData_ShouldWorkCorrect()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var followedUserId = FollowTestsData.FollowedUserId;
            var filterModel = FollowTestsData.FollowFilterModel;
            var results = followService.Filter<FollowViewModel>(filterModel, followedUserId).ToList();

            var followers = this.context.Followers
                  .Where(f => f.FollowingId == followedUserId);
            followers = this.FilteringFollowed(filterModel, followers);

            Assert.Equal(followers.ToList().Count, results.Count);
            foreach (var result in results)
            {
                Assert.True(this.context.Followers.Any(f => f.Id == result.Id
                    && f.FollowedId == result.Followed.Id
                    && f.Followed.UserName == result.Followed.UserName
                    && f.Followed.ImageUrl == result.Followed.ImageUrl));
            }
        }

        [Fact]
        public async Task Delete_WithValidData_ShouldWorkCorrect()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var deleteFollowId = FollowTestsData.DeleteFollowId;
            var results = followService.Delete(deleteFollowId);

            Assert.False(this.context.Followers.Any(f => f.Id == deleteFollowId));
        }

        [Fact]
        public async Task IsFollowed_WithValidData_ShouldReturnTrue()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var followedUserId = FollowTestsData.FollowedUserIdIsFollowedTrue;
            var followingUserId = FollowTestsData.FollowingUserIdIsFollowedTrue;
            var results = followService.IsFollowed(followedUserId, followingUserId);

            Assert.True(results);
        }

        [Fact]
        public async Task IsFollowed_WithValidData_ShouldReturnFalse()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var followedUserId = FollowTestsData.FollowedUserIdIsFollowedFalse;
            var followingUserId = FollowTestsData.FollowingUserIdIsFollowedFalse();
            var results = followService.IsFollowed(followedUserId, followingUserId);

            Assert.False(results);
        }

        [Fact]
        public async Task GetFollow_Following_WithValidData_ShouldWorkCorrect()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var followedUserId = FollowTestsData.FollowedUserIdIsFollowedTrue;
            var followingUserId = FollowTestsData.FollowingUserIdIsFollowedTrue;
            var results = followService.GetFollow<FollowedViewModel>(followedUserId, followingUserId);
            var expectedResult = this.context.Followers
                    .Where(f =>
                        f.FollowedId == followedUserId
                        && f.FollowingId == followingUserId)
                    .To<FollowedViewModel>()
                    .FirstOrDefault();
            Assert.True(results.Id == expectedResult.Id
                && results.Following.UserName == expectedResult.Following.UserName
                && results.Following.Id == expectedResult.Following.Id
                && results.Following.ImageUrl == expectedResult.Following.ImageUrl);
        }

        [Fact]
        public async Task GetFollow_Follower_WithValidData_ShouldWorkCorrect()
        {
            var users = UserTestsData.Users;
            var follows = FollowTestsData.GetFollows;
            var followService = await this.CreateFollowService(users, follows);
            var followedUserId = FollowTestsData.FollowedUserIdIsFollowedTrue;
            var followingUserId = FollowTestsData.FollowingUserIdIsFollowedTrue;
            var results = followService.GetFollow<FollowViewModel>(followedUserId, followingUserId);
            var expectedResult = this.context.Followers
                    .Where(f =>
                        f.FollowedId == followedUserId
                        && f.FollowingId == followingUserId)
                    .To<FollowViewModel>()
                    .FirstOrDefault();
            Assert.True(results.Id == expectedResult.Id
                && results.Followed.UserName == expectedResult.Followed.UserName
                && results.Followed.Id == expectedResult.Followed.Id
                && results.Followed.ImageUrl == expectedResult.Followed.ImageUrl);
        }

        private IQueryable<Follower> FilteringFollowed(FollowFilterInputModel filter, IQueryable<Follower> follows)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                follows = follows.Where(s => s.Followed.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                follows = follows.Where(s => s.Followed.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                follows = follows.Where(s => s.Followed.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return follows;
        }

        private async Task<FollowService> CreateFollowService(List<ApplicationUser> users, List<Follower> followers)
        {
            await this.context.Users.AddRangeAsync(users);
            await this.context.Followers.AddRangeAsync(followers);
            await this.context.SaveChangesAsync();
            var service = new FollowService(this.context);

            return service;
        }
    }
}
