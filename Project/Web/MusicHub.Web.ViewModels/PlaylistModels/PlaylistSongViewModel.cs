namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SongModels;

    public class PlaylistSongViewModel : IMapFrom<PlaylistSong>
    {
        public virtual string Id { get; set; }

        public SongViewModel Song { get; set; }
    }
}
