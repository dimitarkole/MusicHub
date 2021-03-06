namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LicenseAllViewModel<T>
    {
        public IEnumerable<T> Licenses { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
