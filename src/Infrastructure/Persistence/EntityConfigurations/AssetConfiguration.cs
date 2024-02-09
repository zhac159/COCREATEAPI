using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(30).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500).IsRequired();
        builder.Property(e => e.AssetType).IsRequired();
        builder.Property(e => e.FileSrcs);
        builder.Property(e => e.Cost).HasDefaultValue(0);
        builder.Property(e => e.UserId).IsRequired();

        builder.Property(e => e.AssetType);

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Assets)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        ;

        builder.HasIndex(e => new { e.AssetType });
    }
}
