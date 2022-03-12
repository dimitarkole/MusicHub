namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicencesDetailModel : IMapFrom<License>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public LicenseStatus Status { get; set; }

        public virtual ICollection<LicenceFileViewModel> LicenseFiles { get; set; }
    }
}
