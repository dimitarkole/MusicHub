namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class PasswordChangeInputModel
    {
        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string CurrentPassword { get; set; }
    }
}
