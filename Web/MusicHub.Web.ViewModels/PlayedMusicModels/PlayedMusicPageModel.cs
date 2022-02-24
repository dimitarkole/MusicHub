namespace MusicHub.Web.ViewModels.PlayedMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PlayedMusicPageModel<T>
    {

        public IEnumerable<T> PlayedMusics { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
