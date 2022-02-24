namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.CommentModels;

    public interface ICommentService
    {
        CommentPageModel<T> All<T>(int page, int entitesPerPage, string musicId);

        CommentPageModel<T> GetChildrenComments<T>(int page, int entitesPerPage, string parentCommentId);

        Task Create(CommentCreatreInputModel model, string userId);

        Task CreateChildrenComment(CommentChildrenCreatreInputModel model, string parentCommentId, string userId);

        Task Update(string id, CommentEditInputModel model);

        Task Delete(string id);

        bool IsOwned(string id, string userId);
    }
}
