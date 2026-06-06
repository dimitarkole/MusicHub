namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReportPageModel<T>
    {
        public IEnumerable<T> ReportMusics { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
