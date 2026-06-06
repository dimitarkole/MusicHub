namespace MusicHub.Web.ViewModels.AlbumMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class AlbumMusicsPageModel<T> : PageModel
    {
        public IEnumerable<T> AlbumMusics { get; set; }
    }
}
