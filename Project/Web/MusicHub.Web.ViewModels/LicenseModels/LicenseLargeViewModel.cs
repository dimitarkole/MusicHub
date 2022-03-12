namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenseLargeViewModel : IMapFrom<License>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public LicenseStatus Status { get; set; }

        public LicenseUserViewModel User { get; set; }

        // public List<LicenceFileViewModel> LicenseFiles { get; set; }

        /*public int MusicLicensesCount { get; set; }

        public int LicenseFilesCount { get; set; } */
        public DateTime CreatedOn { get; set; }
    }
}
