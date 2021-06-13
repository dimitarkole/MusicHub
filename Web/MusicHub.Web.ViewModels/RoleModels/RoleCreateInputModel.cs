namespace MusicHub.Web.ViewModels.RoleModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class RoleCreateInputModel : IMapTo<Role>
    {
        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }

        public ICollection<string> PermissionIds { get; set; }
    }
}
