// ReSharper disable VirtualMemberCallInConstructor
namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MusicHub.Common;
    using MusicHub.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IAuditInfo
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new List<UserRole>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Musics = new HashSet<Music>();
            this.Orders = new HashSet<Order>();
            this.Playlists = new HashSet<Playlist>();
            this.Comments = new HashSet<Comment>();
            this.MusicReactions = new HashSet<MusicReaction>();
            this.PlayedMusics = new HashSet<PlayedMusic>();
            this.Followeds = new HashSet<Follow>();
            this.Followers = new HashSet<Follow>();
            this.VerificationCodes = new HashSet<VerificationCode>();
            this.Licenses = new HashSet<License>();
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

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<License> Licenses { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Music> Musics { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<MusicReaction> MusicReactions { get; set; }

        public virtual ICollection<PlayedMusic> PlayedMusics { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public ICollection<Follow> Followers { get; set; }

        public ICollection<Follow> Followeds { get; set; }

        public ICollection<VerificationCode> VerificationCodes { get; set; }
        public bool IsAd { get; set; }
    }
}
