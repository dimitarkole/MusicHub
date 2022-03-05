namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.AlbumModels;

    public interface IAlbumService
    {
        AlbumPageModel<T> All<T>(int page, int entitesPerPage, string userId = null);

        AlbumPageModel<T> Filter<T>(int page, int entitesPerPage, AlbumFilterInputModel model, string userId = null);

        IList<T> GetOwnedForAddingMusic<T>(string musicId, string userId);

        Task<string> Create(AlbumCreateInputModel model, string userId);

        Task Update(AlbumEditInputModel model, string id);

        T GetById<T>(string id);

        Task Delete(string id);

        bool IsOwned(string id, string userId);
    }
}
