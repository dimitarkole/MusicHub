namespace MusicHub.Web.ViewModels.RoleModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PermissionModels;

    public class RoleDetailsModel : IMapFrom<Role>
    {
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }

        [NotMapped]
        public PermissionsAllViewModel<PermissionViewModel> PermissionsPagination { get; set; }
    }
}
