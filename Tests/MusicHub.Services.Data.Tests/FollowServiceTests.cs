using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using MusicHub.Tests.TestData;
using MusicHub.Data.Models;
using Xunit;
using MusicHub.Web.ViewModels.FollowModels;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Common;
using FluentAssertions;

namespace MusicHub.Services.Data.Tests
{
    public class FollowServiceTests : BaseTestClass
    {
        // followings - users who I following
        // followers - user who followed me
        public int EntitesPerPage = PaginationData.FollowingsPerPage;

        [Fact]
        public async Task CreateFollow_WithValidData_ShouldWorkCorrect()
        {
            var followService = await this.CreateFollowService(new List<Follow>());
            var model = FollowTestsData.CreateModel;
            var userId = FollowTestsData.CreateUserId;

            await followService.Create(model, userId);

            Assert.True(this.context.Follows.Any(f => f.FollowingId == userId
                && f.FollowerId == model.FollowerId));
        }

        [Fact]
        public async Task IsOwnedTrue_WithValidData_ShouldWorkCorrect()
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var follow = follows[0];
            Assert.True(followService.IsOwned(follow.Id, follow.FollowingId));
        }

        [Fact]
        public async Task IsOwnedFalse_WithValidData_ShouldWorkCorrect()
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var followingId = follows[0].FollowingId;
            var followId = follows
                .Where(f => f.FollowingId != followingId)
                .FirstOrDefault()
                .Id;

            Assert.False(followService.IsOwned(followId, followingId));
        }

        [Fact]
        public async Task IsFollowingTrue_WithValidData_ShouldWorkCorrect()
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var follow = follows[0];
            Assert.True(followService.IsFollowing(follow.FollowingId, follow.FollowerId));
        }

        [Fact]
        public async Task IsFollowingFalse_WithValidData_ShouldWorkCorrect()
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var userId = follows[0].FollowingId;
            var followId = follows
                .Where(f => !f.Follower.Followings.Any(f2 => f2.FollowerId != userId))
                .FirstOrDefault()
                .FollowerId;

            Assert.False(followService.IsFollowing(followId, userId));
        }

        [Fact]
        public async Task DeleteFollow_WithValidData_ShouldWorkCorrect()
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var followId = follows[0].Id;

            await followService.Delete(followId);

            Assert.False(this.context.Follows.Any(f => f.Id == followId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllFollowing_WithValidData_ShouldeReturn(int page)
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;
            var actual = followService.AllFollowing<FollowingViewModel>(page, this.EntitesPerPage, userId);

            this.CheckFilterFollowings(page, new FollowFilterInputModel(), userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllFollowers_WithValidData_ShouldeReturn(int page)
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = followService.AllFollowers<FollowerViewModel>(page, this.EntitesPerPage, userId);

            this.CheckFilterFollowers(page, new FollowFilterInputModel(), userId, actual);
        }

        [Fact]
        public async Task GetFollowingInfo_WithValidData_ShouldWorkCorrect()
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var follow = follows[0];

            var result = followService.GetFollow<FollowerViewModel>(follow.FollowerId, follow.FollowingId);

            Assert.Equal(follow.Id, result.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowing_WithValidData_ShouldeReturn(int page)
        {
            var filter = FollowTestsData.FilterModelUserName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = followService.FilterFollowings<FollowingViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterFollowings(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowers_WithValidData_ShouldeReturn(int page)
        {
            var filter = FollowTestsData.FilterModelUserName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = followService.FilterFollowers<FollowerViewModel>(page, this.EntitesPerPage, filter, userId);
            this.CheckFilterFollowers(page, filter, userId, actual);
        }

        private void CheckFilterFollowers(int page, FollowFilterInputModel filter, string userId, FollowersAllViewModel<FollowerViewModel> actual)
        {
            var followers = this.context.Follows
                            .Where(f => f.FollowingId == userId);
            followers = this.FilteringFollowers(filter, followers);
            followers = this.OrederFollows(filter.OrderMethod, followers);
            var followersT = followers.To<FollowerViewModel>();
            var expected = new FollowersAllViewModel<FollowerViewModel>()
            {
                Followers = followersT
                    .GetPage(page, this.EntitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetFollowPagesCount(followersT.Count(), this.EntitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        private void CheckFilterFollowings(int page, FollowFilterInputModel filter, string userId, FollowingsAllViewModel<FollowingViewModel> actual)
        {
            var followings = this.context.Follows
                   .Where(f => f.FollowerId == userId);

            followings = this.FilteringFollowings(filter, followings);
            followings = this.OrederFollowings(filter.OrderMethod, followings);
            var followersT = followings.To<FollowingViewModel>();

            var expected = new FollowingsAllViewModel<FollowingViewModel>()
            {
                Followings = followersT
                    .GetPage(page, this.EntitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetFollowPagesCount(followings.Count(), this.EntitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowingLastName_WithValidData_ShouldeReturn(int page)
        {
            var filter = FollowTestsData.FilterModelLastName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = followService.FilterFollowings<FollowingViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterFollowings(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowersLastName_WithValidData_ShouldeReturn(int page)
        {
            var filter = FollowTestsData.FilterModelLastName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = followService.FilterFollowers<FollowerViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterFollowers(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowingFirstName_WithValidData_ShouldeReturn(int page)
        {
            var filter = FollowTestsData.FilterModelFirstName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = followService.FilterFollowings<FollowingViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterFollowings(page, filter, userId, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowersFirstName_WithValidData_ShouldeReturn(int page)
        {
            var filter = FollowTestsData.FilterModelFirstName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var actual = followService.FilterFollowers<FollowerViewModel>(page, this.EntitesPerPage, filter, userId);

            this.CheckFilterFollowers(page, filter, userId, actual);
        }

        private int GetFollowsCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private int GetFollowPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private IQueryable<Follow> FilteringFollowings(FollowFilterInputModel filter, IQueryable<Follow> follows)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                follows = follows.Where(s => s.Following.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                follows = follows.Where(s => s.Following.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                follows = follows.Where(s => s.Following.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return follows;
        }

        private IQueryable<Follow> FilteringFollowers(FollowFilterInputModel filter, IQueryable<Follow> follows)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                follows = follows.Where(s => s.Follower.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                follows = follows.Where(s => s.Follower.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                follows = follows.Where(s => s.Follower.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return follows;
        }

        private IQueryable<Follow> OrederFollows(OrderMethod orderMethod, IQueryable<Follow> follows)
        {
            if (orderMethod == OrderMethod.NameAsc)
            {
                follows = follows.OrderBy(f => f.Follower.UserName);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                follows = follows.OrderByDescending(f => f.Follower.UserName);
            }

            return follows;
        }

        private IQueryable<Follow> OrederFollowings(OrderMethod orderMethod, IQueryable<Follow> follows)
        {
            if (orderMethod == OrderMethod.NameDesc)
            {
                follows = follows.OrderByDescending(f => f.Following.UserName);
            }
            else
            {
                follows = follows.OrderBy(f => f.Following.UserName);
            }

            return follows;
        }

        private async Task<FollowService> CreateFollowService(List<Follow> follows)
        {
            var users = UserTestsData.Users;
            await this.context.Users.AddRangeAsync(users);
            await this.context.Follows.AddRangeAsync(follows);
            await this.context.SaveChangesAsync();
            var service = new FollowService(this.context, new PaginationService());

            return service;
        }
    }
}
