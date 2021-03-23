using System;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.Web.ViewModels.Users
{
    public class UserInputModel
    {
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
