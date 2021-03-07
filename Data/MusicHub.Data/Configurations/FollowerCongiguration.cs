namespace MusicHub.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MusicHub.Data.Models;

    public class FollowerCongiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> appFollow)
        {
            appFollow.HasKey(e => new { e.FollowedId, e.FollowerId });

            appFollow.HasOne(e => e.Followed)
                .WithMany(u => u.Followers)
                .HasForeignKey(e => e.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            appFollow.HasOne(e => e.Follower)
                .WithMany(u => u.Followeds)
                .HasForeignKey(e => e.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
