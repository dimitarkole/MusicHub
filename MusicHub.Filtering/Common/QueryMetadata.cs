using System.Collections;
using System.Collections.Generic;

namespace MusicHub.Filtering.Common
{
    public class QueryMetadata
    {
        public QueryMetadata(string content, IList<object> parameters)
        {
            Content = content;
            Parameters = parameters;
        }

        public string Content { get; private set; }

        public IList<object> Parameters { get; private set; }
    }
}
