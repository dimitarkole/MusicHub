namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;
    using MusicHub.Common.Extensions;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.NotificationModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;

    public class NotificationsServiceTests : BaseTestClass
    {
        [Fact]
        public async Task Create_WithValidData_ShouldWorkCorrectly()
        {
            var notificationsService = await CreateNotificationsService(new List<Notification>());
            var model = NotificationTestsData.CreateModel;

            await notificationsService.Add(model);

            Assert.True(this.context.Notifications.Any(n => n.UserId == model.UserId
                && n.Content == model.Content));
        }

        /*[Fact]
        public async Task CreateRange_WithValidData_ShouldWorkCorrectly()
        {
            var notificationsService = await this.CreateNotificationsService(new List<Notification>());
            var models = NotificationTestsData.CreateRange;

            await notificationsService.AddRange(models);

            foreach (var model in models)
            {
                Assert.True(this.context.Notifications.Any(n => n.UserId == model.UserId
                    && n.Content == model.Content));
            }
        }  */

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetUserNotifications_WithValidData_ShouldReturnAllNotificationsOfUser(int page)
        {
            var notifications = NotificationTestsData.GetNotificationData();
            var notificationsService = await this.CreateNotificationsService(notifications);
            var userId = NotificationTestsData.UserId;
            var entitesPerPage = PaginationData.NotificationPerPage;
            var actual = notificationsService.GetUserNotifications<NotificationViewModel>(page, entitesPerPage, userId);
            var allNotifications = this.context.Notifications
                .Where(n => n.UserId == userId)
                .OrderBy(n => n.Seen)
                .ThenByDescending(n => n.CreatedOn)
                .To<NotificationViewModel>();
            var expected = new NotificationPageModel<NotificationViewModel>()
            {
                Notifications = allNotifications
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetNotificationsPagesCount(allNotifications.Count(), entitesPerPage),
            };

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task MarkAsSeen_WithValidData_ShouldWorkCorrectly()
        {

            var notifications = NotificationTestsData.GetNotificationData();
            var notificationsService = await this.CreateNotificationsService(notifications);
            var notificationId = notifications[0].Id;

            await notificationsService.MarkAsSeen(notificationId);

            Assert.True(this.context.Notifications
                .Any(n => n.Id == notificationId
                    && n.Seen == true));
        }

        [Fact]
        public async Task GetNotificationOwner_WithValidData_ShouldWorkCorrectly()
        {
            var notifications = NotificationTestsData.GetNotificationData();
            var notificationsService = await this.CreateNotificationsService(notifications);
            var notification = notifications[0];

            string resultId = notificationsService.GetNotificationOwner(notification.Id);

            Assert.Equal(notification.UserId, resultId);
        }

        private int GetNotificationsPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<NotificationsService> CreateNotificationsService(List<Notification> notifications)
        {
            var users = NotificationTestsData.Users;

            await this.context.Users.AddRangeAsync(users);
            await this.context.Notifications.AddRangeAsync(notifications);
            await this.context.SaveChangesAsync();
            var service = new NotificationsService(this.context, new PaginationService());

            return service;
        }
    }
}
