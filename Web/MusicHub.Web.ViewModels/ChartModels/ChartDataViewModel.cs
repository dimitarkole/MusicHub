using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.ChartModels
{
    public class ChartDataViewModel
    {
        public ChartDataViewModel(string dimensionOne, double quantity)
        {
            this.DimensionOne = dimensionOne;
            this.Quantity = quantity;
        }

        public string DimensionOne { get; set; }

        public double Quantity { get; set; }
    }
}
