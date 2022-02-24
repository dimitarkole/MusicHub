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
        PlaylistPageModel<T> All<T>(int page, int entitesPerPage);

        PlaylistPageModel<T> Filter<T>(int page, int entitesPerPage, PlaylistFilterInputModel model, string userId = null);

        PlaylistPageModel<T> AllOwned<T>(int page, int entitesPerPage, string userId);

        IList<T> GetOwnedForAddingMusic<T>(string musicId, string userId);

        Task<string> Create(PlaylistCreateInputModel model, string userId);

        Task Update(PlaylistEditInputModel model, string id);

        T GetById<T>(string id);

        Task Delete(string id);

        bool IsOwned(string id, string userId);
    }
}
