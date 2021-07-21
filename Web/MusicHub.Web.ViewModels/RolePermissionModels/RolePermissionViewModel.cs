namespace MusicHub.Web.ViewModels.RolePermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PermissionModels;

    public class RolePermissionViewModel : IMapFrom<RolePermission>
    {
        //public int PermissionId { get; set; }

        public PermissionViewModel Permission { get; set; }
    }
}
