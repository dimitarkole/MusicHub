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
            var expectedResult = this.context.Roles
                    .OrderBy(r => r.Name)
                    .To<RoleViewModel>()
                    .ToList();

            var entitesPerPage = PaginationData.RolesPerPage;
            var result = roleService.All<RoleViewModel>(page, entitesPerPage);
            var countPages = this.GetDataPagesCount(expectedResult.Count(), entitesPerPage);

            var expectedRolesList = expectedResult.GetPage(page, entitesPerPage)
               .ToList();

            this.CheckRoleViewModelsIsEqual(page, expectedRolesList, result, countPages);
        }

        [Fact]
        public async Task GetAllRoles_WithValidData_ShouldWorkCurrect()
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);
            var expectedResult = this.context.Roles
                    .OrderBy(r => r.Name)
                    .To<RoleViewModel>()
                    .ToList();

            var result = roleService.All<RoleViewModel>().ToList();

            this.CheckRoleViewModelsIsEqual(expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Filter_WithValidData_ShouldeWorkCurrect(int page)
        {
            var roleService = await this.CreateRoleService(RoleTestsData.GetRoles);
            IQueryable<Role> roles = this.context.Roles;

            var entitesPerPage = PaginationData.RolesPerPage;
            var filter = RoleTestsData.FilterModel;
            roles = this.FilteringData(filter, roles);
            roles = this.OrederSearchData(filter.OrderMethod, roles);
            var result = roleService.Filter<RoleViewModel>(page, entitesPerPage, filter);
            var countPages = this.GetDataPagesCount(roles.Count(), entitesPerPage);

            var expectedRoles = roles
                    .To<RoleViewModel>()
                    .GetPage(page, entitesPerPage)
                    .ToList();

            this.CheckRoleViewModelsIsEqual(page, expectedRoles, result, countPages);
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

        private void CheckRoleViewModelsIsEqual(List<RoleViewModel> expectedRoles, List<RoleViewModel> result)
        {
            Assert.Equal(expectedRoles.Count, result.Count);

            for (int i = 0; i < expectedRoles.Count(); i++)
            {
                var expectedRole = expectedRoles[i];
                var resultRole = result[i];
                Assert.True(this.CheckRoleViewModelIsEqual(expectedRole, resultRole));
                Assert.True(this.CheckRoleViewModelIsEqual(expectedRole, resultRole));
            }
        }

        private void CheckRoleViewModelsIsEqual(int page, List<RoleViewModel> expectedRoles, RolesAllViewModel<RoleViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultRoles = result.Roles.ToList();
            Assert.Equal(resultRoles.Count, expectedRoles.Count);

            for (int i = 0; i < expectedRoles.Count(); i++)
            {
                var expectedRole = expectedRoles[i];
                var resultRole = resultRoles[i];
                Assert.True(this.CheckRoleViewModelIsEqual(expectedRole, resultRole));
                Assert.True(this.CheckRoleViewModelIsEqual(expectedRole, resultRole));
            }
        }

        private bool CheckRoleViewModelIsEqual(RoleViewModel expectedRole, RoleViewModel resultRole)
            => expectedRole.Name == resultRole.Name
                && expectedRole.PermissionsCount == resultRole.PermissionsCount
                && expectedRole.Id == resultRole.Id
                && expectedRole.UsersCount == resultRole.UsersCount;

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private void CheckRolePermissions(string roleName, ICollection<RolePermissionCreateInputModel> permissionIds, string roleId)
        {
            Assert.True(this.context.Roles.Any(r => r.Name == roleName));

            var ectualCount = this.context.RolePermissions.Where(rp => rp.RoleId == roleId).Count();
            Assert.Equal(permissionIds.Count, ectualCount);
            foreach (var permissionId in permissionIds)
            {
                Assert.True(this.context.RolePermissions.Any(rp => rp.PermissionId == permissionId.PermissionId
                    && rp.RoleId == roleId));
            }
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
