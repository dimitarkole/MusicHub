using AutoMapper;
using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicHub.Web.ViewModels.Users
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }
    }
}
