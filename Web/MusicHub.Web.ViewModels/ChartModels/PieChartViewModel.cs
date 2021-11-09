using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class PieChartViewModel
    {
        public string Name { get; set; }

        public ICollection<PieChartSeriesViewModel> Series { get; set; }
    }
}
