using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class ChartData<TValue>
    {
        public ChartData(string key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public ChartData()
        {
        }

        public string Key { get; set; }

        public TValue Value { get; set; }
    }
}
