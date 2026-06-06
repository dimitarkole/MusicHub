namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenseSmallViewModel : IMapFrom<License>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
