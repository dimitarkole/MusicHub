using MusicHub.Web.ViewModels.MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IMusicService
    {
        MusicsAllViewModel<T> All<T>(int page, int entitesPerPage);

        IEnumerable<T> SuggestMusics<T>(string musicId);

        MusicsAllViewModel<T> AllOwn<T>(int page, int entitesPerPage, string userId);

        MusicsAllViewModel<T> Filter<T>(int page, int entitesPerPage, MusicFilter filter, string userId = null);

        Task<string> Create(MusicCreateInputModel model, string userId);

        T GetById<T>(string id);

        Task Update(string id, MusicEditModel model);

        Task Delete(string id);

        bool IsOwned(string musicId, string userId);
    }
}
