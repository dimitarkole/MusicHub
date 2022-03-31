using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.NotificationSetingModels;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;

namespace MusicHub.Web.Tests.Controllers
{
    public class NotificationSettingControllerTests
    {
        [Fact]
        public void GetAllNotificationSettings_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = NotificationSettingTestsData.CreateUserId;
            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithData(notificationSettings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<IEnumerable<NotificationSettingsViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new NotificationSettingsService(From.Services<ApplicationDbContext>());
                        var expected = service.All<NotificationSettingsViewModel>(userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }


        [Fact]
        public void GetNotificationSettingsByType_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = notificationSettings[0].UserId;
            var notificationSettingType = new NotificationSettingsTypeInputModel() { Type = notificationSettings[0].Type };
            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithData(notificationSettings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetByType(notificationSettingType))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<NotificationSettingsViewModel>()
                    .Passing(actual =>
                    {   
                        var service = new NotificationSettingsService(From.Services<ApplicationDbContext>());
                        var expected = service.GetByType<NotificationSettingsViewModel>(notificationSettingType.Type, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsEnable_WithDataInTheDb_ShouldReturnOkWithCorrectData(bool expected)
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = notificationSettings.Where(ns => ns.Enabled == expected)
                .FirstOrDefault()
                .UserId;
            var notificationSettingType = new NotificationSettingsTypeInputModel() { Type = notificationSettings.Where(ns => ns.UserId == userId && ns.Enabled == expected)
                .FirstOrDefault()
                .Type,
            };

            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithData(notificationSettings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsEnabled(notificationSettingType))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.Equal(expected, actual);
                    }));
        }

        [Fact]
        public void IsEnable_WithDataInTheDb_ShouldReturnFalse()
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = notificationSettings[0].UserId;
            var userTypes = notificationSettings.Where(ns => ns.UserId == userId).Select(ns => ns.Type);
            var type = notificationSettings.Where(ns => !userTypes.Any(ut => ut == ns.Type))
                .FirstOrDefault()
                .Type;
            var notificationSettingType = new NotificationSettingsTypeInputModel()
            {
                Type = type
            };

            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithData(notificationSettings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsEnabled(notificationSettingType))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void CreateNotificationSetting_WithDataInTheDb_ShouldReturnOkWithPlaylistId()
        {
            var users = NotificationSettingTestsData.Users;
            var model = NotificationSettingTestsData.CreateModel;
            var userId = NotificationSettingTestsData.CreateUserId;
            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(StatusCodes.Status201Created)
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if (property.Name == "notificationSettingId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().NotificationSettings
                                    .Any(ns => ns.Id == property.GetValue(obj).ToString()
                                        && ns.Type == model.Type
                                        && ns.UserId == userId
                                        && ns.Enabled == model.Enabled));
                            }
                        }
                    }));
        }

        [Fact]
        public void EditNotificationSettings_WithDataInTheDb_ShouldReturnOk()
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = notificationSettings[0].UserId;
            var notificationSettingId = notificationSettings.Where(ns => ns.UserId == userId)
                .FirstOrDefault()
                .Id;

            var model = new NotificationSettingEditInputModel() { Enabled = !notificationSettings.Where(ns => ns.Id == notificationSettingId).FirstOrDefault().Enabled };

            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithData(notificationSettings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(notificationSettingId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().NotificationSettings
             .Any(ns => ns.Id == notificationSettingId
                 && ns.ModifiedOn != null));
        }

        [Fact]
        public void EditNotificationSettings_WithDataInTheDb_ShouldReturnForbid()
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = notificationSettings[0].UserId;
            var notificationSettingId = notificationSettings.Where(ns => ns.UserId != userId)
                .FirstOrDefault()
                .Id;

            var model = new NotificationSettingEditInputModel() { Enabled = !notificationSettings.Where(ns => ns.Id == notificationSettingId).FirstOrDefault().Enabled };

            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithData(notificationSettings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(notificationSettingId, model))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().NotificationSettings
             .Any(ns => ns.Id == notificationSettingId
                 && ns.ModifiedOn == null));
        }

        [Fact]
        public void DeleteNotificationSettings_WithDataInTheDb_ShouldReturnOk()
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = notificationSettings[0].UserId;
            var notificationSettingId = notificationSettings.Where(ns => ns.UserId == userId)
                .FirstOrDefault()
                .Id;

            MyController<NotificationSettingController>
               .Instance()
               .WithData(users)
               .WithData(notificationSettings)
               .WithUser(user => user
                   .WithClaim(ClaimTypes.NameIdentifier, userId))
               .Calling(c => c.Delete(notificationSettingId))
               .ShouldReturn()
               .Ok();

            Assert.False(From.Services<ApplicationDbContext>().NotificationSettings
                .Any(ns => ns.Id == notificationSettingId));
        }

        [Fact]
        public void DeleteNotificationSettings_WithDataInTheDb_ShouldReturnForbid()
        {
            var users = NotificationSettingTestsData.Users;
            var notificationSettings = NotificationSettingTestsData.NotificationSettings();
            var userId = notificationSettings[0].UserId;
            var notificationSettingId = notificationSettings.Where(ns => ns.UserId != userId)
                .FirstOrDefault()
                .Id;

            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithData(notificationSettings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(notificationSettingId))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().NotificationSettings
             .Any(ns => ns.Id == notificationSettingId));
        }

        /*[Fact]
        public void Create_WhitCurrectData_ShouldReturnBadRequestRequared()
        {
            var users = NotificationSettingTestsData.Users;
            var model = NotificationSettingTestsData.CreateModelBadRequest;
            var userId = NotificationSettingTestsData.CreateUserId;
            MyController<NotificationSettingController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
               .BadRequest(result => result
                    .WithErrorOfType<List<string>>()
                    .Passing(actualContacts =>
                    {
                        if (string.IsNullOrEmpty(model.Enabled.ToString()))
                        {
                            Assert.Contains($"The field Enabled is requared", actualContacts);
                        }
                        if (string.IsNullOrEmpty(model.Type.ToString()))
                        {
                            Assert.Contains($"The field Type is requared", actualContacts);
                        }
                    }));

            Assert.False(From.Services<ApplicationDbContext>().NotificationSettings
                .Any(ns => ns.Type == model.Type
                  && ns.UserId == userId
                  && ns.Enabled == model.Enabled));
        }*/
    }
}
