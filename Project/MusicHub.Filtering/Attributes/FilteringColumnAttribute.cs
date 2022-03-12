using System;

namespace MusicHub.Filtering.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class FilteringColumnAttribute : Attribute
    {
        public FilteringColumnAttribute(
            string label,
            string @operator = Operators.DatabaseOperators.Equal,
            string valueConverter = null)
        {
            Label = label;
            Operator = @operator;
            ValueConvetrer = valueConverter;
        }

        public string Label { get; private set; }

        public string Operator { get; private set; }

        public string ValueConvetrer { get; private set; }
    }
}
