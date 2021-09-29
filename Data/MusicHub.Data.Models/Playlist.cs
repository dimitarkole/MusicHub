namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class Playlist : IAuditInfo
    {
        public Playlist()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PlaylistMusics = new HashSet<PlaylistMusic>();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<PlaylistMusic> PlaylistMusics { get; set; }

        public Privacy Privacy { get; set; }
    }
}
