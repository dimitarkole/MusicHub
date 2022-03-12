namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data.Common.Models;

    public class Follower
    {
        public Follower()
        {
           this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string FollowedId { get; set; }

        public ApplicationUser Followed { get; set; }

        public string FollowingId { get; set; }

        public ApplicationUser Following { get; set; }
    }
}
