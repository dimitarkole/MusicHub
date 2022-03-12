namespace MusicHub.Web.ViewModels.CommentModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CommentCreatreInputModel : IMapTo<Comment>
    {
        public string Text { get; set; }

        public string SongId { get; set; }
    }
}
