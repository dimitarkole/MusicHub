namespace MusicHub.Web.ViewModels.Users
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserInfoViewModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Initials { get; set; }

        public string Email { get; set; }
    }
}
