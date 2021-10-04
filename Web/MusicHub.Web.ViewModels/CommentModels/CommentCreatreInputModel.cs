namespace MusicHub.Web.ViewModels.CommentModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CommentCreatreInputModel : IMapTo<Comment>
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string MusicId { get; set; }
    }
}
