namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class Album : IAuditInfo
    {
        public Album()
        {
            this.Id = Guid.NewGuid().ToString();
            this.AlbumMusics = new HashSet<AlbumMusic>();
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

        public virtual ICollection<AlbumMusic> AlbumMusics { get; set; }

        public Privacy Privacy { get; set; }
    }
}
