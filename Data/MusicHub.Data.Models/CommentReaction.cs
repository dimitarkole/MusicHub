namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;

    public class CommentReaction : IAuditInfo
    {
        public CommentReaction()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid().ToString();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual string CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public Reaction Reaction { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
