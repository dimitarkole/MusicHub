namespace MusicHub.Web.ViewModels.AlbumMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class AlbumMusicEditInputModel : IMapTo<AlbumMusic>
    {
        public int Position { get; set; }
    }
}
