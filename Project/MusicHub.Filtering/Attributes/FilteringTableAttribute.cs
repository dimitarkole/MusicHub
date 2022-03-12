using System;

namespace MusicHub.Filtering.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FilteringTableAttribute : Attribute
    {
        public FilteringTableAttribute(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; private set; }
    }
}
