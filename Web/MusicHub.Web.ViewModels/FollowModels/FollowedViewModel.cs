namespace MusicHub.Web.ViewModels.FollowModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class FollowedViewModel : IMapFrom<Follow>
    {
        public string Id { get; set; }

        public FollowUserViewModel Followed { get; set; }
    }
}
