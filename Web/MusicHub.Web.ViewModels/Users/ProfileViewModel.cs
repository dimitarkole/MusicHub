using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.Users
{
    public class ProfileViewModel : IMapFrom<User>
    {
        public virtual string Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string Name { get; set; }

        public virtual string Family { get; set; }

        public virtual string Email { get; set; }
    }
}
