namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.SongViewHistoryModels;

    public interface ISongViewHistoryService
    {
        SongViewHistoryAllViewModel<T> All<T>(int page, int entitesPerPage, string userId);

        SongViewHistoryAllViewModel<T> Search<T>(int page, int entitesPerPage, SongViewHistoryFilterInputModels filter, string userId);

        Task Create(SongViewHistoryCreateInputModel model, string userId);

        Task Delete(string id);
    }
}
