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

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> appCategory)
        {
            appCategory.HasMany(e => e.Musics)
                 .WithOne(e => e.Category)
                 .HasForeignKey(e => e.CategoryId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
