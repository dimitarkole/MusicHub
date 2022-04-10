namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class LicensePageModel<T> : PageModel
    {
        public IEnumerable<T> Licenses { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
