namespace MusicHub.Web.ViewModels.FileModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Services.Mapping;

    public class FileViewModel // : IMapFrom<>
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public string UserId { get; set; }

        public string Link { get; set; }
    }
}
