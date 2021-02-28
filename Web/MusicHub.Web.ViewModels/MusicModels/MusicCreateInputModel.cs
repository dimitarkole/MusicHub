namespace MusicHub.Web.ViewModels.MusicModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    using static MusicHub.Common.ModelConstants;

    public class MusicCreateInputModel : IMapTo<Music>
    {
        [Required]
        [MinLength(MusicConstants.NameMinLength)]
        [MaxLength(MusicConstants.NameMaxLength)]
        public string Name { get; set; }

        public string CategoryId { get; set; }

        [Required]
        [MinLength(MusicConstants.DescriptionMinLength)]
        [MaxLength(MusicConstants.DescriptionMaxLength)]
        public string Descriptio { get; set; }

        public string ImageFilePath { get; set; }

        public string AudioFilePath { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }
    }
}
