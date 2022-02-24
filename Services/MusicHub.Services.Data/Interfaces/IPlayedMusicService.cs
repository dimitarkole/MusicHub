namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.PlayedMusicModels;

    public interface IPlayedMusicService
    {
        PlayedMusicPageModel<T> All<T>(int page, int entitesPerPage, string userId);

        PlayedMusicPageModel<T> Filter<T>(int page, int entitesPerPage, PlayedMusicFilterInputModels filter, string userId);

        Task<string> Create(PlayedMusicCreateInputModel model, string userId);

        Task Delete(string id);

        bool IsOwned(string id, string userId);
    }
}
