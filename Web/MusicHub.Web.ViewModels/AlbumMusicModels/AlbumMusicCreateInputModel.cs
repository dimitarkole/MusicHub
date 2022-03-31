namespace MusicHub.Web.ViewModels.AlbumMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class AlbumMusicCreateInputModel : IMapTo<AlbumMusic>
    {
        [Required]
        public string AlbumId { get; set; }

        [Required]
        public string MusicId { get; set; }
    }
}
