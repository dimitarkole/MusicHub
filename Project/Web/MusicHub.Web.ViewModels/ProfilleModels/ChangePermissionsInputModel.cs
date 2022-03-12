namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ChangePermissionsInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string[] Roles { get; set; }
    }
}
