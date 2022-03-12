namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenseFileCreateInputModel : IMapTo<LicenseFile>
    {
        public string Path { get; set; }

        public string LicensеId { get; set; }
    }
}
