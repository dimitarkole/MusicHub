namespace MusicHub.Web.ViewModels.RoleModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PermissionModels;
    using MusicHub.Web.ViewModels.RolePermissionModels;

    public class RoleDetailsModel : IMapFrom<Role>
    {
        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }

        public int UsersCount { get; set; }

        public ICollection<RolePermissionViewModel> Permissions { get; set; }
    }
}
