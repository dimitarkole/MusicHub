namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;

    using static MusicHub.Common.GlobalConstants;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var permissions = dbContext.Permissions.ToList();
            RoleManager<Role> roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            await this.SeedRoleAsync(roleManager, Roles.SuperAdministrator, permissions);
            await this.SeedRoleAsync(roleManager, Roles.Administrator, permissions);
            await this.SeedRoleAsync(roleManager, Roles.DataAnalisator, permissions);
            await this.SeedRoleAsync(roleManager, Roles.TeamAdministrator, permissions);
            await this.SeedRoleAsync(roleManager, Roles.Artist, permissions);
            await this.SeedRoleAsync(roleManager, Roles.User, permissions);
        }

        private async Task SeedRoleAsync(RoleManager<Role> roleManager, string roleName, List<Permission> permissions)
        {
            Role role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var newRole = new Role { Name = roleName };
                ICollection<Permission> rolePermissions = this.GetRolePremissions(roleName, permissions);
                newRole.Permissions = rolePermissions.Select(p => new RolePermission
                {
                    PermissionId = p.Id,
                })
                .ToList();

                IdentityResult result = await roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private ICollection<Permission> GetRolePremissions(string roleName, List<Permission> permissions)
        {
            if (roleName == Roles.SuperAdministrator)
            {
                return permissions;
            }
            else if (roleName == Roles.Administrator)
            {
                return permissions
                    .Where(p => p.Type != PermissionType.UserAdministration)
                    .ToList();
            }
            else if (roleName == Roles.DataAnalisator)
            {
                return permissions
                    .Where(p => p.Type != PermissionType.UserAdministration &&
                        p.Type != PermissionType.LicenseCreator &&
                        p.Type != PermissionType.CategoryAdministrator)
                   .ToList();
            }
            else if (roleName == Roles.DataAnalisator)
            {
                return permissions
                  .Where(p => p.Type == PermissionType.DataAnalisator)
                  .ToList();
            }
            else if (roleName == Roles.TeamAdministrator)
            {
                return permissions
                    .Where(p => p.Type != PermissionType.UserAdministration
                        && p.Type != PermissionType.LicenseCreator
                        && p.Type != PermissionType.CategoryAdministrator
                        && p.Type != PermissionType.DataAnalisator)
                    .ToList();
            }
            else if (roleName == Roles.Artist)
            {
                return permissions
                    .Where(p => p.Type != PermissionType.UserAdministration
                        && p.Type != PermissionType.LicenseCreator
                        && p.Type != PermissionType.CategoryAdministrator
                        && p.Type != PermissionType.DataAnalisator
                        && p.Type != PermissionType.TeamManager)
                    .ToList();
            }
            else if (roleName == Roles.User)
            {
                return permissions
                    .Where(p => p.Type == PermissionType.User
                        && p.Type == PermissionType.CommentCreator
                        && p.Type == PermissionType.PlaylistCreator)
                    .ToList();
            }

            return new List<Permission>();
        }
    }
}
