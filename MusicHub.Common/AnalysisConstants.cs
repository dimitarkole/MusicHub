using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Common
{
    public static class AnalysisConstants
    {
        public static class TimePeriods
        {
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
            public static string GetTopCategoriesByUploadMusicsCountForMonths = "Top " + TopCategoriesCount + " categories for uploading musics";
            public static string GetTopCategoriesByPlayedMusicsCountForMonths = "Top " + TopCategoriesCount + " categories of played musics";
        }
    }
}
