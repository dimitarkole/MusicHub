namespace MusicHub.Web.ViewModels.PlaylistMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlaylistMusicCreateInputModel : IMapTo<PlaylistMusic>
    {
        [Required]
        public string PlaylistId { get; set; }

        [Required]
        public string MusicId { get; set; }
    }
}
