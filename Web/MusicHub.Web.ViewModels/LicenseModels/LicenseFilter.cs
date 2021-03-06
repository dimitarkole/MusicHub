namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    public class LicenseFilter
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public LicenseStatus Status { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
