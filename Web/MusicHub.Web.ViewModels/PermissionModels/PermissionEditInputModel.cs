namespace MusicHub.Web.ViewModels.PermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class PermissionEditInputModel : IMapTo<Permission>
    {
        public PermissionType Type { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
