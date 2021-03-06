namespace MusicHub.Web.ViewModels.LicenseModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class LicenseUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string UserName { get; set; }
    }
}
