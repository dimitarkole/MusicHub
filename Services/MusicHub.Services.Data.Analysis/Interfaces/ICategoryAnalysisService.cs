using MusicHub.Web.ViewModels.ChartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Analysis.Interfaces
{
    public interface ICategoryAnalysisService
    {
        PieChartViewModel GetCreatedCategoriesCountsByMonths(ChartFilterData filter);

        PieChartViewModel GetCategoriesWithMusicsCount(ChartFilterData filter);

        LineChartViewModel GetTopCategoriesByUploadMusicsCountForMonths(ChartFilterData filter);

        LineChartViewModel GetTopCategoriesByPlayedMusicsCountForMonths(ChartFilterData filter);

        PieChartViewModel GetCategoriesByPlayedMusicsCountAllTime(ChartFilterData filter);

        PieChartViewModel GetCategoriesWithMusicsAllTime();
    }
}
