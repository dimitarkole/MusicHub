namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class PlaylistEditInputModel : IMapTo<Playlist>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Privacy Privacy { get; set; }
    }
}
