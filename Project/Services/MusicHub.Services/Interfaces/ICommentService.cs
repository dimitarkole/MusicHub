namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.CommentModels;

    public interface ICommentService
    {
        CommentsAllViewModel<T> All<T>(int page, int entitesPerPage, string songId);

        Task Create(CommentCreatreInputModel model, string userId);

        Task CreateChildrenComment(CommentChildrenCreatreInputModel model, string parentCommentId, string userId);

        Task Update(string id, CommentEditInputModel model);

        Task Delete(string id);

        bool IsOwn(string id, string userId);
    }
}
