using FluentAssertions;
using MusicHub.Common;
using MusicHub.Data;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.NotificationModels;
using MusicHub.Web.ViewModels.Users;
using MusicHub.Web.ViewModels.VerificationCodeModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class UsersControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUsers_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;

            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.All(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserPageModel<UserViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UsersPerPage;
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<UserViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ListUsers_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;

            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.List(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserPageModel<UserBriefInfoViewModel >>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UsersPerPage;
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<UserBriefInfoViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetUserById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            var getUserId = users[1].Id;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Get(getUserId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserEditInputModel>()
                    .Passing(actual =>
                    {
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<UserEditInputModel>(getUserId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void MyProfile_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.MyProfile())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserEditInputModel>()
                    .Passing(actual =>
                    {
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<UserEditInputModel>(userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void UploadProfileImage_WithDataInTheDb_ShouldReturnOk()
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            var model = UserTestsData.UserProfilePictueInput;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.UploadProfileImage(model))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void GetByEmail_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            var filter = UserTestsData.FilterModelWithEmail;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetByEmail(filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserViewModel>()
                    .Passing(actual =>
                    {
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetByEmail<UserViewModel>(filter.Email);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void ConfirmEmail_WithDataInTheDb_ShouldReturnОк()
        {
            var users = VerificationCodeTestsData.Users;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCode = verificationCodes
                    .Where(vc => vc.Type == VerificationType.ConfirmEmail
                        && vc.IsUsed == false)
                    .OrderByDescending(vc => vc.CreatedOn)
                    .FirstOrDefault();
            var userId = verificationCode.UserId;
            var verificationCodeCheckInputModel = new VerificationCodeCheckInputModel() { 
                Code = verificationCode.Code,
                Type = verificationCode.Type,
            };

            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .Calling(c => c.ConfirmEmail(userId, verificationCodeCheckInputModel))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().VerificationCodes
                 .Any(vc => vc.IsUsed == true && vc.Id == verificationCode.Id));

            Assert.True(From.Services<ApplicationDbContext>().Users
                .Any(u => u.Id == userId && u.EmailConfirmed == true));
        }

        [Fact]
        public void ConfirmEmail_WithDataInTheDb_CodeIsUsed_ShouldReturnBadRequest()
        {
            var users = VerificationCodeTestsData.Users;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCode = verificationCodes
                    .Where(vc => vc.Type == VerificationType.ConfirmEmail
                        && vc.IsUsed == true)
                    .OrderByDescending(vc => vc.CreatedOn)
                    .FirstOrDefault();
            var userId = verificationCode.UserId;
            var verificationCodeCheckInputModel = new VerificationCodeCheckInputModel()
            {
                Code = verificationCodes[0].Code,
                Type = verificationCodes[0].Type,
            };

            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .Calling(c => c.ConfirmEmail(userId, verificationCodeCheckInputModel))
                .ShouldReturn()
                .BadRequest();

            Assert.False(From.Services<ApplicationDbContext>().Users
               .Any(u => u.Id== userId && u.EmailConfirmed == true));
        }

        [Fact]
        public void ConfirmEmail_WithDataInTheDb_UserIdIsNotFound_ShouldReturnBadRequest()
        {
            var users = VerificationCodeTestsData.Users;
            var verificationCodes = VerificationCodeTestsData.VerificationCodes();
            var verificationCode = verificationCodes
                    .Where(vc => vc.Type == VerificationType.ConfirmEmail
                        && vc.IsUsed == true)
                    .OrderByDescending(vc => vc.CreatedOn)
                    .FirstOrDefault();
            var userId = "";
            var verificationCodeCheckInputModel = new VerificationCodeCheckInputModel()
            {
                Code = verificationCodes[0].Code,
                Type = verificationCodes[0].Type,
            };

            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(verificationCodes)
                .Calling(c => c.ConfirmEmail(userId, verificationCodeCheckInputModel))
                .ShouldReturn()
                .BadRequest();

            Assert.False(From.Services<ApplicationDbContext>().Users
               .Any(u => u.Id == userId && u.EmailConfirmed == true));
        }

        [Fact]
        public void HasPermission_WithDataInTheDb_ShouldReturnОкWithTrue()
        {
            var users = PermissionTestsData.Users;
            var roles = PermissionTestsData.Roles;
            var permissions = PermissionTestsData.Permissions();
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userRoles = PermissionTestsData.UsersRoles;

            var userId = userRoles[0].UserId;
            var testRoleId = userRoles
                .Where(ur => ur.UserId == userId)
                .FirstOrDefault()
                .RoleId;
            var testPermissionId = rolePermissions
                .Where(rp => rp.RoleId == testRoleId)
                .FirstOrDefault()
                .PermissionId;
            var testPermissionType = permissions
                .Where(p => p.Id == testPermissionId)
                .FirstOrDefault()
                .Type;

            MyController<UsersController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(rolePermissions)
                .WithData(users)
                .WithData(userRoles)
                 .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                 .Calling(c => c.HasPermission(testPermissionType))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void HasPermission_WithDataInTheDb_ShouldReturnОкWithFalse()
        {
            var users = PermissionTestsData.Users;
            var roles = PermissionTestsData.Roles;
            var permissions = PermissionTestsData.Permissions();
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userRoles = PermissionTestsData.UsersRoles;

            var permissionsCount = permissions.Count();
            var permissionInRole = rolePermissions
                .GroupBy(rp => rp.RoleId)
                .OrderBy(rp => rp.Count())
                .FirstOrDefault()
                .Select(rp => rp);
            var roleId = permissionInRole
                .FirstOrDefault()
                .RoleId;
            var testPermissionType = permissions
                .Where(p => !permissionInRole.Any(rp => rp.PermissionId == p.Id))
                .FirstOrDefault()
                .Type;
            var userId = userRoles
                .Where(ur => ur.RoleId == roleId)
                .FirstOrDefault()
                .UserId;

            MyController<UsersController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(rolePermissions)
                .WithData(users)
                .WithData(userRoles)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.HasPermission(testPermissionType))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

      /*  [Fact]
        public void Edit_WithDataInTheDb_UserId_ShouldReturnOk()
        {
            var users = PermissionTestsData.Users;
            var roles = PermissionTestsData.Roles;
            var permissions = PermissionTestsData.Permissions();
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userId = users[0].Id;
            var model = UserTestsData.UpdateModel;

            MyController<UsersController>
                .Instance()
                .WithData(users)
                .Calling(c => c.Edit(model, userId))
                .ShouldReturn()
                .BadRequest();

            Assert.True(From.Services<ApplicationDbContext>().Users
               .Any(u => u.Id == userId 
                    && u.FirstName == model.FirstName
                    && u.Email == model.Email
                    && u.LastName == model.LastName
                    && u.Birthday == model.Birthday
                    && u.UserName == model.UserName));
        }

        [Fact]
        public void EditWithRoles_WithDataInTheDb_UserId_ShouldReturnOk()
        {
            var users = UserTestsData.Users;
            var userId = users[0].Id;
            var model = UserTestsData.UpdateModel;
            var roles = PermissionTestsData.Roles;
            var userRoles = PermissionTestsData.UsersRoles;
            var permissions = PermissionTestsData.Permissions();
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);

            var result = new List<UserRole>();
            var random = new Random();
            var roleId = roles[random.Next(1, roles.Count - 1)].Id;
            model.Roles = new string[]
            {
                roleId,
            };

            MyController<UsersController>
                .Instance()
                .WithData(permissions)
                .WithData(roles)
                .WithData(rolePermissions)
                .WithData(users)
                .WithData(userRoles)
                .Calling(c => c.Edit(model, userId))
                .ShouldReturn()
                .BadRequest();

            Assert.True(From.Services<ApplicationDbContext>().Users
               .Any(u => u.Id == userId
                    && u.FirstName == model.FirstName
                    && u.Email == model.Email
                    && u.LastName == model.LastName
                    && u.Birthday == model.Birthday
                    && u.UserName == model.UserName));

            Assert.Equal(From.Services<ApplicationDbContext>().UserRoles
                .Where(u => u.UserId == userId)
                .Count(), model.Roles.Count());

        }      */

        [Fact]
        public void DeleteUser_WithDataInTheDb_UserIdIsNotFound_ShouldReturnOk()
        {
            var users = VerificationCodeTestsData.Users;
            var userId = users[0].Id;
            var deleteUserId = users[1].Id;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.DeleteUser(deleteUserId))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Users
               .Any(u => u.Id == deleteUserId && u.IsDeleted == true));
        }

        [Fact]
        public void RenewUser_WithDataInTheDb_UserIdIsNotFound_ShouldReturnOk()
        {
            var users = VerificationCodeTestsData.Users;
            var userId = users[0].Id;
            users[1].IsDeleted = true;
            var deletedUserId = users[1].Id;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.RenewUser(deletedUserId))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Users
               .Any(u => u.Id == deletedUserId && u.IsDeleted == false));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FindUsers_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            var findModel = UserTestsData.FindModel;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.FindUsers(page, findModel))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserPageModel<UserViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UsersPerPage;
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Search(page, entitesPerPage, findModel.SearchQuery);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void AdvancedSearch_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            var filter = UserTestsData.FilterModel;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.AdvancedSearch(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserPageModel<UserViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UsersPerPage;
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Search(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void AdvancedSearchWithEmail_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            var filter = UserTestsData.FilterModelWithEmail;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.AdvancedSearch(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserPageModel<UserViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UsersPerPage;
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Search(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetFullNames_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetFullNames(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserPageModel<UserFullNameViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UsersPerPage;
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<UserFullNameViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetUserName_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = UserTestsData.Users;
            var userId = UserTestsData.UserId;
            var searchUserId = users[1].Id;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetUserName(searchUserId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<UserFullNameViewModel>()
                    .Passing(actual =>
                    {
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<UserFullNameViewModel>(searchUserId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

       /* [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetNotifications_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = NotificationTestsData.Users;
            var notifications = NotificationTestsData.GetNotificationData();
            var userId = NotificationTestsData.UserId;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(notifications)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Notifications(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<NotificationAllViewModel<NotificationViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.UsersPerPage;
                        var service = new NotificationsService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetUserNotifications<NotificationViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }           */

       /* [Fact]
        public void MarkNotificationAsSeen_WithDataInTheDb_ShouldReturnOk()
        {
            var users = NotificationTestsData.Users;
            var notifications = NotificationTestsData.GetNotificationData();
            var userId = notifications[0].UserId;
            var notificationId = notifications
                .Where(n => n.UserId == userId && n.Seen == false)
                .FirstOrDefault()
                .Id;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(notifications)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.MarkNotificationAsSeen(notificationId))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().Notifications
             .Any(n => n.Id == notificationId && n.Seen == true));
        }

        [Fact]
        public void MarkNotificationAsSeen_UserIsNotOwner_WithDataInTheDb_ShouldReturnOk()
        {
            var users = NotificationTestsData.Users;
            var notifications = NotificationTestsData.GetNotificationData();
            var userId = notifications[0].UserId;
            var notificationId = notifications
                .Where(n => !(n.UserId == userId) && n.Seen == false)
                .FirstOrDefault()
                .Id;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(notifications)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.MarkNotificationAsSeen(notificationId))
                .ShouldReturn()
                .Forbid();

            Assert.False(From.Services<ApplicationDbContext>().Notifications
             .Any(n => n.Id == notificationId && n.Seen == true));
        } 

        [Fact]
        public void Settings_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = UserTestsData.Users;
            var settings = UserTestsData.UserSettings;
            var userId = NotificationTestsData.UserId;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(settings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Settings())
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<List<UserSettingViewModel>>()
                    .Passing(actual =>
                    {
                        var service = new ProfileService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetSettings<UserSettingViewModel>(userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void ToggleSetting_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = UserTestsData.Users;
            var settings = UserTestsData.UserSettings;
            var userId = NotificationTestsData.UserId;
            var settingId = settings[0].Id;
            var oldEnableStatus = settings[0].Enabled;
            MyController<UsersController>
                .Instance()
                .WithData(users)
                .WithData(settings)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.ToggleSetting(settingId))
                .ShouldReturn()
                .Ok();

            Assert.True(From.Services<ApplicationDbContext>().UserSettings
                .Any(us => us.Id == settingId && !(us.Enabled == oldEnableStatus)));
        }

    /*    [Fact]
        public void ChangePermissions_WithDataInTheDb_UserId_ShouldReturnOk()
        {
            var users = PermissionTestsData.Users;
            var roles = PermissionTestsData.Roles;
            var permissions = PermissionTestsData.Permissions();
            var rolePermissions = this.AddPermissionsToRole(permissions, roles);
            var userId = users[0].Id;
            var model = UserTestsData.ChangePermissionsInputModel;
            model.Roles = new string[]
            {
                roles[0].Id,
                roles[1].Id,
            };

            MyController<UsersController>
                .Instance()
                .WithData(roles)
                .WithData(permissions)
                .WithData(rolePermissions)
                .WithData(users)
                .Calling(c => c.ChangePermissions(model))
                .ShouldReturn()
                .ActionResult();
        }  */

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
