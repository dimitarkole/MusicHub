namespace MusicHub.Services.Data.Tests
{
    using FluentAssertions;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Services.Messaging;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.MusicReactionModels;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using MusicHub.Web.ViewModels.ReportMusicModels;
    using MusicHub.Web.ViewModels.UserLoginHistoryModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using static MusicHub.Common.GlobalConstants;

    public class UserLoginHistoryServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateByModel_WithValidData_ShouldWorkCorrect()
        {
            var service = await this.CreateService(new List<UserLoginHistory>());
            var model = UserLoginHistoryTestsData.CreateModel;
            var userId = UserLoginHistoryTestsData.CreateUserId;

            await service.Create(userId, model);

            Assert.True(this.context.UserLoginHistories.Any(h => h.UserId == userId
                && h.UserAgent == model.UserAgent
                && h.LoginDate == model.LoginDate
                && h.IpAddress == model.IpAddress));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Filter_WithValidData_ShouldeReturnLikedMusics(int page)
        {
            var userLoginHistory = UserLoginHistoryTestsData.GenUserLoginHistory();
            var service = await this.CreateService(userLoginHistory);
            var entitesPerPage = PaginationData.UserLoginHistoryPerPage;
            var userId = UserLoginHistoryTestsData.UserId;
            var filter = UserLoginHistoryTestsData.FilterModel;

            this.CheckFilterResult(page, service, userId, entitesPerPage, filter);
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var userLoginHistory = UserLoginHistoryTestsData.GenUserLoginHistory();
            var service = await this.CreateService(userLoginHistory);
            var userLoginHistoryId = userLoginHistory[0].Id;
            var actual = service.GetById<UserLoginHistoryViewModel>(userLoginHistoryId);
            var expected = this.context.UserLoginHistories
                .Where(h => h.Id == userLoginHistoryId)
                .To<UserLoginHistoryViewModel>()
                .FirstOrDefault();

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task Delete_WithValidData_ShouldWorkCorrect()
        {
            var userLoginHistory = UserLoginHistoryTestsData.GenUserLoginHistory();
            var service = await this.CreateService(userLoginHistory);
            var userLoginHistoryId = userLoginHistory[0].Id;

            await service.Delete(userLoginHistoryId);

            Assert.False(this.context.UserLoginHistories.Any(c => c.Id == userLoginHistoryId));
        }

        [Fact]
        public async Task Create_WithValidData_ShouldWorkCorrect()
        {
            var service = await this.CreateService(new List<UserLoginHistory>());
            var model = UserLoginHistoryTestsData.CreateModel;
            var userId = UserLoginHistoryTestsData.CreateUserId;

            await service.Create(userId, model.IpAddress, model.UserAgent);

            Assert.True(this.context.UserLoginHistories.Any(h => h.UserId == userId
                && h.UserAgent == model.UserAgent
                && h.IpAddress == model.IpAddress));
        }

        [Fact]
        public async Task HandleLoginAsync_ShouldNotCreateHistory_WhenIpIsNull()
        {
            var service = await this.CreateService(new List<UserLoginHistory>());
            var user = this.context.Users.First();
            await service.HandleLoginAsync(user, null, "agent");
            Assert.Empty(context.UserLoginHistories);
        }

        [Fact]
        public async Task HandleLoginAsync_ShouldCreateHistory_WhenIpIsNew()
        {
            var service = await this.CreateService(new List<UserLoginHistory>());
            var user = this.context.Users.First();
            await service.HandleLoginAsync(user, "1.1.1.1", "agent");
            Assert.Single(this.context.UserLoginHistories);
            var record = this.context.UserLoginHistories.First();
            Assert.Equal(user.Id, record.UserId);
            Assert.Equal("1.1.1.1", record.IpAddress);
            Assert.Equal("agent", record.UserAgent);
        }

        [Fact]
        public async Task HandleLoginAsync_ShouldCreateHistory_WhenIpIsKnown()
        {
            var userLoginHistory = UserLoginHistoryTestsData.GenUserLoginHistory();
            var service = await this.CreateService(userLoginHistory);
            var user = this.context.Users.First();
            var history = this.context.UserLoginHistories
                .Where(h => h.UserId == user.Id)
                .First();
            var oldUserLoginHistoriesCount = this.context.UserLoginHistories.Count();
            await service.HandleLoginAsync(user, history.IpAddress, "agent");

            Assert.Equal(oldUserLoginHistoriesCount + 1, this.context.UserLoginHistories.Count());
        }

        private void CheckFilterResult(int page, UserLoginHistoryService service, string userId, int entitesPerPage, UserLoginHistoryFilterInputModel filter)
        {
            IQueryable<UserLoginHistory> userLoginHistory = this.context.UserLoginHistories;
            if (!string.IsNullOrEmpty(userId))
            {
                userLoginHistory = userLoginHistory.Where(s => s.UserId == userId);
            }

            userLoginHistory = this.Filtering(filter, userLoginHistory);
            userLoginHistory = userLoginHistory.OrderByDescending(l => l.LoginDate);

            var expected = new UserLoginHistoryPageModel<UserLoginHistoryViewModel>()
            {
                UserLoginHistory = userLoginHistory
                    .To<UserLoginHistoryViewModel>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetUserLoginHistoryPagesCount(userLoginHistory.Count(), entitesPerPage),
            };

            var actual = service.Filter<UserLoginHistoryViewModel>(page, entitesPerPage, userId, filter);

            expected.Should().BeEquivalentTo(actual);
        }

        private int GetUserLoginHistoryPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private IQueryable<UserLoginHistory> Filtering(UserLoginHistoryFilterInputModel filter, IQueryable<UserLoginHistory> userLoginHistory)
        {
            if (filter.LoginDateFrom != null && filter.LoginDateFrom != default(DateTime))
            {
                userLoginHistory = userLoginHistory.Where(h => h.LoginDate >= filter.LoginDateFrom);
            }

            if (filter.LoginDateTo != null && filter.LoginDateTo != default(DateTime))
            {
                userLoginHistory = userLoginHistory.Where(h => h.LoginDate <= filter.LoginDateTo);
            }

            return userLoginHistory;
        }

        private async Task<UserLoginHistoryService> CreateService(List<UserLoginHistory> userLoginHistory)
        {
            var users = UserLoginHistoryTestsData.Users;
            await this.context.Users.AddRangeAsync(users);
            await this.context.UserLoginHistories.AddRangeAsync(userLoginHistory);
            await this.context.SaveChangesAsync();
            var service = new UserLoginHistoryService(this.context, new PaginationService(), new EmailSender());

            return service;
        }

        private bool IsKnownIpAsync(string userId, string ipAddress)
        {
            return this.context.UserLoginHistories
                .Any(x => x.UserId == userId && x.IpAddress == ipAddress);
        }
    }
}
