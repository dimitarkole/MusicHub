using MusicHub.Common.Extensions;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Web.ViewModels.PermissionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public static class PermissionTestsData
    {
        public static PermissionCreateInputModel CreateModel => new PermissionCreateInputModel()
        {
            Description = "some description",
            Type = PermissionType.CategoryAdministrator,
        };

        public static PermissionEditInputModel UpdateModel => new PermissionEditInputModel()
        {
            Description = "some description 1",
            Type = PermissionType.FindAndReplaceUsers,
        };

        public static List<Permission> Permissions()
        {
            var permissionTypesByDescription = EnumExtensions.GetEnumValuesByDisplayName<PermissionType>();
            var permissions = new List<Permission>();
            int id = 0;
            foreach (var permissionTypeByDescription in permissionTypesByDescription)
            {
                PermissionType type = permissionTypeByDescription.Key;
                string description = permissionTypeByDescription.Value;

                var newPermission = new Permission
                {
                    Id = ++id,
                    Type = type,
                    Description = description,
                };
                permissions.Add(newPermission);
            }

            return permissions;
        }

        public static List<Role> Roles => RoleTestsData.GetRoles;

        public static List<User> Users => UserTestsData.Users;

        public static List<UserRole> UsersRoles => RoleTestsData.UsersRoles();
    }
}
