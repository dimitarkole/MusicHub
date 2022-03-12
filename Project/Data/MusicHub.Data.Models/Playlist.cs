namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Common.Models;

    public class Playlist : IAuditInfo
    {
        public Playlist()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PlaylistSongs = new HashSet<PlaylistSong>();
        }

        // Summary:
        //     Gets or sets the primary key for this role.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public virtual Category Category { get; set; }

        public virtual string CategoryId { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }

        public VisibleStatus VisibleStatus { get; set; }
    }
}
