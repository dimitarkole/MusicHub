namespace MusicHub.Web.ViewModels.MusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;

    public class MusicFilter
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
