using MusicHub.Filtering.Attributes;
using System;
using System.Reflection;

namespace MusicHub.Filtering.Common
{
    public class ColumnMetadata
    {
        public PropertyInfo Property { get; set; }

        public FilteringColumnAttribute Attribute { get; set; }

        public string Label => Attribute.Label;

        public string Operator => Attribute.Operator;

        public string Name => Property.Name;

        public Type Type => Property.PropertyType;
    }
}
