namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;

    public class PlaylistSong : IAuditInfo
    {
        public PlaylistSong()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        //
        // Summary:
        //     Gets or sets the primary key for this role.
        public virtual string Id { get; set; }

        public virtual Song Song { get; set; }

        public virtual string SongId { get; set; }

        public virtual Playlist Playlist { get; set; }

        public virtual string PlaylistId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
