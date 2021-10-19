using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class LineChartDataViewModel
    {
        public string Name { get; set; }

        public ICollection<LineChartSeriesViewModel> Series { get; set; }
    }
}
