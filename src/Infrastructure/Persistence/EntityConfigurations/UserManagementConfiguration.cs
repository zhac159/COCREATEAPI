using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId);

        builder.Property(e => e.Username).HasMaxLength(200).IsRequired();
        builder.HasIndex(e => e.Username).IsUnique();
        builder.Property(e => e.Password).HasMaxLength(200).IsRequired();
    }
}
