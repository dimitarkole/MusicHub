namespace MusicHub.Web.ViewModels.AlbumModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class AlbumCreateInputModel : IMapTo<Album>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public Privacy Privacy { get; set; }
    }
}
