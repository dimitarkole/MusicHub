namespace MusicHub.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserDetailsViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string[] Roles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserDetailsViewModel>()
                 .ForMember(m => m.Roles, y => y.MapFrom(u => u.Roles.Select(r => r.Role.Name).ToList()));
        }
    }
}
