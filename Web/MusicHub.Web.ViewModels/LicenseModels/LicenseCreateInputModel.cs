namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class LicenseCreateInputModel : IMapTo<License>
    {
        [Required]
        public string Name { get; set; }

        public LicenseStatus Status { get; set; }
    }
}
