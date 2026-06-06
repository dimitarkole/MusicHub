namespace MusicHub.Web.ViewModels.LicenseMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenseMusicCreateModel : IMapTo<MusicLicense>
    {
        [Required]
        public virtual string LicenseId { get; set; }

        [Required]
        public virtual string MusicId { get; set; }
    }
}
