namespace MusicHub.Web.ViewModels.BellModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class BellerViewModel : IMapFrom<Bell>
    {
        public string Id { get; set; }

        public BellerUserViewModel Beller { get; set; }
    }
}
