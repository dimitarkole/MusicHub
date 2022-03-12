namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public virtual string Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Avatar { get; set; }

        public virtual string Name { get; set; }

        public virtual string Family { get; set; }

        public virtual string Email { get; set; }
    }
}
