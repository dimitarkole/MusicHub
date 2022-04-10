namespace MusicHub.Web.ViewModels.PlaylistMusicModels
{
    using MusicHub.Web.ViewModels.CommonModels;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PlaylistMusicsPageModel<T> : PageModel
    {
        public IEnumerable<T> PlaylistMusics { get; set; }
    }
}
