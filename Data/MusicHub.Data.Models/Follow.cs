namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Follow
    {
        public Follow()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string FollowedId { get; set; }

        public User Followed { get; set; }

        public string FollowerId { get; set; }

        public User Follower { get; set; }
    }
}
