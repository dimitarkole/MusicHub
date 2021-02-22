using MusicHub.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Category : IAuditInfo
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Musics = new HashSet<Music>();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Music> Musics { get; set; }
    }
}
