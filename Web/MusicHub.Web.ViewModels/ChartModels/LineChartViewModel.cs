using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class LineChartViewModel
    {
        public string Name { get; set; }

        public string XAxisLabel { get; set; }

        public string YAxisLabel { get; set; }

        public ICollection<LineChartDataViewModel> Data { get; set; }
    }
}
