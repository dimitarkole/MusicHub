namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenceViewModel : IMapFrom<License>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public LicenseStatus Status { get; set; }

        public int SongLicensesCount { get; set; }

        public int LicenseFilesCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
