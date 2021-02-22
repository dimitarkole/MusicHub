using System.ComponentModel.DataAnnotations;

namespace MusicHub.Web.ViewModels.Users
{
    public class ChangePermissionsInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string[] Roles { get; set; }
    }
}
