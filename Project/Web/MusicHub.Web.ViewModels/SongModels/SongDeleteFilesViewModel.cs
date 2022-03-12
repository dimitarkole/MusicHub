namespace MusicHub.Web.ViewModels.SongModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongDeleteFilesViewModel : IMapFrom<Song>
    {
        public string ImageFilePath { get; set; }

        public string AudioFilePath { get; set; }
    }
}
