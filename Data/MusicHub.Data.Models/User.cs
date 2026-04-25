// ReSharper disable VirtualMemberCallInConstructor
namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using MusicHub.Common;
    using MusicHub.Data.Common.Models;

    public class User : IdentityUser, IAuditInfo
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new List<UserRole>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Musics = new HashSet<Music>();
            this.Playlists = new HashSet<Playlist>();
            this.Comments = new HashSet<Comment>();
            this.MusicReactions = new HashSet<MusicReaction>();
            this.PlayedMusics = new HashSet<PlayedMusic>();
            this.Followings = new HashSet<Follow>();
            this.Followers = new HashSet<Follow>();
            this.Bellers = new HashSet<Bell>();
            this.Bellings = new HashSet<Bell>();
            this.Licenses = new HashSet<License>();
            this.VerificationCodes = new HashSet<VerificationCode>();
            this.PaySubscriptions = new HashSet<PaySubscription>();
            this.Albums = new HashSet<Album>();
            this.PlayLaterMusics = new HashSet<PlayLaterMusic>();
            this.NotificationSettings = new HashSet<NotificationSetting>();
            this.ReportMusics = new HashSet<ReportMusic>();
        }

        public virtual string ImageUrl { get; set; }

        public virtual string FirstName { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public virtual string LastName { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<License> Licenses { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Music> Musics { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<MusicReaction> MusicReactions { get; set; }

        public virtual ICollection<PlayedMusic> PlayedMusics { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public ICollection<Follow> Followers { get; set; }

        public ICollection<Follow> Followings { get; set; }

        public ICollection<Bell> Bellers { get; set; }

        public ICollection<Bell> Bellings { get; set; }

        public ICollection<VerificationCode> VerificationCodes { get; set; }

        public ICollection<PaySubscription> PaySubscriptions { get; set; }

        public ICollection<PlayLaterMusic> PlayLaterMusics { get; set; }

        public ICollection<NotificationSetting> NotificationSettings { get; set; }

        public ICollection<ReportMusic> ReportMusics { get; set; }
    }
}
