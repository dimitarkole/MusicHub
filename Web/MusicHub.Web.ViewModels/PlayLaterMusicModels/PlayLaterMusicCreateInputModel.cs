namespace MusicHub.Web.ViewModels.PlayLaterMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlayLaterMusicCreateInputModel : IMapTo<PlayLaterMusic>
    {
        public string MusicId { get; set; }
    }
}
