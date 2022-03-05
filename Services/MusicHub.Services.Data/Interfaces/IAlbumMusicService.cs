namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.AlbumMusicModels;

    public interface IAlbumMusicService
    {
        Task Create(AlbumMusicCreateInputModel model);

        AlbumMusicsPageModel<T> All<T>(int page, int entitesPerPage, string id);

        Task Delete(string id);

        Task Update(AlbumMusicEditInputModel model, string id);

        string GetAlbumIdByAlbumMusicId(string id);
    }
}
