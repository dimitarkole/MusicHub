namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class PlaylistCreateInputModel : IMapTo<Playlist>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Privacy Privacy { get; set; }
    }
}
