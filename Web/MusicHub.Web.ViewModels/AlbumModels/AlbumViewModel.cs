namespace MusicHub.Web.ViewModels.AlbumModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class AlbumViewModel : IMapFrom<Album>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int AlbumMusicsCount { get; set; }

        public string ImageUrl { get; set; }

        public AlbumUserViewModel User { get; set; }

        public DateTime CreatedOn { get; set; }

        public Privacy Privacy { get; set; }
    }
}
