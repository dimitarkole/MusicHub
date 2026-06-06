namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.UserLoginHistoryModels;
    using static MusicHub.Common.GlobalConstants;

    public class UserLoginHistoryTestsData
    {
        public static UserLoginHistoryCreateInputModel CreateModel => new UserLoginHistoryCreateInputModel()
        {
            IpAddress = "192.168.1.1",
            LoginDate = DateTime.UtcNow,
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",
        };

        public static UserLoginHistoryFilterInputModel FilterModel => new UserLoginHistoryFilterInputModel()
        {
            LoginDateFrom = null,
            LoginDateTo = null,
        };

        public static string CreateUserId => Users[0].Id;
        public static string UserId => Users[0].Id;

        public static string UpdateUserLoginHistoryId => GenUserLoginHistory()[0].Id;

        public static string DeleteUserLoginHistoryId => GenUserLoginHistory()[0].Id;

        public static string GetById => GenUserLoginHistory()[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static List<UserLoginHistory> GenUserLoginHistory(int minPerUser = 1,
             int maxPerUser = 5,
             int? seed = 0)
        {
            var result = new List<UserLoginHistory>();
            var random = seed.HasValue ? new Random(seed.Value) : new Random();

            var userAgents = new[]
            {
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",
                "Mozilla/5.0 (iPhone; CPU iPhone OS 17_0 like Mac OS X)",
                "Mozilla/5.0 (Linux; Android 13)",
                "PostmanRuntime/7.32.0",
            };

            foreach (var user in Users)
            {
                var count = random.Next(minPerUser, maxPerUser + 1);

                for (int i = 0; i < count; i++)
                {
                    var login = new UserLoginHistory
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = user.Id,
                        IpAddress = GenerateRandomIp(random),
                        LoginDate = DateTime.UtcNow.AddDays(-random.Next(0, 30))
                                                      .AddMinutes(-random.Next(0, 1440)),
                        UserAgent = userAgents[random.Next(userAgents.Length)],
                    };

                    result.Add(login);
                }
            }

            return result;
        }

        private static string GenerateRandomIp(Random random)
        {
            return $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(1, 255)}";
        }
    }
}
