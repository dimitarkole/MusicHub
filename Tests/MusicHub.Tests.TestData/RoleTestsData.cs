namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.RoleModels;
    using MusicHub.Web.ViewModels.RolePermissionModels;
    using static MusicHub.Common.GlobalConstants;

    public static class RoleTestsData
    {
        public static RoleCreateInputModel CreateModel => new RoleCreateInputModel()
        {
            Name = "role name",
        };

        public static RoleFilter FilterModel => new RoleFilter()
        {
            Name = "role name",
        };

        public static string UpdateRoleId => GetRoles[0].Id;

        public static string DeleteRoleId => GetRoles[0].Id;

        public static string GetById => GetRoles[0].Id;

        public static RoleEditInputModel UpdateModel => new RoleEditInputModel()
        {
            Name = "edited role name",
        };

        public static List<Role> GetRoles => new List<Role>() {
            new Role { Id = "Role1", Name = Roles.SuperAdministrator },
            new Role { Id = "Role2", Name = Roles.Administrator },
            new Role { Id = "Role3", Name = Roles.DataAnalisator },
            new Role { Id = "Role4", Name = Roles.TeamAdministrator },
            new Role { Id = "Role5", Name = Roles.Artist },
            new Role { Id = "Role6", Name = Roles.User },
        };

        public static List<Permission> Permissions => PermissionTestsData.Permissions();

        public static List<User> Users => UserTestsData.Users;

        public static List<UserRole> UsersRoles()
        {
            var result = new List<UserRole>();
            var random = new Random();
            foreach (var user in Users)
            {
                var roleId = GetRoles[random.Next(1, GetRoles.Count - 1)].Id;
                var userRole = new UserRole()
                {
                    RoleId = roleId,
                    UserId = user.Id,
                };

                result.Add(userRole);
            }

            return result;
        }
    }
}
