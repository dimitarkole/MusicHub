using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.MusicModels
{
    public class MusicPageModel<T>
    {
        public IEnumerable<T> Musics { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
