using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.NotificationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public static class NotificationTestsData
    {
        public static List<User> Users => UserTestsData.Users;

        public static string UserId => Users[0].Id;

        public static NotificationInputModel CreateModel => new NotificationInputModel()
        {
            Content = "content",
            UserId = Users[0].Id,
        };

        public static List<NotificationInputModel> CreateRange => new List<NotificationInputModel>()
        {
            new NotificationInputModel(){ Content = "content1", UserId = UserTestsData.Users[0].Id },
            new NotificationInputModel(){ Content = "content2", UserId = UserTestsData.Users[2].Id },
            new NotificationInputModel(){ Content = "content3", UserId = UserTestsData.Users[1].Id },
            new NotificationInputModel(){ Content = "content4", UserId = UserTestsData.Users[0].Id },
            new NotificationInputModel(){ Content = "content5", UserId = UserTestsData.Users[0].Id },
        };

        public static List<Notification> GetNotificationData()
        {
            var notifications = new List<Notification>();
            var random = new Random();
            const int notificationMaxCount = 10;
            var count = 0;
            foreach (var user in UserTestsData.Users)
            {
                var notificationCounts = random.Next(1, notificationMaxCount);
                if(notificationCounts >= notificationMaxCount * 0.4)
                {
                    for (int i = 0; i < notificationCounts; i++)
                    {
                        string[] trueOrFalse = { "false", "true" };
                        bool seen = bool.Parse(trueOrFalse[random.Next(0, trueOrFalse.Length)]);
                        var notification = new Notification()
                        {
                            Id = "NotificationId" + count,
                            Content = "Content" + count,
                            UserId = user.Id,
                            Seen = seen,
                        };
                        count++;
                        notifications.Add(notification);
                    }
                }
            }

            return notifications;
        }
    }
}
