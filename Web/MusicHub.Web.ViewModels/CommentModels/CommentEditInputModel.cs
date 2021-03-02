namespace MusicHub.Web.ViewModels.CommentModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CommentEditInputModel : IMapTo<Comment>
    {
        public string Text { get; set; }

        public string ParentCommentId { get; set; }
    }
}
