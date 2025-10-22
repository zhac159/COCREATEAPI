using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Username).HasMaxLength(30).IsRequired();
        builder.Property(e => e.PasswordHash).HasMaxLength(300).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(200).IsRequired();
        builder.Property(e => e.IsEmailVerified).IsRequired().HasDefaultValue(false);
        builder.Property(e => e.Location).HasDefaultValue(null);
        builder.Property(e => e.Address).HasDefaultValue(null);
        builder.Property(e => e.AboutYou).HasMaxLength(2000).HasDefaultValue(null);
        builder.Property(e => e.Coins).IsRequired().HasDefaultValue(0);
        builder.Property(e => e.ProfilePictureSrc).HasDefaultValue(null);
        builder.Property(e => e.BannerPictureSrc).HasDefaultValue(null);
        builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("NOW()");
    }
}
