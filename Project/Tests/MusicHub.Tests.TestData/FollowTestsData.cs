namespace MusicHub.Tests.TestData
{
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.FollowModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class FollowTestsData
    {
        public static FollowCreateInputModel CreateModel => new FollowCreateInputModel()
        {
            FollowedId = UserTestsData.Users[1].Id
        };

        public static string FollowerUserIdIsEqualsFollowingUserId => CreateModel.FollowedId;

        public static string FollowedUserId = GetFollows[0].FollowedId;

        public static string FollowingUserId = GetFollows[0].FollowingId;

        public static string DeleteFollowUserId = GetFollows[0].FollowingId;

        public static string DeleteFollowId = GetFollows[0].Id;

        public static string FollowedUserIdIsFollowedTrue = GetFollows[0].FollowedId;

        public static string FollowingUserIdIsFollowedTrue = GetFollows
            .Where(f => f.FollowedId == FollowedUserIdIsFollowedTrue)
            .FirstOrDefault()
            .FollowingId;

        public static string FollowedUserIdIsFollowedFalse = GetFollows[0].FollowedId;

        public static string FollowingUserIdIsFollowedFalse()
        {
            var followingUser = GetFollows
                .Where(f => f.FollowedId == FollowedUserIdIsFollowedFalse)
                .ToList();

            return GetFollows.Where(f => !followingUser.Contains(f))
                .LastOrDefault()
                .FollowingId;
        }


        public static FollowFilterInputModel FollowFilterModel = new FollowFilterInputModel()
        {
            FirstName = "1",
            LastName = "",
            OrderMethod = Common.OrderMethod.CreatedOnAsc,
            Username = "",
        };

        public static List<Follower> GetFollows => new List<Follower>()
        {
            new Follower() {Id = "FollowedId1", FollowedId = Users[0].Id, FollowingId = Users[1].Id},
            new Follower() {Id = "FollowedId2", FollowedId = Users[0].Id, FollowingId = Users[2].Id},
            new Follower() {Id = "FollowedId3", FollowedId = Users[1].Id, FollowingId = Users[2].Id},
            new Follower() {Id = "FollowedId4", FollowedId = Users[1].Id, FollowingId = Users[4].Id},
            new Follower() {Id = "FollowedId5", FollowedId = Users[1].Id, FollowingId = Users[5].Id},
            new Follower() {Id = "FollowedId6", FollowedId = Users[2].Id, FollowingId = Users[1].Id},
            new Follower() {Id = "FollowedId7", FollowedId = Users[3].Id, FollowingId = Users[2].Id},
            new Follower() {Id = "FollowedId8", FollowedId = Users[3].Id, FollowingId = Users[5].Id},
            new Follower() {Id = "FollowedId9", FollowedId = Users[4].Id, FollowingId = Users[1].Id},
            new Follower() {Id = "FollowedId10", FollowedId = Users[5].Id, FollowingId = Users[3].Id},
        };

        public static List<ApplicationUser> Users => UserTestsData.Users;
    }
}
