namespace MusicHub.Web.ViewModels.PlaylistMusicModels
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
