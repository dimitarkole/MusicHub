namespace MusicHub.Web.ViewModels.LicenseMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenseMusicCreateModel : IMapTo<SongLicense>
    {
        public virtual string LicenseId { get; set; }

        public virtual string SongId { get; set; }
    }
}
