namespace MusicHub.Data.Models
{
    using MusicHub.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Follow : IAuditInfo
    {
        public Follow()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string FollowingId { get; set; }

        public User Following { get; set; }

        public string FollowerId { get; set; }

        public User Follower { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
