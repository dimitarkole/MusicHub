
using MusicHub.Data.Models;
using MusicHub.Services.Mapping;

namespace MusicHub.Web.ViewModels.Users
{
    public class UserBriefInfoViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
