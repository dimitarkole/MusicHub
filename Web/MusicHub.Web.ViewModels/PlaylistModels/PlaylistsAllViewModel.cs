namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PlaylistsAllViewModel<T>
    {
        public IEnumerable<T> Playlists { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
