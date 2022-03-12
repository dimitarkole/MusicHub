namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Common.Models;
    using MusicHub.Filtering.Attributes;
    using MusicHub.Filtering.Operators;

    using static MusicHub.Common.BooleanSearchConstants;

    public class Song : IAuditInfo
    {
        public Song()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
            this.SongReactions = new HashSet<SongReaction>();
            this.SongViewHistories = new HashSet<SongViewHistory>();
            this.PlaylistSongs = new HashSet<PlaylistSong>();
            this.SongLicenses = new HashSet<SongLicense>();
        }

        // Summary:
        //     Gets or sets the primary key.
        public virtual string Id { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Text { get; set; }

      /*  [ForeignFilteringCollection(Labels.File, DatabaseOperators.Like, nameof(SongFile.Name), Tables.MailItemFiles, nameof(SongFile.ParentId))]
        public ICollection<SongFile> Files { get; set; }*/

        public string ImageFilePath { get; set; }

        public string AudioFilePath { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<SongReaction> SongReactions { get; set; }

        public virtual ICollection<SongViewHistory> SongViewHistories { get; set; }

        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }

        public virtual ICollection<SongLicense> SongLicenses { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
