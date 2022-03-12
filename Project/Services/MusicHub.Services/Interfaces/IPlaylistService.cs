namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.PlaylistModels;

    public interface IPlaylistService
    {
        PlaylistsAllViewModel<T> All<T>(int page, int entitesPerPage);

        PlaylistsAllViewModel<T> FilterOwn<T>(int page, int entitesPerPage, PlaylistFilterInputModel model, string userId);

        PlaylistsAllViewModel<T> Filter<T>(int page, int entitesPerPage, PlaylistFilterInputModel model);

        PlaylistsAllViewModel<T> AllOwn<T>(int page, int entitesPerPage, string userId);

        IList<T> GetOwnForAddingSong<T>(string songId, string userId);

        Task Create(PlaylistCreateInputModel model, string userId);

        Task Update(PlaylistEditInputModel model, string id);

        T GetById<T>(string id);

        Task Delete(string id);

        bool IsOwn(string id, string userId);
    }
}
