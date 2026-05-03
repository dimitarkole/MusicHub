using FluentAssertions;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Services.Messaging;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.MusicModels;
using MusicHub.Web.ViewModels.UserLoginHistoryModels;
using MusicHub.Web.ViewModels.Users;
using MyTested.AspNetCore.Mvc;
using SendGrid;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class UserLoginHistoryControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Filter_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = UserLoginHistoryTestsData.Users;
            var userId = users[0].Id;
            var userLoginHistories = UserLoginHistoryTestsData.GenUserLoginHistory();
            var filter = UserLoginHistoryTestsData.FilterModel;
            MyController<UserLoginHistoryController>
                .Instance()
                .WithData(userLoginHistories)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserLoginHistoryPageModel<UserLoginHistoryViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UserLoginHistoryPerPage;
                        var service = new UserLoginHistoryService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>(), From.Services<IEmailSender>());
                        var expected = service.Filter<UserLoginHistoryViewModel>(page, entitesPerPage, userId, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = UserLoginHistoryTestsData.Users;
            var userId = users[0].Id;
            var userLoginHistories = UserLoginHistoryTestsData.GenUserLoginHistory();
            var userLoginHistoryId = userLoginHistories[0].Id;
            MyController<UserLoginHistoryController>
                .Instance()
                .WithData(userLoginHistories)
                .WithData(users)
                .Calling(c => c.Get(userLoginHistoryId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserLoginHistoryViewModel>()
                    .Passing(actual =>
                    {
                        var service = new UserLoginHistoryService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>(), From.Services<IEmailSender>());
                        var expected = service.GetById<UserLoginHistoryViewModel>(userLoginHistoryId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }
    }
}
