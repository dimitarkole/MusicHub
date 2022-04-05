namespace MusicHub.Web.ViewModels.PlayLaterMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlayLaterMusicEditInputModel : IMapTo<PlayLaterMusic>
    {
        [Range(0, Int32.MaxValue)]
        public int Position { get; set; }
    }
}
