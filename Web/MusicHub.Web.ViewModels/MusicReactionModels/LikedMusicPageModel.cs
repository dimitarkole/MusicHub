namespace MusicHub.Web.ViewModels.MusicReactionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class LikedMusicPageModel<T> : PageModel
    {
        public IEnumerable<T> LikedMusics { get; set; }
    }
}
