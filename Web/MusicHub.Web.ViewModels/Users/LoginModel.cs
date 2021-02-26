using System.ComponentModel.DataAnnotations;

namespace MusicHub.Web.ViewModels.Users
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
