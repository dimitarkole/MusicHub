namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.ProfilleModels;

    public interface IProfileService
    {
        IEnumerable<T> All<T>();

        T GetById<T>(string id);

        bool UserExistsByUsername(string username, string id = null);

        IEnumerable<string> GetFullNames(IEnumerable<string> userIds);

        Task Delete(string id);

        Task Renew(string id);

        IEnumerable<ProfileViewModel> Search(string searchQuery);

        IEnumerable<ProfileViewModel> Search(UserFilter model);

        //bool HasPermissions(string id, PermissionType permissionType);

        bool UserExistsByEmail(string email, string id = null);

        Task<ApplicationUser> Update(string id, ProfileEditModel model);

        Task ToggleSetting(int settingId);

        bool SettingEnabled(SettingType settingType, string userId);

        IEnumerable<T> GetUsersInfo<T>(IEnumerable<string> userIds);

        Task AddDefaultSettings(string userId);

        IEnumerable<T> GetSettings<T>(string userId);

        Task UploadProfileImage(string userId, UserProfilePictueInputModel model);

        //IEnumerable<PermissionType> GetPermissions(IEnumerable<string> roles);
    }
}
