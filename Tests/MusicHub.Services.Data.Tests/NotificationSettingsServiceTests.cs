namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.NotificationSetingModels;
    using Xunit;

    public class NotificationSettingsServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateNotificationSetting_WithValidData_ShouldWorkCorrect()
        {
            var notificationSettingsService = await this.CreateService(new List<NotificationSetting>());
            var model = NotificationSettingTestsData.CreateModel;
            var userId = NotificationSettingTestsData.CreateUserId;

            var notificationSettingId = await notificationSettingsService.Create(model, userId);

            Assert.True(this.context.NotificationSettings.Any(ns => ns.Type == model.Type
                && ns.UserId == userId
                && ns.Id == notificationSettingId));
        }

        [Fact]
        public async Task GetAllNotificationSetting_WithValidData_ShouldWorkCorrect()
        {
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var notificationSettingsService = await this.CreateService(notificationSettings);
            var userId = notificationSettings[0].UserId;
            var expected = this.context.NotificationSettings
                .Where(ns => ns.UserId == userId)
                .To<NotificationSettingsViewModel>()
                .ToList();

            var actual = notificationSettingsService.All<NotificationSettingsViewModel>(userId);

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetNotificationSettingByType_WithValidData_ShouldWorkCorrect()
        {
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var notificationSettingsService = await this.CreateService(notificationSettings);
            var userId = notificationSettings[0].UserId;
            var type = notificationSettings[0].Type;
            var expected = this.context.NotificationSettings
                .Where(ns => ns.UserId == userId
                    && ns.Type == type)
                .To<NotificationSettingsViewModel>()
                .FirstOrDefault();

            var actual = notificationSettingsService.GetByType<NotificationSettingsViewModel>(type, userId);

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task IsOwnedNotificationSetting_WithValidData_ShouldeReturnTrue()
        {
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var notificationSettingsService = await this.CreateService(notificationSettings);
            var userId = notificationSettings[0].UserId;
            var notificationSettingId = notificationSettings.Where(ns => ns.UserId == userId)
                .FirstOrDefault()
                .Id;
            Assert.True(notificationSettingsService.IsOwned(notificationSettingId, userId));
        }

        [Fact]
        public async Task IsOwnedNotificationSetting_WithValidData_ShouldeReturnFalse()
        {
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var notificationSettingsService = await this.CreateService(notificationSettings);
            var userId = notificationSettings[0].UserId;
            var notificationSettingId = notificationSettings.Where(ns => ns.UserId != userId)
               .FirstOrDefault()
               .Id;
            Assert.False(notificationSettingsService.IsOwned(notificationSettingId, userId));
        }

        [Fact]
        public async Task UpdateMusic_WithValidData_ShouldWorkCorrect()
        {
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var notificationSettingsService = await this.CreateService(notificationSettings);
            var notificationSettingId = notificationSettings[0].Id;
            var model = new NotificationSettingEditInputModel() { Enabled = !notificationSettings.Where(ns => ns.Id == notificationSettingId).FirstOrDefault().Enabled };

            await notificationSettingsService.Update(notificationSettingId, model);

            Assert.True(this.context.NotificationSettings.Any(ns => ns.Enabled == model.Enabled && ns.Id == notificationSettingId));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task IsEnabled_WithValidData_ShouldWorkCurrectly(bool isEnabled)
        {
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var notificationSettingsService = await this.CreateService(notificationSettings);
            var userId = notificationSettings.Where(ns => ns.Enabled == isEnabled)
                .FirstOrDefault()
                .UserId;
            var type = notificationSettings.Where(ns => ns.UserId == userId && ns.Enabled == isEnabled)
                .FirstOrDefault()
                .Type;

            var actual = notificationSettingsService.IsEnabled(type, userId);

            Assert.Equal(isEnabled,actual);
        }

        [Fact]
        public async Task IsEnabled_WithValidData_ShouldReturnTrue()
        {
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var notificationSettingsService = await this.CreateService(notificationSettings);
            var userId = notificationSettings[0].UserId;
            var userTypes = notificationSettings.Where(ns => ns.UserId == userId).Select(ns => ns.Type);
            var type = notificationSettings.Where(ns => !userTypes.Any(ut => ut == ns.Type))
                .FirstOrDefault()
                .Type;

            var actual = notificationSettingsService.IsEnabled(type, userId);

            Assert.True(actual);
        }

        private async Task<NotificationSettingsService> CreateService(List<NotificationSetting> notificationSettings)
        {
            var users = NotificationSettingTestsData.Users;
            await this.context.Users.AddRangeAsync(users);
            await this.context.NotificationSettings.AddRangeAsync(notificationSettings);
            await this.context.MusicLicenses.AddRangeAsync(new List<MusicLicense>());
            await this.context.SaveChangesAsync();
            var service = new NotificationSettingsService(this.context);
            return service;
        }
    }
}
