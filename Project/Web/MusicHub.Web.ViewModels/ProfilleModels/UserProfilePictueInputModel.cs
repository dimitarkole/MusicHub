namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserProfilePictueInputModel : IMapTo<ApplicationUser>
    {
        [Required]
        public string ImageUrl { get; set; }
    }
}
