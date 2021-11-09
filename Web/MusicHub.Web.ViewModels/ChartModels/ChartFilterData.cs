using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class ChartFilterData
    {
        public DateTime CreatedOnFrom { get; set; }

        public DateTime CreatedOnTo { get; set; }

        public int CountOfSeries { get; set; }

        public int CountOfMonthsBefore { get; set; }
    }
}
