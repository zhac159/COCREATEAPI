using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.UserId);
        builder.Property(e => e.Username).HasMaxLength(30).IsRequired();
        builder.Property(e => e.Password).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Location).HasDefaultValue(null);
        builder.Property(e => e.Address).HasDefaultValue(null);
        builder.Property(e => e.AboutYou).HasMaxLength(2000).HasDefaultValue(null);
        builder.Property(e => e.Coins).IsRequired().HasDefaultValue(10000);
        builder.Property(e => e.Rating).IsRequired().HasDefaultValue(0);
        builder.Property(e => e.TotalReviews).IsRequired().HasDefaultValue(0);
        builder.Property(e => e.ProfilePictureSrc).HasDefaultValue(null);
        builder.Property(e => e.BannerPictureSrc).HasDefaultValue(null);

        builder.HasIndex(e => new { e.Username }).IsUnique();
        builder.HasIndex(e => new { e.Email }).IsUnique();
    }
}
