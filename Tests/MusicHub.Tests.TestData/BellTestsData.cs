using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.BellModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public class BellTestsData
    {
        public static BellFilterInputModel FilterModelUserName => new BellFilterInputModel()
        {
            Username = "1",
            OrderMethod = OrderMethod.NameAsc,
        };

        public static BellFilterInputModel FilterModelFirstName => new BellFilterInputModel()
        {
            FirstName = "1",
            OrderMethod = OrderMethod.NameAsc,
        };

        public static BellFilterInputModel FilterModelLastName => new BellFilterInputModel()
        {
            LastName = "1",
            OrderMethod = OrderMethod.NameAsc,
        };

        public static List<User> Users => UserTestsData.Users;

        public static string CreateUserId => Users[0].Id;

        public static BellCreateInputModel CreateModel => new BellCreateInputModel()
        {
            BellingId = Users[0].Id,
        };

        public static List<Bell> Bells()
        {
            var result = new List<Bell>();
            var random = new Random(1);
            var count = 1;
            foreach (var user in Users)
            {
                var unbellingUsers = Users.Where(u => u.Id != user.Id).ToList();
                var bellingUsersCount = random.Next(0, unbellingUsers.Count());
                for (int i = 0; i < bellingUsersCount; i++)
                {
                    var bellingUser = unbellingUsers[random.Next(0, unbellingUsers.Count())];
                    var follow = new Bell()
                    {
                        Id = "BellId" + count,
                        BellerId = user.Id,
                        BellingId = bellingUser.Id,
                    };

                    count++;
                    unbellingUsers.Remove(bellingUser);
                    result.Add(follow);
                }
            }

            return result;
        }
    }
}
