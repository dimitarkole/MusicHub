using MusicHub.Filtering.Attributes;
using MusicHub.Filtering.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MusicHub.Filtering
{
    public static class MetadataReader
    {
        public static IDictionary<string, ColumnMetadata> GetColumns(Type model) =>
           model.GetProperties()
               .Where(p => p.GetCustomAttribute<FilteringColumnAttribute>() != null)
               .ToDictionary(p => p.GetCustomAttribute<FilteringColumnAttribute>().Label.ToLower(), property => new ColumnMetadata
               {
                   Attribute = property.GetCustomAttribute<FilteringColumnAttribute>(),
                   Property = property
               });

        public static string GetTableName(Type type) => type.GetCustomAttribute<FilteringTableAttribute>().TableName;

    }
}
