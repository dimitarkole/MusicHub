namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.PlaylistModels;

    public interface IPlaylistSongService
    {
        Task Create(SongToPlaylistCreateInputModel model);

        PlaylistSongsAllViewModel<T> All<T>(int page, int entitesPerPage, string id);

        Task Delete(string id);
    }
}
