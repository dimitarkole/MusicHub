namespace MusicHub.Web.ViewModels.AlbumMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AlbumMusicsPageModel<T>
    {
        public IEnumerable<T> AlbumMusics { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
