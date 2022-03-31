using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.PermissionModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class PermissionsControllerTests
    {
        [Fact]
        public void GetAllPermissions_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var users = PermissionTestsData.Users;
            var userId = users[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.All())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<PermissionViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new PermissionsService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PermissionViewModel>();
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var users = PermissionTestsData.Users;
            var userId = users[0].Id;
            var permissionId = permissions[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(permissionId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PermissionViewModel>()
                    .Passing(actual =>
                    {
                        var service = new PermissionsService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<PermissionViewModel>(permissionId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetSuperAdminPermissions_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var roles = PermissionTestsData.Roles;
            var users = PermissionTestsData.Users;
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userId = users[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(rolePermissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetSuperAdminPermissions())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<PermissionViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new PermissionsService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetPermissions(Roles.SuperAdministrator);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetAdminPermissions_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var roles = PermissionTestsData.Roles;
            var users = PermissionTestsData.Users;
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userId = users[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(rolePermissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetAdminPermissions())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<PermissionViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new PermissionsService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetPermissions(Roles.Administrator);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetUserPermissions_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var roles = PermissionTestsData.Roles;
            var users = PermissionTestsData.Users;
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userId = users[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(rolePermissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetUserPermissions())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<PermissionViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new PermissionsService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetPermissions(Roles.User);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetOwnPermissions_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var roles = PermissionTestsData.Roles;
            var users = PermissionTestsData.Users;
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userId = users[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(rolePermissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetUserPermissions())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<PermissionViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new PermissionsService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetUserPermissions(userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void CreatePermission_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = PermissionTestsData.Users;
            var userId = users[0].Id;
            var model = PermissionTestsData.CreateModel;
            MyController<PermissionsController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(result => result
                    .WithStatusCode(StatusCodes.Status201Created)
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if(property.Name == "permissionId")
                            {
                                var id = int.Parse(property.GetValue(obj).ToString());
                                Assert.True(From.Services<ApplicationDbContext>().Permissions.
                                    Any(p => p.Id == id));
                            }
                        }
                    }));
        }


        [Fact]
        public void CreatePermission_WithEmptyModel_ShouldReturnBadRequest()
        {
            var users = PermissionTestsData.Users;
            var userId = users[0].Id;
            var model = PermissionTestsData.CreateEmptyModel;
            MyController<PermissionsController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Description))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Description field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().Permissions
                .Any(p => p.Description == model.Description
                    && p.Type == model.Type));
        }

        [Fact]
        public void EditPermission_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var users = PermissionTestsData.Users;
            var userId = users[0].Id;
            var model = PermissionTestsData.UpdateModel;
            var permissionId = permissions[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(permissionId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Permissions
                          .Any(p => p.Id == permissionId
                              && p.Description == model.Description
                              && p.Type == model.Type));
        }

        [Fact]
        public void EditPermission_WithEmptyModel_ShouldReturnBadRequest()
        {
            var permissions = PermissionTestsData.Permissions();
            var users = PermissionTestsData.Users;
            var userId = users[0].Id;
            var model = PermissionTestsData.UpdateEmptyModel;
            var permissionId = permissions[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(permissionId, model))
                .ShouldReturn()
                .BadRequest(result => result
                .WithErrorOfType<List<string>>()
                .Passing(actualContacts =>
                {
                    if (string.IsNullOrEmpty(model.Description))
                    {
                        Assert.True(actualContacts.Where(ac => ac.Contains("The Description field is required")).Any());
                    }
                }));

            Assert.False(From.Services<ApplicationDbContext>().Permissions
                         .Any(p => p.Id == permissionId
                             && p.Description == model.Description
                             && p.Type == model.Type));
        }

        [Fact]
        public void DeletePermission_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var permissions = PermissionTestsData.Permissions();
            var users = PermissionTestsData.Users;
            var userId = users[0].Id;
            var permissionId = permissions[0].Id;
            MyController<PermissionsController>
                .Instance()
                .WithData(permissions)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(permissionId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Permissions
                          .Any(p => p.Id == permissionId));
        }

        private List<RolePermission> AddPermissionsToRole(List<Permission> permissions, List<Role> roles)
        {
            var allRolePermissions = new List<RolePermission>();
            foreach (var role in roles)
            {
                var rolePermissions = GetRolePremissions(role.Name, permissions);
                var roleId = role.Id;
                foreach (var rolePermission in rolePermissions)
                {
                    var newRolePermission = new RolePermission()
                    {
                        RoleId = roleId,
                        PermissionId = rolePermission.Id,
                    };

                    allRolePermissions.Add(newRolePermission);
                }
            }

            return allRolePermissions;
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
    }
}
