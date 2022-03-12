namespace MusicHub.Web.ViewModels.FollowModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class FollowCreateInputModel : IMapTo<Follower>
    {
        [Required]
        public virtual string FollowedId { get; set; }
    }
}
