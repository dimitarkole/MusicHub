using MusicHub.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class PlaylistMusic : IAuditInfo
    {
        public PlaylistMusic()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public virtual Music Music { get; set; }

        public virtual string MusicId { get; set; }

        public virtual Playlist Playlist { get; set; }

        public virtual string PlaylistId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
