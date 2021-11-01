using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class BarChartViewModel
    {
        public string Name { get; set; }

        public string XAxisLabel { get; set; }

        public string YAxisLabel { get; set; }

        public IEnumerable<BarChartSeriesViewModel> Series { get; set; }
    }
}
