using MusicHub.Web.ViewModels.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IPlaylistService
    {
        PlaylistsAllViewModel<T> All<T>(int page, int entitesPerPage);

        PlaylistsAllViewModel<T> Filter<T>(int page, int entitesPerPage, PlaylistFilterInputModel model, string userId = null);

        PlaylistsAllViewModel<T> AllOwned<T>(int page, int entitesPerPage, string userId);

        IList<T> GetOwnedForAddingSong<T>(string musicId, string userId);

        Task Create(PlaylistCreateInputModel model, string userId);

        Task Update(PlaylistEditInputModel model, string id);

        T GetById<T>(string id);

        Task Delete(string id);

        bool IsOwned(string id, string userId);
    }
}
