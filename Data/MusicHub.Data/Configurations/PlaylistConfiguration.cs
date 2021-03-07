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

    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> appPlaylist)
        {
            appPlaylist
              .HasMany(e => e.PlaylistMusics)
              .WithOne(e => e.Playlist)
              .HasForeignKey(e => e.PlaylistId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
