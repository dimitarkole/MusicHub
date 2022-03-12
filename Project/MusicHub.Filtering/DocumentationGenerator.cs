using MusicHub.Filtering.Common;
using MusicHub.Filtering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Filtering
{
    public class DocumentationGenerator
    {
        public IList<ColumnDocumentation> GenerateEntityDocumentation(Type entityType)
        {
            var docummentedColumns = new List<ColumnDocumentation>();
            string tableName = MetadataReader.GetTableName(entityType);
            ICollection<ColumnMetadata> columns = MetadataReader.GetColumns(entityType).Values;

            foreach (ColumnMetadata column in columns)
            {
                if (column.Type == typeof(DateTime))
                {
                    string description = $"{column.Label}:MM/dd/yyyy - find {tableName} which {column.Label} is equal to the date provided by you\n";
                    description += $"{column.Label}:from:MM/dd/yyyy - find {tableName} which {column.Label} is from the date provided by you\n";
                    description += $"{column.Label}:from:MM/dd/yyyy - find {tableName} which {column.Label} is to the date provided by you";
                    docummentedColumns.Add(new ColumnDocumentation(column.Label, description));
                }
                else if (column.Type == typeof(string))
                {
                    string description = $"{column.Label}:value - find {tableName} which {column.Label} contains the value provided by you";
                    docummentedColumns.Add(new ColumnDocumentation(column.Label, description));
                }
                else
                {
                    string description = $"{column.Label}:value - find {tableName} which {column.Label} is equal to the value provide by you\n";
                    description += $"{column.Label}:lt:value - find {tableName} which {column.Label} is less than the value provide by you\n";
                    description += $"{column.Label}:gt:value - find {tableName} which {column.Label} is greater than the value provide by you";
                    docummentedColumns.Add(new ColumnDocumentation(column.Label, description));
                }
            }

            return docummentedColumns;
        }
    }
}
