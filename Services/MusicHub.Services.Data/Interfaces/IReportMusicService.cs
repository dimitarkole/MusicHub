namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.ReportMusicModels;

    public interface IReportMusicService
    {
        ReportPageModel<T> All<T>(string musicId, int page, int entitesPerPage);

        ReportPageModel<T> Filter<T>(string musicId, ReportFilter filterModel, int page, int entitesPerPage);

        Task<string> Create(ReportMusicCreateInputModel model, string userId);

        T GetById<T>(string id);

        Task Delete(string id);
    }
}
