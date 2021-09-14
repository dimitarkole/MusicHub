namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class PlaylistCreateInputModel : IMapTo<Playlist>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Privacy Privacy { get; set; }
    }
}
