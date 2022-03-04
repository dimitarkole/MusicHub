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

    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> appAlbum)
        {
            appAlbum
              .HasMany(e => e.AlbumMusics)
              .WithOne(e => e.Album)
              .HasForeignKey(e => e.AlbumId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
