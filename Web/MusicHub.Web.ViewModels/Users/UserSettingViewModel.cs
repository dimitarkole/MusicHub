using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Services.Mapping;

namespace MusicHub.Web.ViewModels.Users
{
    public class UserSettingViewModel : IMapFrom<UserSetting>
    {
        public int Id { get; set; }

        public SettingType Type { get; set; }

        public bool Enabled { get; set; }
    }
}
