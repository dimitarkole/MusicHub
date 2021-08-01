namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Data.Common;
    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Filtering.Attributes;
    using static MusicHub.Common.BooleanSearchConstants;

    [FilteringTable(Tables.Musics)]
    public class Music : Entity<string>, IAuditInfo
    {
        public Music()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
            this.MusicReactions = new HashSet<MusicReaction>();
            this.PlayedMusics = new HashSet<PlayedMusic>();
            this.PlaylistMusics = new HashSet<PlaylistMusic>();
            this.MusicLicenses = new HashSet<MusicLicense>();
        }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string AudioUrl { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public ICollection<MusicReaction> MusicReactions { get; set; }

        public ICollection<PlaylistMusic> PlaylistMusics { get; set; }

        public ICollection<PlayedMusic> PlayedMusics { get; set; }

        public ICollection<MusicLicense> MusicLicenses { get; set; }
    }
}
