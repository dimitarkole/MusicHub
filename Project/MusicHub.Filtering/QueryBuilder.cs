using MusicHub.Filtering.Attributes;
using MusicHub.Filtering.Common;
using MusicHub.Filtering.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static MusicHub.Filtering.Operators.DatabaseOperators;
using static MusicHub.Filtering.Operators.InputOperators;
using static MusicHub.Filtering.Extensions.StringExtensions;

namespace MusicHub.Filtering
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly OperatorsParser operatorsParser;
        private readonly Type convertersType;
        private IList<string> joins;
        private char alias;

        public QueryBuilder(OperatorsParser operatorsParser, Type convertersType)
        {
            this.operatorsParser = operatorsParser;
            this.convertersType = convertersType;
        }

        public QueryMetadata BuildQuery(string searchString, Type entityType)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                throw new ArgumentException($"{nameof(searchString)} cannot be empty");
            }

            FilteringTableAttribute tableAttribute = entityType.GetCustomAttribute<FilteringTableAttribute>();

            if (entityType == null)
            {
                throw new ArgumentException($"{entityType.Name} is not configured");
            }

            IDictionary<string, ColumnMetadata> columns = MetadataReader.GetColumns(entityType);

            joins = new List<string>();
            alias = 'a';
            var whereClaues = new List<string>();
            var queryParametrs = new List<object>();
            var booleanOperators = new List<string>();
            string sortClause = "";
            int placeholderIndex = 0;

            List<string> queryParts = SplitQuery(searchString);

            foreach (string queryPart in queryParts)
            {
                if (queryPart.Contains(Separator))
                {
                    if (queryPart.StartsWith($"{Sort}{Separator}"))
                    {
                        sortClause = ParseSortQuery(queryPart, columns);
                    }
                    else
                    {
                        (string columnName, string value, string databaseOperator) = ParseFilterQuery(queryPart, columns, tableAttribute.TableName);

                        int rightParenthesesIndex = value.IndexOf(RightParentheses);
                        string rightParentheses = string.Empty;
                        if (rightParenthesesIndex >= 0)
                        {
                            rightParentheses = value.Substring(rightParenthesesIndex, value.Length - rightParenthesesIndex);
                            value = value.Substring(0, rightParenthesesIndex);
                        }

                        string whereClause = $"{columnName} {databaseOperator} {{{placeholderIndex++}}}{rightParentheses}";
                        queryParametrs.Add(value);
                        whereClaues.Add(whereClause);
                    }
                }
                else
                {
                    booleanOperators.Add(queryPart);
                }
            }

            string whereClaue = BuildWhereClause(whereClaues, booleanOperators);

            QueryMetadata query = BuildQueryMetadata(entityType, whereClaue, sortClause, tableAttribute.TableName, queryParametrs);
            return query;
        }

        private List<string> SplitQuery(string searchString)
        {
            var queryParts = new List<string>();
            int previousIndex = 0;
            int spaceIndex = searchString.IndexOf(Space);

            while (spaceIndex != -1)
            {
                string subPart = searchString.Substring(previousIndex, spaceIndex - previousIndex);
                if (!subPart.Contains($"{Separator}{Quote}") || (subPart.IndexOf(Quote) != subPart.LastIndexOf(Quote)))
                {
                    previousIndex = spaceIndex + 1;
                    queryParts.Add(subPart);
                }

                spaceIndex = searchString.IndexOf(Space, spaceIndex + 1);
            }

            queryParts.Add(searchString.Substring(previousIndex, searchString.Length - previousIndex));

            return queryParts;
        }

        private string BuildWhereClause(List<string> filters, List<string> booleanOperators)
        {
            if (filters.Count == 0 || filters.Count - 1 != booleanOperators.Count)
            {
                return string.Empty;
            }

            string whereClause = filters[0];
            for (int i = 1; i < filters.Count; i++)
            {
                whereClause += $" {booleanOperators[i - 1]} {filters[i]}";
            }

            return whereClause;
        }

        private QueryMetadata BuildQueryMetadata(Type entityType, string whereClaue, string sortClause, string tableName, IList<object> parameters)
        {
            string query = $"SELECT [{tableName}].[Id] FROM {tableName}";

            if (joins.Any())
            {
                query += $" {string.Join(" ", joins)}";
            }

            query += " WHERE";
            query = AddClase(query, whereClaue);
            query = AddClase(query, sortClause);

            return new QueryMetadata(query, parameters);
        }

        private char GetAlias()
        {
            int nextAsciCode = (int)alias + 1;
            alias = (char)(nextAsciCode);
            return alias;
        }

        private string GetSelectedColumns(string tableName, Type entityType)
        {
            Type stringType = typeof(string);

            IEnumerable<string> columns = entityType
                .GetProperties()
                .Where(p => !p.PropertyType.IsClass || p.PropertyType == stringType)
                .Select(p => $"[{tableName}].[{p.Name}]")
                .OrderBy(c => c);

            return string.Join(", ", columns);
        }

        private string AddClase(string query, string clause)
        {
            if (!string.IsNullOrEmpty(clause))
            {
                return query + " " + clause;
            }

            return query;
        }

        private (string columnName, string value, string databaseOperator) ParseFilterQuery(string queryPart, IDictionary<string, ColumnMetadata> columns, string tableName)
        {
            string[] subParts = queryPart.Split(new string[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            string key = subParts[0];
            string leftParentheses = null;
            string rightParentheses = null;

            if (key.StartsWith(LeftParentheses))
            {
                leftParentheses = key.Substring(0, key.LastIndexOf(LeftParentheses) + 1);
                key = key.Substring(leftParentheses.Length);
            }

            (string databaseOperator, string value) = GetOperatorAndValue(subParts);

            if (value.EndsWith(RightParentheses))
            {
                int indexOfFirstRightParentheses = value.IndexOf(RightParentheses);
                rightParentheses = value.Substring(indexOfFirstRightParentheses);
                value = value.Substring(0, value.Length - rightParentheses.Length);
            }

            ColumnMetadata column = columns[key.ToLower()];
            string columnName = column.Name;
            if (column.Attribute is ForeignFilteringColumnAttribute foreignFilteringColumn)
            {
                char joinAlias = GetAlias();
                columnName = $"{joinAlias}.{foreignFilteringColumn.ForeignName}";
                PerformJoin(joinAlias, foreignFilteringColumn, column);
                databaseOperator = foreignFilteringColumn.Operator;
            }
            else if (column.Attribute is ForeignFilteringCollectionAttribute filteringCollectionAttribute)
            {
                char joinAlias = GetAlias();
                columnName = $"{joinAlias}.{filteringCollectionAttribute.ForeignName}";
                PerformJoin(joinAlias, filteringCollectionAttribute, column, tableName);
                databaseOperator = filteringCollectionAttribute.Operator;
            }

            if (string.IsNullOrEmpty(databaseOperator))
            {
                databaseOperator = column.Attribute.Operator;
            }
            if (column.Type == typeof(DateTime))
            {
                value = $"{value.ChangeFormat()}";

            }
            if (databaseOperator == Like)
            {
                if (value.StartsWith(Quote))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                value = $"%{value}%";
            }

            if (!string.IsNullOrEmpty(column.Attribute.ValueConvetrer))
            {
                value = ConvertValue(column.Attribute.ValueConvetrer, value);
            }

            if (!string.IsNullOrEmpty(leftParentheses))
            {
                columnName = leftParentheses + columnName;
            }
            if (!string.IsNullOrEmpty(rightParentheses))
            {
                value += rightParentheses;
            }

            return (columnName, value, databaseOperator);
        }

        private string ConvertValue(string valueConvetrer, string value)
        {
            MethodInfo method = convertersType.GetMethod(valueConvetrer, BindingFlags.Static | BindingFlags.Public);
            object result = method.Invoke(null, new object[] { value });
            return result.ToString();
        }

        private void PerformJoin(char joinAlias, ForeignFilteringCollectionAttribute attribute, ColumnMetadata column, string tableName)
        {
            string joinClause = $"INNER JOIN {attribute.JoinName} AS {joinAlias} ON {joinAlias}.{attribute.ForeignKey} = {tableName}.Id";
            joins.Add(joinClause);
        }

        private void PerformJoin(char alias, ForeignFilteringColumnAttribute attribute, ColumnMetadata column)
        {
            // INNER JOIN TableName AS alies ON Column = alias.Id
            string joinClause = $"INNER JOIN {attribute.JoinName} AS {alias} ON {column.Name} = {alias}.Id";
            joins.Add(joinClause);
        }

        private (string databaseOperator, string value) GetOperatorAndValue(string[] subParts)
        {
            string value;
            string booleanOperator = string.Empty;

            if (subParts.Length == 3)
            {
                booleanOperator = operatorsParser.Parse(subParts[1]);
                value = subParts[2];
            }
            else
            {
                value = subParts[1];
            }

            return (booleanOperator, value);
        }

        private string ParseSortQuery(string queryPart, IDictionary<string, ColumnMetadata> columns)
        {
            // sort:name:desc,matter:asc,duration
            queryPart = queryPart.Replace($"{Sort}{Separator}", "");

            IEnumerable<string> sortingColumns = queryPart.Split(Comma)
                .Select(part =>
                {
                    string label = string.Empty;
                    string sortingType = string.Empty;
                    string[] columnBySotringType = part.Split(new string[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

                    label = columnBySotringType[0];
                    ColumnMetadata column = columns[label.ToLower()];
                    string sortingClause = column.Name;

                    if (columnBySotringType.Length > 1)
                    {
                        sortingType = operatorsParser.Parse(columnBySotringType[1]);
                        sortingClause += $" {sortingType}";
                    }

                    return sortingClause;
                });

            return $"ORDER BY {string.Join(", ", sortingColumns)}";
        }
    }
}
