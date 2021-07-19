using System;
using System.Globalization;
using static MusicHub.Filtering.Common.Constants;

namespace MusicHub.Filtering.Extensions
{
    public static class StringExtensions
    {
        public static string ChangeFormat(
            this string inputDate, 
            string inputDateFormat = InputDateFormat, 
            string outputDateFormat = DatabaseDateFormat)
        {
            var date = DateTime.ParseExact(inputDate, inputDateFormat, CultureInfo.InvariantCulture);

            return date.ToString(outputDateFormat);
        }
    }
}
