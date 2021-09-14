using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;
using MusicHub.Web.ViewModels.CategoryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.MusicModels
{
    public class MusicViewModel : IMapFrom<Music>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserUserName { get; set; }

        public string ImageUrl { get; set; }

        public string AudioUrl { get; set; }

        public virtual CategoryViewModel Category { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
