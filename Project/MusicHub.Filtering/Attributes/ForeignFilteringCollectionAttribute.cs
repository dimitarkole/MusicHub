using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Filtering.Attributes
{
    public class ForeignFilteringCollectionAttribute : FilteringColumnAttribute
    {
        public ForeignFilteringCollectionAttribute(
           string label,
           string @operator,
           string columnName,
           string tableName,
           string foreignKey) : base(label, @operator)
        {
            ForeignName = columnName;
            JoinName = tableName;
            ForeignKey = foreignKey;
        }

        public string ForeignName { get; private set; }

        public string JoinName { get; private set; }

        public string ForeignKey { get; private set; }
    }
}
