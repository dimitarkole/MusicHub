using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.RoleModels;
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
    public class RoleControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetRoles_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var roles = RoleTestsData.GetRoles;
            var users = RoleTestsData.Users;
            var userId = PlaylistTestsData.CreateUserId;
            MyController<RoleController>
                .Instance()
                .WithData(roles)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<RolesAllViewModel<RoleViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.RolesPerPage;
                        var service = new RoleService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<RoleViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetAllRoles_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var roles = RoleTestsData.GetRoles;
            var users = RoleTestsData.Users;
            var userId = PlaylistTestsData.CreateUserId;
            MyController<RoleController>
                .Instance()
                .WithData(roles)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<RoleViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new RoleService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<RoleViewModel>();
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterRoles_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var roles = RoleTestsData.GetRoles;
            var users = RoleTestsData.Users;
            var userId = PlaylistTestsData.CreateUserId;
            var filter = RoleTestsData.FilterModel;
            MyController<RoleController>
                .Instance()
                .WithData(roles)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<RolesAllViewModel<RoleViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.RolesPerPage;
                        var service = new RoleService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<RoleViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetRolesById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var roles = RoleTestsData.GetRoles;
            var users = RoleTestsData.Users;
            var userId = PlaylistTestsData.CreateUserId;
            var roleId = RoleTestsData.GetById;
            MyController<RoleController>
                .Instance()
                .WithData(roles)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(roleId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<RoleDetailsModel>()
                    .Passing(actual =>
                    {
                        var service = new RoleService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById(roleId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void DeleteRole_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var roles = RoleTestsData.GetRoles;
            var users = RoleTestsData.Users;
            var userId = PlaylistTestsData.CreateUserId;
            var deleteRoleId = RoleTestsData.GetById;
            MyController<RoleController>
                .Instance()
                .WithData(roles)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteRoleId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Playlists
                .Any(m => m.Id == deleteRoleId));
        }

        [Fact]
        public void CreateRole_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var roles = RoleTestsData.GetRoles;
            var users = RoleTestsData.Users;
            var permissions = RoleTestsData.Permissions;
            var userId = PlaylistTestsData.CreateUserId;
            var model = RoleTestsData.CreateModel;
            MyController<RoleController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
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
                            if (property.Name == "roleId")
                            {
                                Assert.True(From.Services<ApplicationDbContext>().Roles
                                    .Any(p => p.Id == property.GetValue(obj).ToString()
                                        && p.Name == model.Name));
                                if(model.PermissionIds != null)
                                {
                                    var setPermissionIds = From.Services<ApplicationDbContext>().Roles
                                        .Where(p => p.Id == property.GetValue(obj).ToString())
                                        .FirstOrDefault()
                                        .Permissions
                                        .Select(pm => pm.PermissionId)
                                        .OrderBy(p => p);

                                    model.PermissionIds.OrderBy(p => p).Should().BeEquivalentTo(setPermissionIds);
                                }
                            }
                        }
                    }));
        }

        [Fact]
        public void UpdateRole_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var roles = RoleTestsData.GetRoles;
            var users = RoleTestsData.Users;
            var permissions = RoleTestsData.Permissions;
            var userId = PlaylistTestsData.CreateUserId;
            var model = RoleTestsData.UpdateModel;
            var roleId = RoleTestsData.UpdateRoleId;
            MyController<RoleController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Put(roleId, model))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Roles
                .Any(r => r.Id == roleId
                    && r.Name == model.Name));
            if (model.PermissionIds != null)
            {
                var setPermissionIds = From.Services<ApplicationDbContext>().Roles
                    .Where(p => p.Id == roleId)
                    .FirstOrDefault()
                    .Permissions
                    .Select(pm => pm.PermissionId)
                    .OrderBy(p => p);

                model.PermissionIds.OrderBy(p => p).Should().BeEquivalentTo(setPermissionIds);
            }
        }
    }
}
