namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common;
    using MusicHub.Data.Models.Enums;

    public class Permission : Entity<int>
    {
        public Permission()
        {
            this.Roles = new List<RolePermission>();
        }

        public PermissionType Type { get; set; }

        public string Description { get; set; }

        public ICollection<RolePermission> Roles { get; set; }
    }
}
