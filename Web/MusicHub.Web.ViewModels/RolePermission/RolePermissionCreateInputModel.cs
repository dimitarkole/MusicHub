using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.RolePermission
{
    public class RolePermissionCreateInputModel : IMapTo<MusicHub.Data.Models.RolePermission>
    {
        public int PermissionId { get; set; }
    }
}
