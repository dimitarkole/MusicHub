using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.Web.ViewModels.Users
{
    public class UserProfilePictueInputModel : IMapTo<User>
    {
        [Required]
        public string ImageUrl { get; set; }
    }
}
