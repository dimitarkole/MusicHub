namespace MusicHub.Web.ViewModels.CommentModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CommentEditInputModel : IMapTo<Comment>
    {
        [Required]
        public string Text { get; set; }
    }
}
