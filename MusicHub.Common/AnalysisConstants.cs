using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Common
{
    public static class AnalysisConstants
    {
        public static class TimePeriods
        {
            public static int Week = 7;
            public static int TreeMonths = 3;
            public static int SixMonths = 6;
            public static int NineMonths = 9;
            public static int Year = 12;
            public static int TwoYear = 24;
        }

        public static class CategoryAnalysis
        {
            public static int TopCategoriesCount = 3;
            public static string MonthsXLabel = "Months";
            public static string MusicYLabel = "Count of musics";
            public static string GetCategoriesByPlayedMusicsCountAllTime = "Categories with played musics for all time";
            public static string GetCategoriesWithMusicsAllTime = "Categories with musics for all time";
            public static string GetCategoriesWithMusicsCount = "Categories with musics count";
            public static string GetCreatedCategoriesCountsByMonths = "Created categories by months";
            public static string GetTopCategoriesByUploadMusicsCountForMonths = "Top categories of uploading musics";
            public static string GetTopCategoriesByPlayedMusicsCountForMonths = "Top categories of playing musics";
        }

        public static class OwnMusicAnalysis
        {
            public static string GetCreatedMusicByCategoriesCountsAllTime = "Musics by categories for all time";
            public static int PlayedMusicsCount = 10;
        }

        public static class RoleAnalysis
        {
            public static string GetRolesByUsersForMonths = "Roles by count of users";
            public static string GetRolesByAverageCountOfUsersForWeek = "Roles by average count of users in month";
            public static string GetRolesByCountOfUsers = "Roles by count of users";
            public static string GetRolesByUsersLastWeek = "Roles by count of users last week";
            public static string GetRolesByCountOfPermissions = "Roles by count of permissions";
        }
    }
}
