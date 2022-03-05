namespace MusicHub.Web.ViewModels.Users
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserEditInputModel : UserInputModel, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public virtual int MusicsCount { get; set; }

        public virtual int PlaylistsCount { get; set; }

        public virtual int AlbumsCount { get; set; }

        public string[] Roles { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserEditInputModel>()
                 .ForMember(m => m.Roles, y => y.MapFrom(u => u.Roles.Select(r => r.Role.Name).ToList()))
                 .ForMember(m => m.MusicsCount, y => y.MapFrom(u => u.Musics.Count() == 0 ? 0 : u.Musics.Count()))
                 .ForMember(m => m.PlaylistsCount, y => y.MapFrom(u => u.Playlists.Count() == 0 ? 0 : u.Playlists.Count()))
                 .ForMember(m => m.AlbumsCount, y => y.MapFrom(u => u.Albums.Count() == 0 ? 0 : u.Albums.Count()));

            configuration.CreateMap<UserEditInputModel, User>()
                .ForMember(u => u.Roles, y => y.Ignore())
                .ForMember(u => u.Id, y => y.Ignore());
        }
    }
}
