namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Common.Models;

    public class Comment : IAuditInfo
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.CommentsChildren = new HashSet<Comment>();
            this.CommentReactions = new HashSet<CommentReaction>();
        }

        // Summary:
        //     Gets or sets the primary key for this role.
        public virtual string Id { get; set; }

        public virtual string SongId { get; set; }

        public virtual Song Song { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ParentCommentId { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> CommentsChildren { get; set; }

        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
    }
}
