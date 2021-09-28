namespace MusicHub.Web.ViewModels.RolePermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class RolePermissionCreateInputModel : IMapTo<RolePermission>, IMapFrom<RolePermission>
    {
        public int PermissionId { get; set; }
    }
}
