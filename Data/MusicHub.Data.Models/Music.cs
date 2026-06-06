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
            this.AlbumMusics = new HashSet<AlbumMusic>();
            this.PlayLaterMusics = new HashSet<PlayLaterMusic>();
            this.ReportMusics = new HashSet<ReportMusic>();
        }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string AudioUrl { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public Privacy Privacy { get; set; }

        public string BuyLink { get; set; }

        public string AlbumTitle { get; set; }

        public string RecordLabel { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Barcode { get; set; }

        public string ISWC { get; set; }

        public string PLine { get; set; }

        public string ContainsExplicitContent { get; set; }

        public bool EnableDirectDownloads { get; set; }

        public bool DisplayEmbedCode { get; set; }

        public bool OfflineListening { get; set; }

        public bool AppPlayback { get; set; }

        public bool RSSfeed { get; set; }

        public string ContainsMusic { get; set; }

        public string Artist { get; set; }

        public string Publisher { get; set; }

        public string ISRC { get; set; }

        public string Composer { get; set; }

        public string ReleaseTitle { get; set; }

        public string License { get; set; }

        public bool Attribution { get; set; }

        public bool Noncommercial { get; set; }

        public bool NoDerivativeWorks { get; set; }

        public bool ShareALike { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public ICollection<MusicReaction> MusicReactions { get; set; }

        public ICollection<PlaylistMusic> PlaylistMusics { get; set; }

        public ICollection<PlayLaterMusic> PlayLaterMusics { get; set; }

        public ICollection<PlayedMusic> PlayedMusics { get; set; }

        public ICollection<AlbumMusic> AlbumMusics { get; set; }

        public ICollection<MusicLicense> MusicLicenses { get; set; }

        public ICollection<ReportMusic> ReportMusics { get; set; }
    }
}
