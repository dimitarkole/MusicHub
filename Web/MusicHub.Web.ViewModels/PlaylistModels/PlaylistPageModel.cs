namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Web.ViewModels.CommonModels;

    public class PlaylistPageModel<T> : PageModel
    {
        public IEnumerable<T> Playlists { get; set; }
    }
}
