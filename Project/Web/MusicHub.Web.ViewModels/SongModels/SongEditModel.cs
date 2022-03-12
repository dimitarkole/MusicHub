namespace MusicHub.Web.ViewModels.SongModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongEditModel : IMapTo<Song>//, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ImageFilePath { get; set; }

        public string CategoryId { get; set; }

        public string Text { get; set; }

        public string AudioFilePath { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }
    }
}
