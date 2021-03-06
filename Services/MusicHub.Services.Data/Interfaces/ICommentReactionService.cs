namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.CommentModels;

    public interface ICommentReactionService
    {
        Task Create(CommentReactionCreateModel model, string userId);

        Task Delete(string id);

        bool IsOwned(string id, string userId);

        Task Update(CommentReactionEditInputModel model, string id);

        CommentReactionViewModel GetOwnReaction(string commentId, string userId);
    }
}
