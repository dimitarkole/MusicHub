namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Tests.TestData;
    using MusicHub.Web.ViewModels.Users;
    using Xunit;
    using static MusicHub.Common.GlobalConstants;
    using MusicHub.Services.Mapping;
    using MusicHub.Common.Extensions;
    using MusicHub.Data.Models.Enums;

    public class ProfileServiceTests : BaseTestClass
    {
        [Fact]
        public async Task UserExistsByUsername__WithValidData_ShouldReturnTrue()
        {
            var profileService = await this.CreateProfileService();
            var username = UserTestsData.UserName;

            Assert.True(profileService.UserExistsByUsername(username));
        }

        [Fact]
        public async Task UserExistsByUsername__WithValidData_ShouldReturnFalse()
        {
            var profileService = await this.CreateProfileService();
            var username = "";

            Assert.False(profileService.UserExistsByUsername(username));
        }

        [Fact]
        public async Task UserExistsByEmail__WithValidData_ShouldReturnTrue()
        {
            var profileService = await this.CreateProfileService();
            var email = UserTestsData.UserEmail;

            Assert.True(profileService.UserExistsByEmail(email));
        }

        [Fact]
        public async Task UserExistsByEmail__WithValidData_ShouldReturnFalse()
        {
            var profileService = await this.CreateProfileService();
            var email = "";

            Assert.False(profileService.UserExistsByEmail(email));
        }

        [Fact]
        public async Task GetFullNames__WithValidData_ShouldReturnFalse()
        {
            var profileService = await this.CreateProfileService();
            var userIds = UserTestsData.UsersIds;

            var expectedResults = this.context.Users
                .Where(u => !u.IsDeleted)
                .Where(u => userIds.Contains(u.Id))
                .Select(u => $"{u.FirstName} {u.LastName}")
                .ToList();
            var result = profileService.GetFullNames(userIds).ToList();
            Assert.Equal(expectedResults.Count, result.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.Equal(expectedResults[i], result[i]);
            }
        }

        [Fact]
        public async Task DeleteUser_WithValidData_ShouldWorkCorrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;
            await profileService.Delete(userId);

            Assert.True(this.context.Users.Any(u => u.Id == userId && u.IsDeleted == true));
        }

        [Fact]
        public async Task RenewUser_WithValidData_ShouldWorkCorrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;

            User user = this.context.Users.Find(userId);
            user.IsDeleted = true;
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();

            await profileService.Renew(userId);

            Assert.True(this.context.Users.Any(u => u.Id == userId
                && u.IsDeleted == false));
        }

        [Fact]
        public async Task UploadProfileImage_WithValidData_ShouldWorkCorrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;

            User user = this.context.Users.Find(userId);
            user.IsDeleted = true;
            var model = UserTestsData.UserProfilePictueInput;

            await profileService.UploadProfileImage(userId, model);

            Assert.True(this.context.Users.Any(u => u.Id == userId
                && u.ImageUrl == model.ImageUrl));
        }

        [Fact]
        public async Task UploadProfile_WithValidData_ShouldWorkCorrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;

            User user = this.context.Users.Find(userId);
            user.IsDeleted = true;
            var model = UserTestsData.UpdateModel;

            await profileService.Update(userId, model);

            Assert.True(this.context.Users.Any(u => u.Id == userId
                && u.UserName == model.UserName
                && u.FirstName == model.FirstName
                && u.LastName == model.LastName
                && u.Email == model.Email
                && u.Birthday == model.Birthday));
        }

        [Fact]
        public async Task UploadProfileWithRoles_WithValidData_ShouldWorkCorrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;

            User user = this.context.Users.Find(userId);
            user.IsDeleted = true;
            var model = UserTestsData.UpdateModelWithRoles;
            var lastRoles = this.context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToList();

            await profileService.Update(userId, model);
            Assert.True(this.context.Users.Any(u => u.Id == userId
                && u.UserName == model.UserName
                && u.FirstName == model.FirstName
                && u.LastName == model.LastName
                && u.Email == model.Email
                && u.Birthday == model.Birthday));
            var updatedRoles = this.context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(r => r.Role.Name)
                .ToArray();
            var expectedResult = model.Roles.ToList();
            expectedResult.AddRange(lastRoles);

            this.IsRolesEquals(expectedResult.ToArray(), updatedRoles);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllUsers_WithValidData_ShouldWorkCurrect(int page)
        {
            var profileService = await this.CreateProfileService();
            var expextedResult = this.context.Users.Where(u => !u.IsDeleted)
                    .OrderBy(u => u.UserName)
                    .To<UserViewModel>();
            var entitesPerPage = PaginationData.UsersPerPage;
            var result = profileService.All<UserViewModel>(page, entitesPerPage);
            var countPages = this.GetUsersCount(expextedResult.Count(), entitesPerPage);

            var expectedUsersList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            this.CheckUserAllViewModelsIsEqual(page, expectedUsersList, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task SearchByQuery_WithValidData_ShouldWorkCurrect(int page)
        {
            var profileService = await this.CreateProfileService();
            var entitesPerPage = PaginationData.UsersPerPage;
            var searchQuery = UserTestsData.SearchQuery;
            var expextedResult = this.context.Users
                   .Where(u => !u.IsDeleted)
                   .Where(u => u.FirstName.Contains(searchQuery) ||
                           u.LastName.Contains(searchQuery))

                   .OrderBy(u => u.UserName)
                   .To<UserViewModel>();

            var result = profileService.Search(page, entitesPerPage, searchQuery);
            var countPages = this.GetUsersCount(expextedResult.Count(), entitesPerPage);

            var expectedUsersList = expextedResult.GetPage(page, entitesPerPage)
               .ToList();

            this.CheckUserAllViewModelsIsEqual(page, expectedUsersList, result, countPages);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task SearchByFilterModel_WithValidData_ShouldWorkCurrect(int page)
        {
            var profileService = await this.CreateProfileService();
            var entitesPerPage = PaginationData.UsersPerPage;
            var model = UserTestsData.FilterModel;
            var expextedResult = this.context.Users
                   .Where(u => !u.IsDeleted);

            if (!string.IsNullOrEmpty(model.LastName))
            {
                expextedResult = expextedResult.Where(u => u.LastName.Contains(model.LastName));
            }
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                expextedResult = expextedResult.Where(u => u.FirstName.Contains(model.FirstName));
            }

            var result = profileService.Search(page, entitesPerPage, model);
            var countPages = this.GetUsersCount(expextedResult.Count(), entitesPerPage);

            var expectedUsersList = expextedResult.GetPage(page, entitesPerPage)
                .OrderBy(u => u.UserName)
                .To<UserViewModel>()
                .ToList();

            this.CheckUserAllViewModelsIsEqual(page, expectedUsersList, result, countPages);
        }

        [Fact]
        public async Task HasPermissionsTrue_WithValidData_ShouldWorkCorrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.HasPermissionTrueUserId;
            var permition = UserTestsData.HasPermissionTruePermitionType;

            Assert.True(profileService.HasPermissions(userId, permition));
        }

        [Fact]
        public async Task HasPermissionsFalse_WithValidData_ShouldWorkCorrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.HasPermissionFalseUserId;
            var permition = UserTestsData.HasPermissionFalsePermitionType;

            Assert.False(profileService.HasPermissions(userId, permition));
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldWorkCurrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;
            var expectedResult = this.context.Users.Where(u => !u.IsDeleted
                    && u.Id == userId)
                .To<UserDetailsViewModel>()
                .FirstOrDefault();
            var result = profileService.GetById<UserDetailsViewModel>(userId);

            Assert.True(CheckUserDetailsViewModelsIsEqual(expectedResult, result));
            this.IsRolesEquals(expectedResult.Roles, result.Roles);
        }

        [Fact]
        public async Task AddDefaultSettings_WithValidData_ShouldWorkCurrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;

            await profileService.AddDefaultSettings(userId);

            foreach (SettingType settingType in Enum.GetValues(typeof(SettingType)))
            {
                Assert.True(this.context.UserSettings.Any(us => us.UserId == userId
                    && us.Type == settingType
                    && us.Enabled == true));
            }
        }

        [Fact]
        public async Task GetSettings_WithValidData_ShouldWorkCurrect()
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;

            var expectedResults = this.context.UserSettings
                .Where(u => u.UserId == userId)
                .To<UserSettingViewModel>()
                .ToList();

            var results = profileService.GetSettings<UserSettingViewModel>(userId).ToList();

            Assert.Equal(expectedResults.Count, results.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedResult = expectedResults[i];
                var result = results[i];
                Assert.Equal(expectedResult.Enabled, result.Enabled);
                Assert.Equal(expectedResult.Type, result.Type);
                Assert.Equal(expectedResult.Id, result.Id);
            }
        }

        [Theory]
        [InlineData(SettingType.ReceiveEventChangesEmail)]
        [InlineData(SettingType.ReceiveEventChangesNotification)]
        public async Task SettingEnabled_WithValidData_ShouldWorkCurrect(SettingType type)
        {
            var profileService = await this.CreateProfileService();
            var userId = UserTestsData.UserId;

            var expected = this.context.UserSettings
                .Where(us => us.UserId == userId
                    && us.Type == type)
                .FirstOrDefault()
                .Enabled;

            var result = profileService.SettingEnabled(type, userId);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ToggleSetting_WithValidData_ShouldWorkCurrect()
        {
            var profileService = await this.CreateProfileService();
            var toggleSettingId = UserTestsData.ToggleSettingId;

            var expected = !this.context.UserSettings
                .Where(us => us.Id == toggleSettingId)
                .FirstOrDefault()
                .Enabled;

            profileService.ToggleSetting(toggleSettingId);
            var result = this.context.UserSettings
                .Where(us => us.Id == toggleSettingId)
                .FirstOrDefault()
                .Enabled;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetUsersInfo__WithValidData_ShouldReturnFalse()
        {
            var profileService = await this.CreateProfileService();
            var userIds = UserTestsData.UsersIds;

            var expectedResults = this.context.Users
                .Where(u => !u.IsDeleted)
                .Where(u => userIds.Contains(u.Id))
                .To<UserDetailsViewModel>()
                .ToList();
            var results = profileService.GetUsersInfo<UserDetailsViewModel>(userIds).ToList();
            Assert.Equal(expectedResults.Count, results.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.True(this.CheckUserDetailsViewModelsIsEqual(expectedResults[i], results[i]));
            }
        }

        private bool CheckUserDetailsViewModelsIsEqual(UserDetailsViewModel expectedUser, UserDetailsViewModel resultUser)
          => expectedUser.FirstName == resultUser.FirstName
               && expectedUser.Email == resultUser.Email
               && expectedUser.Id == resultUser.Id
               && expectedUser.ImageUrl == resultUser.ImageUrl
               && expectedUser.LastName == resultUser.LastName
               && expectedUser.Username == resultUser.Username;

        private void CheckUserAllViewModelsIsEqual(int page, List<UserViewModel> expectedUsersList, UserAllViewModel<UserViewModel> result, int countPages)
        {
            Assert.Equal(result.CurrentPage, page);
            Assert.Equal(result.NumberOfPages, countPages);
            var resultUsers = result.Users.ToList();
            Assert.Equal(resultUsers.Count, expectedUsersList.Count);

            for (int i = 0; i < expectedUsersList.Count(); i++)
            {
                var expectedUser = expectedUsersList[i];
                var resultUser = resultUsers[i];
                Assert.True(this.CheckUserViewModelsIsEqual(expectedUser, resultUser));
            }
        }

        private bool CheckUserViewModelsIsEqual(UserViewModel expectedUser, UserViewModel resultUser)
            => expectedUser.FirstName == resultUser.FirstName
               && expectedUser.Email == resultUser.Email
               && expectedUser.Id == resultUser.Id
               && expectedUser.ImageUrl == resultUser.ImageUrl
               && expectedUser.LastName == resultUser.LastName
               && expectedUser.Username == resultUser.Username;

        private void IsRolesEquals(string[] expectedRoles, string[] result)
        {
            Assert.Equal(expectedRoles.Count(), result.Count());
            for (int i = 0; i < expectedRoles.Length; i++)
            {
                Assert.Equal(expectedRoles[i], result[i]);
            }
        }

        private int GetUsersCount(int entityCount, int entitesPerPage)
        {
            var paginationService = new PaginationService();
            return paginationService.CalculatePagesCount(entityCount, entitesPerPage);
        }

        private async Task<ProfileService> CreateProfileService()
        {
            var users = UserTestsData.Users;
            await this.context.Permissions.AddRangeAsync(UserTestsData.Permissions());
            await this.context.Users.AddRangeAsync(users);
            await this.context.Roles.AddRangeAsync(UserTestsData.GetRoles());
            await this.context.UserRoles.AddRangeAsync(UserTestsData.UserRoles);
            await this.context.UserSettings.AddRangeAsync(UserTestsData.UserSettings);
            await this.context.MusicLicenses.AddRangeAsync(new List<MusicLicense>());
            await this.context.SaveChangesAsync();
            var service = new ProfileService(this.context, new PaginationService());

            return service;
        }
    }
}
