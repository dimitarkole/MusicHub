namespace MusicHub.Services.Interfaces
{
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.CommentModels;

    public interface ICommentReactionService
    {
        Task Create(CommentReactionCreateModel model, string userId);

        Task Delete(string id);

        Task Update(CommentReactionCreateModel model, string id);

        CommentReactionViewModel GetOwnReaction(string songId, string userId);
    }
}
