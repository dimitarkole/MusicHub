using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class ChartViewModel<TValue>
    {
        public ChartViewModel() 
            : this(string.Empty)
        {
        }

        public ChartViewModel(string titlle, List<ChartData<TValue>> data)
        {
            this.Title = titlle;
            this.Data = data;
        }

        public ChartViewModel(string title)
        {
            this.Title = title;
        }

        public List<ChartData<TValue>> Data { get; set; }

        public string Title { get; set; }
    }
}
