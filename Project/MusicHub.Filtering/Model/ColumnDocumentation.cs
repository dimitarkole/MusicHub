using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Filtering.Model
{
    public class ColumnDocumentation
    {
        public ColumnDocumentation()
        {
        }

        public ColumnDocumentation(string label, string description)
        {
            Label = label;
            Description = description;
        }

        public string Label { get; set; }

        public string Description { get; set; }
    }
}
