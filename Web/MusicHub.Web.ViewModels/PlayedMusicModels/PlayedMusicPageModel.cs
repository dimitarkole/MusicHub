namespace MusicHub.Web.ViewModels.PlayedMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class PlayedMusicPageModel<T> : PageModel
    {

        public IEnumerable<T> PlayedMusics { get; set; }
    }
}
