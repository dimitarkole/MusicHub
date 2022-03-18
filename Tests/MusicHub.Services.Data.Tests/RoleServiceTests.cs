namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.RoleModels;
    using MusicHub.Web.ViewModels.RolePermissionModels;
    using Xunit;

    using static MusicHub.Common.GlobalConstants;
    using FluentAssertions;

    public class RoleServiceTests : BaseTestClass
    {
        [Fact]
        public async Task CreateRole_WithValidData_ShouldWorkCorrect()
        {
            var roleService = await this.CreateRoleService(new List<Role>());
            var model = RoleTestsData.CreateModel;
            model.PermissionIds = this.context.RolePermissions.Where(p => p.PermissionId < 5)
                .Select(p => new RolePermissionCreateInputModel()
                {
                    PermissionId = p.PermissionId,
                })
                .ToArray();
            await roleService.Create(model);

            var roleId = this.context.Roles.Where(r => r.Name == model.Name)
                .FirstOrDefault()
                .Id;
            this.CheckRolePermissions(model.Name, model.PermissionIds, roleId);
        }

        [Fact]
        public async Task UpdateRole_WithValidData_ShouldWorkCorrect()
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);

            var model = RoleTestsData.UpdateModel;
            model.PermissionIds = this.context.Permissions
                .Where(p => p.Id > 5)
                .Select(p => new RolePermissionCreateInputModel()
                {
                    PermissionId = p.Id,
                })
                .ToArray();
            var roleId = RoleTestsData.UpdateRoleId;
            await roleService.Update(roleId, model);

            this.CheckRolePermissions(model.Name, model.PermissionIds, roleId);
        }

        [Fact]
        public async Task DeleteRole_WithValidData_ShouldWorkCorrect()
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);
            var roleId = RoleTestsData.DeleteRoleId;

            await roleService.Delete(roleId);

            Assert.False(this.context.Roles.Any(r => r.Id == roleId));
            Assert.False(this.context.RolePermissions.Any(r => r.RoleId == roleId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllRolesPerPage_WithValidData_ShouldWorkCurrect(int page)
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);
            var entitesPerPage = PaginationData.RolesPerPage;

            var roles = this.context.Roles
                 .OrderBy(r => r.Name)
                 .To<RoleViewModel>();

            var expected = new RolePageModel<RoleViewModel>()
            {
                Roles = roles
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(roles.Count(), entitesPerPage),
            };

            var actual = roleService.All<RoleViewModel>(page, entitesPerPage);

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetAllRoles_WithValidData_ShouldWorkCurrect()
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);

            var expected = this.context.Roles
                  .OrderBy(r => r.Name)
                  .To<RoleViewModel>();

            var actual = roleService.All<RoleViewModel>().ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Filter_WithValidData_ShouldeWorkCurrect(int page)
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);
            var entitesPerPage = PaginationData.RolesPerPage;
            var filter = RoleTestsData.FilterModel;

            IQueryable<Role> roles = this.context.Roles;
            roles = this.FilteringData(filter, roles);
            roles = this.OrederSearchData(filter.OrderMethod, roles);

            var expected = new RolePageModel<RoleViewModel>()
            {
                Roles = roles
                    .GetPage(page, entitesPerPage)
                    .To<RoleViewModel>()
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(roles.Count(), entitesPerPage),
            };

            var actual = roleService.Filter<RoleViewModel>(page, entitesPerPage, filter);

            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCorrect()
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);
            var roleId = RoleTestsData.GetById;
            var rolePermissions = this.context.RolePermissions
                .Where(rp => rp.RoleId == roleId).ToList();
            var result = roleService.GetById(roleId);

            Assert.True(this.context.Roles.Any(r => r.Id == roleId
                  && r.Name == result.Name));
            var expectedRolePermissions = this.context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToList();
            Assert.Equal(expectedRolePermissions.Count, result.Permissions.Count);

            /*foreach (var permission in result.Permissions)
            {

                Assert.True(this.context.RolePermissions.Any(rp => rp.RoleId == roleId
                      && rp.PermissionId == permission.PermissionId
                      /*&& rp.Permission.Id == permission.Permission.Id
                      && rp.Permission.Type.ToString() == permission.Permission.Type
                      && rp.Permission.Description == permission.Permission.Description*));
            }*/
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private void CheckRolePermissions(string roleName, ICollection<RolePermissionCreateInputModel> actual, string roleId)
        {
            Assert.True(this.context.Roles.Any(r => r.Name == roleName));

            var expected = this.context.RolePermissions.Where(rp => rp.RoleId == roleId)
                .To<RolePermissionCreateInputModel>()
                .ToList();

            expected.Should().BeEquivalentTo(actual);
        }

        private IQueryable<Role> FilteringData(RoleFilter filter, IQueryable<Role> roles)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                roles = roles.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return roles;
        }

        private IQueryable<Role> OrederSearchData(OrderMethod orderMethod, IQueryable<Role> roles)
        {
            if (orderMethod == OrderMethod.NameAsc)
            {
                roles = roles.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                roles = roles.OrderByDescending(s => s.Name);
            }

            return roles;
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

        private async Task<RoleService> CreateRoleService(List<Role> roles)
        {
            await this.context.Roles.AddRangeAsync(roles);
            await this.context.Users.AddRangeAsync(RoleTestsData.Users);
            await this.context.Permissions.AddRangeAsync(RoleTestsData.Permissions);
            await this.context.SaveChangesAsync();
            var service = new RoleService(this.context, new PaginationService());
            if (roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    await this.AddPermissionsToRole(role.Name);
                }

                await this.context.UserRoles.AddRangeAsync(RoleTestsData.UsersRoles());
                await this.context.SaveChangesAsync();
            }

            return service;
        }
    }
}
