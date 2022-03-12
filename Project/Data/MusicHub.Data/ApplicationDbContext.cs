namespace MusicHub.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using MusicHub.Data.Common.Models;
    using MusicHub.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserSetting> UserSettings { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CommentReaction> CommentReactions { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Vaucher> Vauchers { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistSong> PlaylistSongs { get; set; }

        public DbSet<SongReaction> SongReactions { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<License> Licenses { get; set; }

        public DbSet<SongLicense> SongLicenses { get; set; }

        public DbSet<LicenseFile> LicenseFiles { get; set; }

        public DbSet<VerificationCode> VerificationCodes { get; set; }

        public DbSet<SongViewHistory> SongViewHistories { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            builder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);
            this.ConfigureApplicationEntitiesRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
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

        private void ConfigureApplicationEntitiesRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                 .HasMany(e => e.Songs)
                 .WithOne(e => e.Category)
                 .HasForeignKey(e => e.CategoryId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.CommentReactions)
                .WithOne(e => e.Comment)
                .HasForeignKey(e => e.CommentId);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Song)
                .HasForeignKey(e => e.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.PlaylistSongs)
                .WithOne(e => e.Song)
                .HasForeignKey(e => e.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.SongReactions)
                .WithOne(e => e.Song)
                .HasForeignKey(e => e.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.SongViewHistories)
                .WithOne(e => e.Song)
                .HasForeignKey(e => e.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Playlist>()
              .HasMany(e => e.PlaylistSongs)
              .WithOne(e => e.Playlist)
              .HasForeignKey(e => e.PlaylistId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
