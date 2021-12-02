namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.PermissionModels;
    using Xunit;
    using static MusicHub.Common.GlobalConstants;

    public class PermissionServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreatePermission_WithValidData_ShouldWorkCorrect()
        {
            var permissionService = await this.CreatePermissionService(new List<Permission>());
            var model = PermissionTestsData.CreateModel;
            await permissionService.Create(model);

            Assert.True(this.context.Permissions.Any(p => p.Description == model.Description
                && p.Type == model.Type));
        }

        [Fact]
        public async Task DeletePermission_WithValidData_ShouldWorkCorrect()
        {
            var permissionService = await this.CreatePermissionService(PermissionTestsData.Permissions());
            var permissionId = this.context.Permissions
                .FirstOrDefault()
                .Id;

            await permissionService.Delete(permissionId);

            Assert.False(this.context.Permissions.Any(r => r.Id == permissionId));
        }

        [Fact]
        public async Task UpdatePermission_WithValidData_ShouldWorkCorrect()
        {
            var permissionService = await this.CreatePermissionService(PermissionTestsData.Permissions());

            var model = PermissionTestsData.UpdateModel;
            var permissionId = this.context.Permissions
                .FirstOrDefault()
                .Id;
            await permissionService.Update(permissionId, model);

            Assert.True(this.context.Permissions.Any(p => p.Description == model.Description
                && p.Type == model.Type));
        }

        [Fact]
        public async Task GetAllPermissions_WithValidData_ShouldWorkCurrect()
        {
            var permissionService = await this.CreatePermissionService(PermissionTestsData.Permissions());

            var expected = this.context.Permissions
                    .To<PermissionViewModel>()
                    .ToList();

            var actual = permissionService.All<PermissionViewModel>().ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCurrect()
        {
            var permissionService = await this.CreatePermissionService(PermissionTestsData.Permissions());

            var permissionId = this.context.Permissions
                .FirstOrDefault()
                .Id;
            var expected = this.context.Permissions
                .Where(p => p.Id == permissionId)
                .To<PermissionViewModel>()
                .FirstOrDefault();

            var actual = permissionService.GetById<PermissionViewModel>(permissionId);
            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetPermissionsByRoleName_WithValidData_ShouldWorkCurrect()
        {
            var permissionService = await this.CreatePermissionService(PermissionTestsData.Permissions());

            var roleName = this.context.Roles
                .FirstOrDefault()
                .Name;
            var expected = this.context.RolePermissions
                .Include(rp => rp.Role)
                .Where(rp => rp.Role.Name == roleName)
                .Select(rp => new PermissionViewModel
                {
                    Description = rp.Permission.Description,
                    Id = rp.Permission.Id,
                    Type = rp.Permission.Type.ToString(),
                })
                .ToList();

            var actual = permissionService.GetPermissions(roleName).ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetUserPermissions_WithValidData_ShouldWorkCurrect()
        {
            var permissionService = await this.CreatePermissionService(PermissionTestsData.Permissions());
            var userId = this.context.Users
                .FirstOrDefault()
                .Id;
            var userRoles = this.context.UserRoles
                .Where(ur => ur.UserId == userId);

            var expected = this.context.RolePermissions
                 .Include(rp => rp.Role)
                 .Where(rp => userRoles.Any(ur => ur.RoleId == rp.RoleId))
                 .Select(rp => new PermissionViewModel
                 {
                     Description = rp.Permission.Description,
                     Id = rp.Permission.Id,
                     Type = rp.Permission.Type.ToString(),
                 })
                 .ToList();

            var actual = permissionService.GetUserPermissions(userId).ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        private async Task AddPermissionsToRole(string roleName)
        {
            var permissions = this.context.Permissions.ToList();
            var rolePermissions = GetRolePremissions(roleName, permissions);
            var roleId = this.context.Roles
                .Where(r => r.Name == roleName)
                .FirstOrDefault()
                .Id;
            foreach (var rolePermission in rolePermissions)
            {
                var newRolePermission = new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId = rolePermission.Id,
                };

                await this.context.RolePermissions.AddAsync(newRolePermission);
            }
        }

        private static ICollection<Permission> GetRolePremissions(string roleName, List<Permission> permissions)
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
                        p.Type != PermissionType.ManageUserPermissions &&
                        p.Type != PermissionType.CategoryAdministrator)
                   .ToList();
            }
            else if (roleName == Roles.DataAnalisator)
            {
                return permissions
                  .Where(p => p.Type == PermissionType.DataAnalisator)
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

        private async Task<PermissionsService> CreatePermissionService(List<Permission> permissions)
        {
            await this.context.Permissions.AddRangeAsync(permissions);
            if (permissions.Count > 0)
            {
                var roles = PermissionTestsData.Roles;
                await this.context.Roles.AddRangeAsync(roles);
                await this.context.Users.AddRangeAsync(RoleTestsData.Users);
                await this.context.SaveChangesAsync();
                foreach (var role in roles)
                {
                    await this.AddPermissionsToRole(role.Name);
                }

                await this.context.UserRoles.AddRangeAsync(RoleTestsData.UsersRoles());
                await this.context.SaveChangesAsync();
            }

            await this.context.SaveChangesAsync();
            var service = new PermissionsService(this.context, new PaginationService());

            return service;
        }
    }
}
