namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System.Collections.Generic;

    using MusicHub.Web.ViewModels.CommonModels;

    public class ReportMusicPageModel<T> : PageModel
    {
        public IEnumerable<T> ReportMusics { get; set; }
    }
}
