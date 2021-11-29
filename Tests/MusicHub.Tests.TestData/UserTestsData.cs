using MusicHub.Common.Extensions;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using static MusicHub.Common.GlobalConstants;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MusicHub.Web.ViewModels.VerificationCodeModels;

namespace MusicHub.Tests.TestData
{
    public static class UserTestsData
    {
        public static string UserName => Users[0].UserName;

        public static string UserEmail => Users[0].Email;

        public static string HasPermissionTrueUserId => Users[0].Id;

        public static PermissionType HasPermissionTruePermitionType => PermissionType.User;

        public static ChangePermissionsInputModel ChangePermissionsInputModel => new ChangePermissionsInputModel()
        {
            Id = Users[0].Id,
        };

        public static string HasPermissionFalseUserId => Users[1].Id;

        public static PermissionType HasPermissionFalsePermitionType => PermissionType.CategoryAdministrator;

        public static string UserId => Users[0].Id;

        public static string SearchQuery => "1";

        public static FindUserInputModel FindModel => new FindUserInputModel()
        {
            SearchQuery = SearchQuery,
        };

        public static UserFilter FilterModel => new UserFilter()
        {
            FirstName = "First Name1",
            LastName = "Last",
        };

        public static UserFilter FilterModelWithEmail => new UserFilter()
        {
            FirstName = "First Name1",
            LastName = "Last",
            Email = Users[0].Email,
        };

        public static List<string> UsersIds => Users.Select(u => u.Id)
            .Take(5)
            .ToList();

        public static UserProfilePictueInputModel UserProfilePictueInput => new UserProfilePictueInputModel() 
        {
            ImageUrl = "updated image url",
        };

        public static UserEditInputModel UpdateModel => new UserEditInputModel()
        {
            FirstName = "updated first name",
            Email = "updated_email@gmail.com",
            LastName = "updated lase name",
            Birthday = DateTime.Parse("26.03.2002"),
            UserName = "updated username",
        };

        public static UserEditInputModel UpdateModelWithRoles => new UserEditInputModel()
        {
            FirstName = "updated first name",
            Email = "updated_email@gmail.com",
            LastName = "updated lase name",
            Birthday = DateTime.Parse("26.03.2002"),
            Roles = UpdateRoles(),
            UserName = "updated username",
        };

        public static List<User> Users => new List<User>()
        {
            new User() { Id ="testUserId1", FirstName = "First Name1", LastName = "Last Name1", UserName= "UserName1", ImageUrl = "image", Roles = new List<UserRole>() },
            new User() { Id ="testUserId2", FirstName = "First Name2", LastName = "Last Name2",  UserName= "UserName2", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId3", FirstName = "First Name3", LastName = "Last Name3",  UserName= "UserName3", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId4", FirstName = "First Name4", LastName = "Last Name4",  UserName= "UserName4", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId5", FirstName = "First Name5", LastName = "Last Name5",  UserName= "UserName5", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId6", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName6", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId7", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName7", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId8", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName8", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId9", FirstName = "First Name6", LastName = "Last Name6",  UserName= "UserName9", ImageUrl = "image", Roles = new List<UserRole>()},
            new User() { Id ="testUserId10", FirstName = "First Name10", LastName = "Last Name10",  UserName= "UserName10", ImageUrl = "image", Roles = new List<UserRole>()},
        };

        public static List<UserRole> UserRoles => new List<UserRole>()
        {
            new UserRole(){RoleId = Roles.SuperAdministrator, UserId = Users[0].Id},
            new UserRole(){RoleId = Roles.User, UserId = Users[1].Id},
            new UserRole(){RoleId = Roles.DataAnalisator, UserId = Users[3].Id},
            new UserRole(){RoleId = Roles.Administrator, UserId = Users[4].Id},
        };

        public static int ToggleSettingId => UserSettings[0].Id;

        public static List<UserSetting> UserSettings => new List<UserSetting>()
        {
            new UserSetting() {Id= 1, Type = SettingType.ReceiveEventChangesEmail, Enabled= true, UserId = Users[0].Id },
            new UserSetting() {Id= 2, Type = SettingType.ReceiveEventChangesNotification, Enabled= false, UserId = Users[0].Id },
            new UserSetting() {Id= 3, Type = SettingType.ReceiveEventChangesEmail, Enabled= false, UserId = Users[1].Id },
            new UserSetting() {Id= 4, Type = SettingType.ReceiveEventChangesNotification, Enabled= true, UserId = Users[1].Id },
            new UserSetting() {Id= 5, Type = SettingType.ReceiveEventChangesEmail, Enabled= true, UserId = Users[2].Id },
            new UserSetting() {Id= 6, Type = SettingType.ReceiveEventChangesNotification, Enabled= true, UserId = Users[2].Id },
            new UserSetting() {Id= 7, Type = SettingType.ReceiveEventChangesEmail, Enabled= true, UserId = Users[3].Id },
            new UserSetting() {Id= 8, Type = SettingType.ReceiveEventChangesNotification, Enabled= true, UserId = Users[3].Id },
        };

        public static List<Permission> Permissions()
        {
            var permissionTypesByDescription = EnumExtensions.GetEnumValuesByDisplayName<PermissionType>();
            List<Permission> permissions = new List<Permission>();
            int id = 1;
            foreach (var permissionTypeByDescription in permissionTypesByDescription)
            {
                PermissionType type = permissionTypeByDescription.Key;
                string description = permissionTypeByDescription.Value;
                permissions.Add(new Permission
                {
                    Id = id,
                    Type = type,
                    Description = description,
                    Roles = new List<RolePermission>()
                });

                id++;
            }

            return permissions;
        }

        public static List<Role> GetRoles()
        {
            var roles = new List<Role>();
            var permissions = Permissions();

            roles.Add(GenRole(Roles.User, permissions));
            roles.Add(GenRole(Roles.Administrator, permissions));
            roles.Add(GenRole(Roles.DataAnalisator, permissions));
            roles.Add(GenRole(Roles.SuperAdministrator, permissions));

            return roles;
        }

        private static Role GenRole(string roleName, List<Permission> permissions)
        {
            var newRole = new Role { Id = roleName, Name = roleName };
            ICollection<Permission> rolePermissions = GetRolePremissions(roleName, permissions);
            newRole.Permissions = rolePermissions.Select(p => new RolePermission
            {
                PermissionId = p.Id,
            })
            .ToList();

            return newRole;
        }

        private static ICollection<Permission> GetRolePremissions(string roleName, List<Permission> permissions)
        {
            if (roleName == Roles.SuperAdministrator)
            {
                return permissions;
            }
            else if (roleName == Roles.Administrator)
            {
                return permissions.Where(p =>
                    p.Type != PermissionType.UserAdministration &&
                    p.Type != PermissionType.ManageUserPermissions &&
                    p.Type != PermissionType.FindAndReplaceUsers)
                    .ToList();
            }
            else if (roleName == Roles.DataAnalisator)
            {
                return permissions.Where(p =>
                   p.Type != PermissionType.UserAdministration &&
                   p.Type != PermissionType.ManageUserPermissions &&
                   p.Type != PermissionType.FindAndReplaceUsers)
                   .ToList();
            }
            else if (roleName == Roles.User)
            {
                return permissions.Where(p =>
                    p.Type == PermissionType.User)
                    .ToList();
            }

            return new List<Permission>();
        }

        public static string[] UpdateRoles()
        {
            var result = new List<string>();

            return result.ToArray();
        }
    }
}
