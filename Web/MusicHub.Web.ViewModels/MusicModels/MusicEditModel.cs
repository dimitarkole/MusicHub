namespace MusicHub.Web.ViewModels.MusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class MusicEditModel : IMapTo<Music>// , IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }

        public string Description { get; set; }

        public string AudioUrl { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }
    }
}
