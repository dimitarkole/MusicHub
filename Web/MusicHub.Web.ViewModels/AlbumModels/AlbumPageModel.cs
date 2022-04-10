namespace MusicHub.Web.ViewModels.AlbumModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class AlbumPageModel<T> : PageModel
    {
        public IEnumerable<T> Albums { get; set; }
    }
}
