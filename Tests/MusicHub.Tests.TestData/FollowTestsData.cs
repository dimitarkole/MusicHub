using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.FollowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public static class FollowTestsData
    {
        public static FollowFilterInputModel FilterModelUserName => new FollowFilterInputModel()
        {
            Username = "1",
            OrderMethod = OrderMethod.NameAsc,
        };

        public static FollowFilterInputModel FilterModelFirstName => new FollowFilterInputModel()
        {
            FirstName = "1",
            OrderMethod = OrderMethod.NameAsc,
        };

        public static FollowFilterInputModel FilterModelLastName => new FollowFilterInputModel()
        {
            LastName = "1",
            OrderMethod = OrderMethod.NameAsc,
        };

        public static List<User> Users => UserTestsData.Users;

        public static string CreateUserId => Users[0].Id;

        public static FollowCreateInputModel CreateModel => new FollowCreateInputModel()
        {
            FollowerId = Users[0].Id,
        };

        public static List<Follow> Follows()
        {
            var result = new List<Follow>();
            var random = new Random();
            var count = 1;
            foreach (var user in Users)
            {
                var unfollowingUsers = Users.Where(u => u.Id != user.Id).ToList();
                var followingUsersCount = random.Next(0, unfollowingUsers.Count());
                for (int i = 0; i < followingUsersCount; i++)
                {
                    var followingUser = unfollowingUsers[random.Next(0, unfollowingUsers.Count())];
                    var follow = new Follow()
                    {
                        Id = "FollowId" + count,
                        FollowerId = user.Id,
                        FollowingId = followingUser.Id,
                    };
                    count++;
                    unfollowingUsers.Remove(followingUser);
                    result.Add(follow);
                }
            }

            return result;
        }
    }
}
