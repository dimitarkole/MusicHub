namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.NotificationSetingModels;

    public static class NotificationSettingTestsData
    {
        public static NotificationSettingCreateInputModel CreateModel => new NotificationSettingCreateInputModel()
        {
            Type = NotificationSettingType.SendEmail,
            Enabled = false,
        };

        public static List<User> Users => UserTestsData.Users;

        public static string CreateUserId => Users[0].Id;

        public static List<NotificationSetting> NotificationSettings()
        {
            var result = new List<NotificationSetting>();
            var random = new Random();
            var users = Users;
            var count = 1;
            var notificationTypes = Enum.GetValues(typeof(NotificationSettingType));
            string[] trueOrFalse = { "false", "true" };
            foreach (var user in users)
            {
                var countNotificationSetting = random.Next(0, 3);
                for (int i = 0; i < countNotificationSetting; i++)
                {
                    NotificationSettingType notificationTypeRnd;
                    do
                    {
                        notificationTypeRnd = (NotificationSettingType)notificationTypes.GetValue(random.Next(notificationTypes.Length));
                    } while (result.Any(r => r.Type == notificationTypeRnd && r.UserId == user.Id));

                    var notificationSetting = new NotificationSetting()
                    {
                        Id = "NotificationSetting" + count,
                        Type = notificationTypeRnd,
                        UserId = user.Id,
                        Enabled = bool.Parse(trueOrFalse[random.Next(0, trueOrFalse.Length)]),
                    };

                    result.Add(notificationSetting);
                    count++;
                }
            }

            return result;
        }
    }
}
