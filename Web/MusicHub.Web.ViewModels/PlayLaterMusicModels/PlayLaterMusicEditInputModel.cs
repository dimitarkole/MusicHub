namespace MusicHub.Web.ViewModels.PlayLaterMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlayLaterMusicEditInputModel : IMapTo<PlayLaterMusic>
    {
        public int Position { get; set; }
    }
}
