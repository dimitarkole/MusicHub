namespace MusicHub.Web.ViewModels.SongReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LikedSongsAllViewModel<T>
    {
        public IEnumerable<T> LinkedSongs { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
