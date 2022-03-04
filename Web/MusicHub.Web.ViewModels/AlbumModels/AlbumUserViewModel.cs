namespace MusicHub.Web.ViewModels.AlbumModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class AlbumUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string UserName { get; set; }
    }
}