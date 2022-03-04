namespace MusicHub.Web.ViewModels.AlbumModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class AlbumEditInputModel : IMapTo<Album>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Privacy Privacy { get; set; }
    }
}
