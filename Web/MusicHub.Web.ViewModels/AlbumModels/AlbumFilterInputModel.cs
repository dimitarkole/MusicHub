namespace MusicHub.Web.ViewModels.AlbumModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    public class AlbumFilterInputModel
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
