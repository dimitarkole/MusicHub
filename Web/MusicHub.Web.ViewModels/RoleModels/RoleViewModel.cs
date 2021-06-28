namespace MusicHub.Web.ViewModels.RoleModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class RoleViewModel : IMapFrom<Role>
    {
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }

        public int PermissionsCount { get; set; }

        public int UsersCount { get; set; }
    }
}
