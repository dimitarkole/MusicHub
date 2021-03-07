namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlaylistFilterInputModel : IMapTo<Playlist>
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
