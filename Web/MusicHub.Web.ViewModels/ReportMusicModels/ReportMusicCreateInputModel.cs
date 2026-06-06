namespace MusicHub.Web.ViewModels.ReportMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class ReportMusicCreateInputModel : IMapTo<ReportMusic>
    {
        [Required]
        public ReportMusicType Type { get; set; }

        [Required]
        public string MusicId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
