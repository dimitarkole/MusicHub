namespace MusicHub.Web.ViewModels.SongModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using AutoMapper;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongViewModel : IMapFrom<Song>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserUserName { get; set; }

        public string ImageFilePath { get; set; }

        public string AudioFilePath { get; set; }

        public virtual string CategoryId { get; set; }

        public virtual string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }

        public virtual ICollection<SongLicense> SongLicenses { get; set; }
    }
}
