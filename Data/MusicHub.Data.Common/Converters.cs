using MusicHub.Data.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Common
{
    public static class Converters
    {
        public static object HoursToSeconds(string value)
        {
            double hours = double.Parse(value);
            return hours * 3600;
        }

        public static int UserActiveStatus(string value)
        {
            if (Enum.TryParse(value, out ActiveStatus status))
            {
                return (int)status;
            }

            return 0;
        }
    }
}
