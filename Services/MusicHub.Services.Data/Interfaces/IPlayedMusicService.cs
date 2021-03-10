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
        PlayedMusicAllViewModel<T> All<T>(int page, int entitesPerPage, string userId);

        PlayedMusicAllViewModel<T> Search<T>(int page, int entitesPerPage, PlayedMusicFilterInputModels filter, string userId);

        Task Create(PlayedMusicCreateInputModel model, string userId);

        Task Delete(string id);
    }
}
