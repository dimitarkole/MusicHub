using MusicHub.Data.Models;
using MusicHub.Tests.TestData;
using MusicHub.Web.ViewModels.NotificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Common.Extensions;
using MusicHub.Services.Mapping;

namespace MusicHub.Services.Data.Tests
{
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

        [Fact]
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
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetUserNotifications_WithValidData_ShouldReturnAllNotificationsOfUser(int page)
        {
            var notifications = NotificationTestsData.GetNotificationData();
            var notificationsService = await this.CreateNotificationsService(notifications);
            var userId = NotificationTestsData.UserId;

            var entitesPerPage = PaginationData.NotificationPerPage;
            var result = notificationsService.GetUserNotifications<NotificationViewModel>(page, entitesPerPage, userId);

            var userNotifications = this.context.Notifications
              .Where(n => n.UserId == userId && !n.Seen)
              .OrderBy(n => n.Seen == false)
              .ThenByDescending(n => n.CreatedOn)
              .To<NotificationViewModel>();
            var expectedResult = userNotifications.GetPage(page, entitesPerPage)
               .ToList();
            var expectedCountPages = this.GetNotificationPagesCount(expectedResult.Count(), entitesPerPage);

            this.CheckAllNotificationModelsIsEqual(expectedResult, result);
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

        private int GetNotificationPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private void CheckAllNotificationModelsIsEqual(List<NotificationViewModel> expectedResult, NotificationAllViewModel<NotificationViewModel> result)
        {
            Assert.Equal(expectedResult.Count, result.Notifications.Count());
            var notifications = result.Notifications.ToList();
            for (int i = 0; i < expectedResult.Count(); i++)
            {
                var expectedNotification = expectedResult[i];
                var resultNotification = notifications[i];
                Assert.True(this.CheckNotificationViewModelIsEqual(expectedNotification, resultNotification));
            }
        }

        private bool CheckNotificationViewModelIsEqual(NotificationViewModel expectedNotification, NotificationViewModel resultNotification)
            => expectedNotification.Id == resultNotification.Id
                && expectedNotification.CreatedOn == resultNotification.CreatedOn
                && expectedNotification.Content == resultNotification.Content;

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
