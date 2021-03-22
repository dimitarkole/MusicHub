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

namespace MusicHub.Services.Data.Tests
{
    public class FollowServiceTests : BaseTestClass
    {
        // followings - users who I following
        // followers - user who followed me

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
            Assert.True(followService.IsFollowing(follow.FollowerId, follow.FollowingId));
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

            var expectedResult = this.context.Follows
                .Where(f => f.FollowerId == userId)
                .To<FollowingViewModel>()
                .ToList();

            var entitesPerPage = PaginationData.FollowingsPerPage;

            var result = followService.AllFollowing<FollowingViewModel>(page, entitesPerPage, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);

            expectedResult = expectedResult.GetPage(page, entitesPerPage)
               .ToList();

            this.CheckFollowingViewModelsIsEqual(page, expectedResult, result, countPages);
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

            var expectedResult = this.context.Follows
                .Where(f => f.FollowingId == userId)
                .To<FollowerViewModel>()
                .ToList();

            var entitesPerPage = PaginationData.FollowingsPerPage;

            var result = followService.AllFollowers<FollowerViewModel>(page, entitesPerPage, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);

            expectedResult = expectedResult.GetPage(page, entitesPerPage)
               .ToList();

            this.CheckFollowerViewModelsIsEqual(page, expectedResult, result, countPages);
        }

        [Fact]
        public async Task GetFollowingInfo_WithValidData_ShouldWorkCorrect()
        {
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var follow = follows[0];

            var result = followService.GetFollow<FollowerViewModel>(follow.FollowingId, follow.FollowerId);

            Assert.Equal(follow.Id, result.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowing_WithValidData_ShouldeReturn(int page)
        {
            var entitesPerPage = PaginationData.FollowingsPerPage;
            var filterModel = FollowTestsData.FilterModelUserName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var expectedResult = this.context.Follows
                .Where(f => f.FollowerId == userId);

            expectedResult = this.FilteringFollowings(filterModel, expectedResult);
            expectedResult = this.OrederFollowings(filterModel.OrderMethod, expectedResult);

            var result = followService.FilterFollowings<FollowingViewModel>(page, entitesPerPage, filterModel, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);
            var expecetdResultT = expectedResult
                .To<FollowingViewModel>()
                .GetPage(page, entitesPerPage)
                .ToList();

            this.CheckFollowingViewModelsIsEqual(page, expecetdResultT, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowers_WithValidData_ShouldeReturn(int page)
        {
            var entitesPerPage = PaginationData.FollowingsPerPage;
            var filterModel = FollowTestsData.FilterModelUserName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var expectedResult = this.context.Follows
                .Where(f => f.FollowingId == userId);

            expectedResult = this.FilteringFollowers(filterModel, expectedResult);
            expectedResult = this.OrederFollows(filterModel.OrderMethod, expectedResult);

            var result = followService.FilterFollowers<FollowerViewModel>(page, entitesPerPage, filterModel, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);

            var expectedResultT = expectedResult.GetPage(page, entitesPerPage)
                .To<FollowerViewModel>()
                .ToList();

            this.CheckFollowerViewModelsIsEqual(page, expectedResultT, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowingLastName_WithValidData_ShouldeReturn(int page)
        {
            var entitesPerPage = PaginationData.FollowingsPerPage;
            var filterModel = FollowTestsData.FilterModelLastName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var expectedResult = this.context.Follows
                .Where(f => f.FollowerId == userId);

            expectedResult = this.FilteringFollowings(filterModel, expectedResult);
            expectedResult = this.OrederFollowings(filterModel.OrderMethod, expectedResult);

            var result = followService.FilterFollowings<FollowingViewModel>(page, entitesPerPage, filterModel, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);
            var expecetdResultT = expectedResult
                .To<FollowingViewModel>()
                .GetPage(page, entitesPerPage)
                .ToList();

            this.CheckFollowingViewModelsIsEqual(page, expecetdResultT, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowersLastName_WithValidData_ShouldeReturn(int page)
        {
            var entitesPerPage = PaginationData.FollowingsPerPage;
            var filterModel = FollowTestsData.FilterModelLastName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var expectedResult = this.context.Follows
                .Where(f => f.FollowingId == userId);

            expectedResult = this.FilteringFollowers(filterModel, expectedResult);
            expectedResult = this.OrederFollows(filterModel.OrderMethod, expectedResult);

            var result = followService.FilterFollowers<FollowerViewModel>(page, entitesPerPage, filterModel, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);

            var expectedResultT = expectedResult.GetPage(page, entitesPerPage)
                .To<FollowerViewModel>()
                .ToList();

            this.CheckFollowerViewModelsIsEqual(page, expectedResultT, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowingFirstName_WithValidData_ShouldeReturn(int page)
        {
            var entitesPerPage = PaginationData.FollowingsPerPage;
            var filterModel = FollowTestsData.FilterModelFirstName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var expectedResult = this.context.Follows
                .Where(f => f.FollowerId == userId);

            expectedResult = this.FilteringFollowings(filterModel, expectedResult);
            expectedResult = this.OrederFollowings(filterModel.OrderMethod, expectedResult);

            var result = followService.FilterFollowings<FollowingViewModel>(page, entitesPerPage, filterModel, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);
            var expecetdResultT = expectedResult
                .To<FollowingViewModel>()
                .GetPage(page, entitesPerPage)
                .ToList();

            this.CheckFollowingViewModelsIsEqual(page, expecetdResultT, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task FilterFollowersFirstName_WithValidData_ShouldeReturn(int page)
        {
            var entitesPerPage = PaginationData.FollowingsPerPage;
            var filterModel = FollowTestsData.FilterModelFirstName;
            var follows = FollowTestsData.Follows();
            var followService = await this.CreateFollowService(follows);
            var users = UserTestsData.Users;
            var userId = users[0].Id;

            var expectedResult = this.context.Follows
                .Where(f => f.FollowingId == userId);

            expectedResult = this.FilteringFollowers(filterModel, expectedResult);
            expectedResult = this.OrederFollows(filterModel.OrderMethod, expectedResult);

            var result = followService.FilterFollowers<FollowerViewModel>(page, entitesPerPage, filterModel, userId);

            var countPages = this.GetFollowPagesCount(expectedResult.Count(), entitesPerPage);

            var expectedResultT = expectedResult.GetPage(page, entitesPerPage)
                .To<FollowerViewModel>()
                .ToList();

            this.CheckFollowerViewModelsIsEqual(page, expectedResultT, result, countPages);
        }

        private void CheckFollowerViewModelsIsEqual(int page, List<FollowerViewModel> expectedResult, FollowersAllViewModel<FollowerViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultFollowers = result.Followers.ToList();
            Assert.Equal(resultFollowers.Count, expectedResult.Count);

            for (int i = 0; i < expectedResult.Count(); i++)
            {
                var expected = expectedResult[i];
                var resultFollower = resultFollowers[i];
                Assert.True(this.CheckFollowerViewModelsIsEqual(expected, resultFollower));
            }
        }

        private void CheckFollowingViewModelsIsEqual(int page, List<FollowingViewModel> expectedResult, FollowingsAllViewModel<FollowingViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultFollowings = result.Followings.ToList();
            Assert.Equal(resultFollowings.Count, expectedResult.Count);

            for (int i = 0; i < expectedResult.Count(); i++)
            {
                var expected = expectedResult[i];
                var resultFollowing = resultFollowings[i];
                Assert.True(this.CheckFollowingViewModelsIsEqual(expected, resultFollowing));
            }
        }

        private bool CheckFollowingViewModelsIsEqual(FollowingViewModel expected, FollowingViewModel result)
            => expected.Following.Id == result.Following.Id
                        && expected.Following.UserName == result.Following.UserName
                        && expected.Following.ImageUrl == result.Following.ImageUrl
                        && expected.Id == result.Id;

        private bool CheckFollowerViewModelsIsEqual(FollowerViewModel expected, FollowerViewModel result)
            => expected.Follower.Id == result.Follower.Id
                && expected.Follower.UserName == result.Follower.UserName
                && expected.Follower.ImageUrl == result.Follower.ImageUrl
                && expected.Id == result.Id;

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
