namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlaylistMusicCreateInputModel : IMapTo<PlaylistMusic>
    {
        public string PlaylistId { get; set; }

        public string MusicId { get; set; }
    }
}
