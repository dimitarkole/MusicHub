namespace MusicHub.Data.Configurations
{
    using MusicHub.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static MusicHub.Common.ModelConstants;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(UserConstants.NameMaxlength)
                .IsUnicode(false);

            builder.Property(e => e.ImageUrl)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(UserConstants.NameMaxlength)
                .IsUnicode(false);

            builder
                .HasMany(e => e.Roles)
                .WithOne(u => u.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}