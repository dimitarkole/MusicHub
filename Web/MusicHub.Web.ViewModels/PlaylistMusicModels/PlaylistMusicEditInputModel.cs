namespace MusicHub.Web.ViewModels.PlaylistMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlaylistMusicEditInputModel : IMapTo<PlaylistMusic>
    {
        public int Position { get; set; }
    }
}
