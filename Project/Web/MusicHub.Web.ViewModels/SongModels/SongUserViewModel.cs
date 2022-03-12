namespace MusicHub.Web.ViewModels.SongModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string UserName { get; set; }
    }
}
