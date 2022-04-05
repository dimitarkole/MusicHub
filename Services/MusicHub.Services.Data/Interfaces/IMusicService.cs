namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.ReportMusicModels;

    public interface IMusicService
    {
        ReportMusicPageModel<T> AllReportedMusic<T>(int page, int entitesPerPage);

        ReportMusicPageModel<T> FilterReported<T>(ReportMusicFilter filter, int page, int entitesPerPage);

        MusicPageModel<T> All<T>(int page, int entitesPerPage);

        MusicPageModel<T> SuggestMusics<T>(int page, int entitesPerPage, string musicId);

        MusicPageModel<T> AllOwn<T>(int page, int entitesPerPage, string userId);

        MusicPageModel<T> Filter<T>(int page, int entitesPerPage, MusicFilter filter, string userId = null);

        Task<string> Create(MusicCreateInputModel model, string userId);

        T GetById<T>(string id);

        Task Update(string id, MusicEditModel model);

        Task Delete(string id);

        bool IsOwned(string musicId, string userId);
    }
}
