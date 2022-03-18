namespace MusicHub.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        public virtual DbSet<UserSetting> UserSettings { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Music> Musics { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<NotificationClaim> NotificationClaims { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<CommentReaction> CommentReactions { get; set; }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public virtual DbSet<PaySubscription> PaySubscriptions { get; set; }

        public virtual DbSet<SubscriptionClaim> SubscriptionClaims { get; set; }

        public virtual DbSet<Playlist> Playlists { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<AlbumMusic> AlbumMusics { get; set; }

        public virtual DbSet<VerificationCode> VerificationCodes { get; set; }

        public virtual DbSet<PlaylistMusic> PlaylistMusics { get; set; }

        public virtual DbSet<MusicReaction> MusicReactions { get; set; }

        public virtual DbSet<Follow> Follows { get; set; }

        public virtual DbSet<License> Licenses { get; set; }

        public virtual DbSet<MusicLicense> MusicLicenses { get; set; }

        public virtual DbSet<LicenseFile> LicenseFiles { get; set; }

        public virtual DbSet<PlayedMusic> PlayedMusics { get; set; }

        public virtual DbSet<PlayLaterMusic> PlayLaterMusics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInfoRules()
        {
            IEnumerable<EntityEntry> changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (EntityEntry entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
