namespace MusicHub.Web.ViewModels.FileModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class FileInputModel // : IMapTo<>
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
