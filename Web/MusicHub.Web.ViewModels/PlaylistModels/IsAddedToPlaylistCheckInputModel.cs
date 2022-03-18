namespace MusicHub.Web.ViewModels.PlaylistModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IsAddedToPlaylistCheckInputModel
    {
        public string PlaylistId { get; set; }

        public string MusicId { get; set; }
    }
}
