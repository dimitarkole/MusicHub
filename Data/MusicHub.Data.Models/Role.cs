// ReSharper disable VirtualMemberCallInConstructor
namespace MusicHub.Data.Models
{
    using System;

    using MusicHub.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class Role : IdentityRole
    {
        public Role()
        {
            this.Permissions = new List<RolePermission>();
            this.Users = new List<UserRole>();
        }

        public ICollection<RolePermission> Permissions { get; set; }

        public ICollection<UserRole> Users { get; set; }
    }
}
