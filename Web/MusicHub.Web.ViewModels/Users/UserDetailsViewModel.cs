namespace MusicHub.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserDetailsViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string[] Roles { get; set; }

        public int MusicsCount { get; set; }

        public virtual int PlaylistsCount { get; set; }

        public virtual int AlbumsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserDetailsViewModel>()
                 .ForMember(m => m.Roles, y => y.MapFrom(u => u.Roles.Select(r => r.Role.Name).ToArray()))
                 .ForMember(m => m.MusicsCount, y => y.MapFrom(u => u.Musics.Count() == 0 ? 0 : u.Musics.Count()))
                 .ForMember(m => m.PlaylistsCount, y => y.MapFrom(u => u.Playlists.Count() == 0 ? 0 : u.Playlists.Count()))
                 .ForMember(m => m.AlbumsCount, y => y.MapFrom(u => u.Albums.Count() == 0 ? 0 : u.Albums.Count()));
        }
    }
}
