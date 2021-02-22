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

    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> appMusic)
        {
            appMusic.HasMany(e => e.Comments)
                .WithOne(e => e.Music)
                .HasForeignKey(e => e.MusicId)
                .OnDelete(DeleteBehavior.Cascade);

            appMusic
                .HasMany(e => e.PlaylistMusics)
                .WithOne(e => e.Music)
                .HasForeignKey(e => e.MusicId)
                .OnDelete(DeleteBehavior.Cascade);

            appMusic
                .HasMany(e => e.MusicReactions)
                .WithOne(e => e.Music)
                .HasForeignKey(e => e.MusicId)
                .OnDelete(DeleteBehavior.Cascade);

            appMusic
                .HasMany(e => e.PlayedMusics)
                .WithOne(e => e.Music)
                .HasForeignKey(e => e.MusicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
