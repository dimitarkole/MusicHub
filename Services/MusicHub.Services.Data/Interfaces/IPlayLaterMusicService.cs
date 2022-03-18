namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.PlayLaterMusicModels;

    public interface IPlayLaterMusicService
    {
        PlayLaterMusicPageModel<T> All<T>(int page, int entitesPerPage, string userId);

        PlayLaterMusicPageModel<T> Filter<T>(int page, int entitesPerPage, PlayLaterMusicFilterInputModels filter, string userId);

        Task<string> Create(PlayLaterMusicCreateInputModel model, string userId);

        Task Update(string id, PlayLaterMusicEditInputModel model);

        Task Delete(string id);

        bool IsOwned(string id, string userId);
    }
}
