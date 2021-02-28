using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;
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

        public string ImageFilePath { get; set; }

        public string AudioFilePath { get; set; }

        public virtual string CategoryId { get; set; }

        public virtual string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }
    }
}
