namespace MusicHub.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class PasswordChangeWithoutAuthInputModel
    {
        [Required]
        public string Password { get; set; }
    }
}
