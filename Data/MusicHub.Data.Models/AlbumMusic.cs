namespace MusicHub.Data.Models
{
    using System;

    public class AlbumMusic
    {
        public AlbumMusic()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public virtual Music Music { get; set; }

        public virtual string MusicId { get; set; }

        public virtual Album Album { get; set; }

        public virtual string AlbumId { get; set; }

        public virtual int Position { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
