namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.MusicReactionModels;

    public interface IMusicReactionService
    {
        LikedMusicPageModel<T> All<T>(int page, int entitesPerPage, string userId);

        LikedMusicPageModel<T> Filter<T>(int page, int entitesPerPage, string userId, MusicFilter filter);

        Task Create(MusicReactionCreateInputModel model, string userId);

        Task Delete(string id);

        Task Update(MusicReactionCreateInputModel model, string id);

        MusicReactionViewModel GetOwnReaction(string MusicId, string userId);

        bool IsOwned(string id, string userId);
    }
}
