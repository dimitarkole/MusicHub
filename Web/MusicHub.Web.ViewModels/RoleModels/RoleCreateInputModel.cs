namespace MusicHub.Web.ViewModels.RoleModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.RolePermissionModels;

    public class RoleCreateInputModel : IMapTo<Role>
    {
        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }

        [NotMapped]
        // [Required]
        // [MinLength(1)]
        public ICollection<RolePermissionCreateInputModel> PermissionIds { get; set; }
    }
}
