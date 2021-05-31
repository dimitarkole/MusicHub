namespace MusicHub.Web.ViewModels.MusicLicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.LicenseModels;
    using MusicHub.Web.ViewModels.MusicModels;

    public class MusicLicenseViewModel : IMapFrom<MusicLicense>
    {
        public virtual string Id { get; set; }

        public virtual string MusicId { get; set; }

        public virtual MusicViewModel Music { get; set; }

        public virtual string LicenseId { get; set; }

        public virtual LicenceViewModel License { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
