using System;
using System.Collections.Generic;
using System.Text;
using static MusicHub.Filtering.Operators.InputOperators;
using static MusicHub.Filtering.Operators.DatabaseOperators;

namespace MusicHub.Filtering.Operators
{
    public class OperatorsParser
    {
        private readonly IDictionary<string, OperatorMetadata> store;

        public OperatorsParser()
        {
            store = new Dictionary<string, OperatorMetadata>();
        }

        public static OperatorsParser Initialize()
        {
            var parser = new OperatorsParser();
            parser.AddOeprator(Sort, OrderBy);
            parser.AddOeprator(InputOperators.Ascending, DatabaseOperators.Ascending);
            parser.AddOeprator(InputOperators.Descending, DatabaseOperators.Descending);
            parser.AddOeprator(InputOperators.GreaterThan, DatabaseOperators.GreaterThan);
            parser.AddOeprator(InputOperators.LessThan, DatabaseOperators.LessThan);
            parser.AddOeprator(From, GreaterThanOrEqual);
            parser.AddOeprator(To, LessThanOrEqual);

            return parser;
        }

        public void AddOeprator(string label, string @operator) =>
            store[label] = new OperatorMetadata
            {
                Label = label.ToLower(),
                Operator = @operator
            };

        public string Parse(string label) => store[label.ToLower()]?.Operator;
    }
}
