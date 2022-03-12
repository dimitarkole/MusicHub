namespace MusicHub.Filtering.Attributes
{
    public class ForeignFilteringColumnAttribute : FilteringColumnAttribute
    {
        public ForeignFilteringColumnAttribute(
            string label,
            string @operator,
            string columnName,
            string tableName) : base(label, @operator)
        {
            ForeignName = columnName;
            JoinName = tableName;
        }

        public string ForeignName { get; private set; }

        public string JoinName { get; private set; }
    }
}
