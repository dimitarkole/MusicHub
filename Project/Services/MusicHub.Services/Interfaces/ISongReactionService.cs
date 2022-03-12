namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Web.ViewModels.SongModels;
    using MusicHub.Web.ViewModels.SongReactionModels;

    public interface ISongReactionService
    {
        LikedSongsAllViewModel<T> All<T>(int page, int entitesPerPage, string userId);

        LikedSongsAllViewModel<T> Filter<T>(int page, int entitesPerPage, string userId, SongFilter filter);

        Task Create(SongReactionCreateInputModel model, string userId);

        Task Delete(string id);

        Task Update(SongReactionCreateInputModel model, string id);

        SongReactionViewModel GetOwnReaction(string songId, string userId);
    }
}
