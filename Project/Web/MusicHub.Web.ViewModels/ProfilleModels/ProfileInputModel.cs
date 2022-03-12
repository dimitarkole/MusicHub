namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class ProfileInputModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string Username { get; set; }

        public string Email { get; set; }
    }
}
