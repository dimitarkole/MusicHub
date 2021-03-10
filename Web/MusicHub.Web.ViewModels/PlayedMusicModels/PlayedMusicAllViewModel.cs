namespace MusicHub.Web.ViewModels.PlayedMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PlayedMusicAllViewModel<T>
    {

        public IEnumerable<T> PlayedMusic { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
