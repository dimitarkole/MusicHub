namespace MusicHub.Web.ViewModels.AlbumModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AlbumPageModel<T>
    {
        public IEnumerable<T> Albums { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
