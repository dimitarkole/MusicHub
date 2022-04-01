namespace MusicHub.Web.ViewModels.PlayedMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PlayedMusicCreateInputModel : IMapTo<PlayedMusic>
    {
        [Required]
        public string MusicId { get; set; }
    }
}
