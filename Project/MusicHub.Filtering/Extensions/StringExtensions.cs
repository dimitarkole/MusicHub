using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MusicHub.Filtering.Common;

namespace MusicHub.Filtering.Extensions
{
    public static class StringExtensions
    {
        public static string ChangeFormat(
            this string inputDate,
            string inputDateFormat = Constants.InputDateFormat,
            string outputDateFormat = Constants.DatabaseDateFormat)
        {
            var date = DateTime.ParseExact(inputDate, inputDateFormat, CultureInfo.InvariantCulture);

            return date.ToString(outputDateFormat);
        }
    }
}
