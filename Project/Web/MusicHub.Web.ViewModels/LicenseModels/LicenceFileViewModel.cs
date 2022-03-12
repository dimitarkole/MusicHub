namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenceFileViewModel : IMapFrom<LicenseFile>
    {
        public string Id { get; set; }

        public string Path { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
