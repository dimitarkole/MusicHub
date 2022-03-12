namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.SongModels;

    public interface ISongService
    {
        SongsAllViewModel<T> All<T>(int page, int entitesPerPage);

        IEnumerable<T> SuggestSongs<T>(string songId);

        SongsAllViewModel<T> AllOwn<T>(int page, int entitesPerPage, string userId);

        SongsAllViewModel<T> Filter<T>(int page, int entitesPerPage, SongFilter filter, string userId = null);

        Task<string> Create(SongCreateInputModel model, string userId);

        T GetById<T>(string id);

        Task Update(string id, SongEditModel model);

        Task Delete(string id);

        bool IsOwn(string songId, string userId);
    }
}
