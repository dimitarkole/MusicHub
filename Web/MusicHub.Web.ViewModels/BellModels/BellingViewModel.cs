namespace MusicHub.Web.ViewModels.BellModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class BellingViewModel : IMapFrom<Bell>
    {
        public string Id { get; set; }

        public BellerUserViewModel Belling { get; set; }
    }
}
