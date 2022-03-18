namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.PlaylistModels;

    public interface IPlaylistMusicService
    {

        Task Create(PlaylistMusicCreateInputModel model);

        PlaylistMusicsPageModel<T> All<T>(int page, int entitesPerPage, string id);

        Task Delete(string id);

        bool IsAddedToPlaylist(IsAddedToPlaylistCheckInputModel model);

        Task Update(PlaylistMusicEditInputModel model, string id);

        string GetPlaylistIdByPlaylistMusicId(string id);

        T GetPlaylistMusicId<T>(PlaylistMusicCreateInputModel model);
    }
}
