namespace MusicHub.Web.ViewModels.SongModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.CategoryModels;
    using Microsoft.AspNetCore.Http;
    using MusicHub.Common;

    public class SongCreateInputModel : IMapTo<Song>
    {
        [MaxLength(150)]
        public string Name { get; set; }

        public string CategoryId { get; set; }

        [MaxLength(1000)]
        public string Text { get; set; }

        public string ImageFilePath { get; set; }

        public string AudioFilePath { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }
    }
}
