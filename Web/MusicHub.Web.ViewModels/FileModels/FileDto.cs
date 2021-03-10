namespace MusicHub.Web.ViewModels.FileModels
{
    using MusicHub.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileDto //: IMapFrom<MailItemFile>,
    {
        public string Path { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }
    }
}
