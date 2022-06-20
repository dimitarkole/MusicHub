namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenseFileCreateInputModel : IMapTo<LicenseFile>
    {
        [Required]
        public string FileUrl { get; set; }

        [Required]
        public string LicensеId { get; set; }
    }
}
