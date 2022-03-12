namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongToPlaylistCreateInputModel : IMapTo<PlaylistSong>
    {
        public string PlaylistId { get; set; }

        public string SongId { get; set; }
    }
}
