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

        PlaylistMusicsAllViewModel<T> All<T>(int page, int entitesPerPage, string id);

        Task Delete(string id);

        Task Update(PlaylistMusicEditInputModel model, string id);
    }
}
