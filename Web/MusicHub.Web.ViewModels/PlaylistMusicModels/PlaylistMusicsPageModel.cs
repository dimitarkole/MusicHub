namespace MusicHub.Web.ViewModels.PlaylistMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PlaylistMusicsPageModel<T>
    {
        public IEnumerable<T> PlaylistMusics { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
