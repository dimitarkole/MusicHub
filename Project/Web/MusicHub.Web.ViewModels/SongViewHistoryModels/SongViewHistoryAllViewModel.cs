namespace MusicHub.Web.ViewModels.SongViewHistoryModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SongViewHistoryAllViewModel<T>
    {

        public IEnumerable<T> SongViewHistory { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
