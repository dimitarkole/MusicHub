using MusicHub.Web.ViewModels.ChartModels;
using MusicHub.Web.ViewModels.MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Analysis.Interfaces
{
    public interface IOwnMusicAnalysisService
    {
        PieChartViewModel GetCreatedMusicByCategoriesCountsAllTime(string userId);

        LineChartViewModel GetTopCreatedMusicByCategoriesCountForMonths(string userId, ChartFilterData filter);

        MusicPageModel<T> GetTopPlayedMusicsAllTime<T>(int page, int entitesPerPage, string userId);
    }
}
