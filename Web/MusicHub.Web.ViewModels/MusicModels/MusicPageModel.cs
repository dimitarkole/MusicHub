namespace MusicHub.Web.ViewModels.MusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class MusicPageModel<T> : PageModel
    {
        public IEnumerable<T> Musics { get; set; }
    }
}
