namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlaylistEditInputModel : IMapTo<Playlist>
    {
        public string Name { get; set; }
    }
}
