using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class BarChartViewModel
    {
        public string Name { get; set; }

        public ICollection<BarChartSeriesViewModel> Series { get; set; }
    }
}
