using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MusicHub.Common.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<string> GetEnumValuesAsString<T>()
        {
            foreach (T element in Enum.GetValues(typeof(T)))
            {
                yield return element.ToString();
            }
        }

        public static IEnumerable<string> GetEnumDisplayNames<T>()
        {
            foreach (T element in Enum.GetValues(typeof(T)))
            {
                yield return GetDisplayAttributeName(element);
            }
        }

        public static Dictionary<T, string> GetEnumValuesByDisplayName<T>()
        {
            var valuesByDisplayName = new Dictionary<T, string>();
            foreach (T element in Enum.GetValues(typeof(T)))
            {
                string displayName = GetDisplayAttributeName(element);
                valuesByDisplayName[element] = displayName ?? element.ToString();
            }

            return valuesByDisplayName;
        }

        public static string GetDisplayAttributeName<T>(T element)
        {
            DisplayAttribute displayAttribute = element.GetType()
                       .GetMember(element.ToString())
                       .First()
                       .GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name;
        }
    }
}
