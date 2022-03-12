namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class PasswordChangeWithoutAuthInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string CurrentPassword { get; set; }
    }
}
