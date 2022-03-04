using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.AlbumModels
{
    public class AlbumPlayModel : IMapFrom<Album>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public Privacy Privacy { get; set; }

        public AlbumUserViewModel User { get; set; }
    }
}
