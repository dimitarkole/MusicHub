namespace MusicHub.Web.ViewModels.PlayLaterMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class PlayLaterMusicPageModel<T> : PageModel
    {
        public IEnumerable<T> PlayLaterMusics { get; set; }
    }
}
