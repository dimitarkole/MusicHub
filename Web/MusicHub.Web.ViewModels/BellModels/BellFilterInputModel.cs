namespace MusicHub.Web.ViewModels.BellModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class BellFilterInputModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public OrderMethod OrderMethod { get; set; }
    }
}
