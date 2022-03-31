namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class PlaylistEditInputModel : IMapTo<Playlist>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Privacy Privacy { get; set; }
    }
}
