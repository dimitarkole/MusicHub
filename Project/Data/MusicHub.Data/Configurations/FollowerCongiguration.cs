using MusicHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Configurations
{
    public class FollowerCongiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> appFollower)
        {
            appFollower.HasKey(e => new { e.FollowedId, e.FollowingId });

            appFollower.HasOne(e => e.Followed)
                .WithMany(u => u.Followings)
                .HasForeignKey(e => e.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            appFollower.HasOne(e => e.Following)
                .WithMany(u => u.Followeds)
                .HasForeignKey(e => e.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
