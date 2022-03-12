namespace MusicHub.Web.ViewModels.FollowModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class FollowUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string UserName { get; set; }
    }
}
