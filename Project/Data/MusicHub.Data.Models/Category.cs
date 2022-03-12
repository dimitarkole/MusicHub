namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;

    public class Category : IAuditInfo
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Songs = new HashSet<Song>();
        }

        // Summary:
        //     Gets or sets the primary key for this role.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
