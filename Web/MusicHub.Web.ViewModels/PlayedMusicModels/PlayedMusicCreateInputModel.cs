namespace MusicHub.Web.ViewModels.PlayedMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlayedMusicCreateInputModel : IMapTo<PlayedMusic>
    {
        public string MusicId { get; set; }
    }
}
