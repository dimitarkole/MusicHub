namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.ReportMusicModels;
    using static MusicHub.Common.GlobalConstants;

    public static class ReportMusicTestsData
    {
        public static ReportMusicCreateInputModel CreateModel => new ReportMusicCreateInputModel()
        {
            Description = "some desc1",
            MusicId = Musics[0].Id,
            Type = ReportMusicType.HarassmentOrAbuseContent,
        };

        public static ReportMusicCreateInputModel CreateEmptyModel => new ReportMusicCreateInputModel()
        {
            Description = "",
            MusicId = "",
            Type = ReportMusicType.HarassmentOrAbuseContent,
        };

        public static string UserId => Users[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static List<Category> Categories => MusicTestsData.Categories;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<ReportMusic> GetReportMusic()
        {
            var result = new List<ReportMusic>();
            var random = new Random();
            var musics = Musics.ToArray();
            var users = Users.ToList();
            int count = 0;
            var usersCount = random.Next(1, users.Count - 1);
            Array values = Enum.GetValues(typeof(ReportMusicType));
            for (int i = 0; i < usersCount; i++)
            {
                var randomUser = users[random.Next(0, users.Count - 1)];
                users.Remove(randomUser);
                var reportedMusicCount = random.Next(0, musics.Length / 3);
                var allMusics = new List<Music>(musics);
                for (int j = 0; j < reportedMusicCount; j++)
                {
                    var music = allMusics[random.Next(0, allMusics.Count())];
                    var reportMusic = new ReportMusic()
                    {
                        Id = "ReportMusicId" + count,
                        UserId = randomUser.Id,
                        MusicId = music.Id,
                        Description = "Some description from user",
                        Type = (ReportMusicType)values.GetValue(random.Next(values.Length)),
                    };

                    allMusics.Remove(music);
                    count++;
                    result.Add(reportMusic);
                }
            }

            return result;
        }

        public static ReportMusicFilter FilterModelWithCategory => new ReportMusicFilter
        {
            CategoryId = Categories[0].Id,
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static ReportMusicFilter FilterModel => new ReportMusicFilter
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static ReportMusicFilter FilterModelWhitUserId => new ReportMusicFilter
        {
            Name = "Mus",
            UserId = Users[0].Id,
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static ReportMusicFilter FilterModelWhitUserIdAndCategoryId => new ReportMusicFilter
        {
            CategoryId = Categories[0].Id,
            Name = "Mus",
            UserId = Users[0].Id,
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static ReportMusicFilter FilterModelWhitUserName => new ReportMusicFilter
        {
            CategoryId = Categories[0].Id,
            Name = "1",
            UserId = Users[0].Id,
            OrderMethod = OrderMethod.CreatedOnAsc,
            Username = Users[0].UserName,
        };

        public static ReportFilter FilterModelReportWithTypeUserIdAndUsername => new ReportFilter
        {
            Type = ReportMusicType.PromotesTerrorism,
            UserId = Users[0].Id,
            UserName = "User",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static ReportFilter FilterModelReportWithTypeAndUsername => new ReportFilter
        {
            Type = ReportMusicType.PromotesTerrorism,
            UserName = "User",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static ReportFilter FilterModelReportWithTypeUserId => new ReportFilter
        {
            Type = ReportMusicType.PromotesTerrorism,
            UserId = Users[0].Id,
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static ReportFilter FilterModelReportWithType => new ReportFilter
        {
            Type = ReportMusicType.PromotesTerrorism,
            OrderMethod = OrderMethod.CreatedOnAsc,
        };
    }
}
