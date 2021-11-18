using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.MusicReactionModels
{
    public class LikedMusicsAllViewModel<T>
    {
        public IEnumerable<T> LikedMusics { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
