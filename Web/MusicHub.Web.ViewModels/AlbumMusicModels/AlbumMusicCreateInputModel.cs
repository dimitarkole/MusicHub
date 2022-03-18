namespace MusicHub.Web.ViewModels.AlbumMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class AlbumMusicCreateInputModel : IMapTo<AlbumMusic>
    {
        public string AlbumId { get; set; }

        public string MusicId { get; set; }
    }
}
