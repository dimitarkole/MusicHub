namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlaylistCreateInputModel : IMapTo<Playlist>
    {
        public string Name { get; set; }
    }
}
