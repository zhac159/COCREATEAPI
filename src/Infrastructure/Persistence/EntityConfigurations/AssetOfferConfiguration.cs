using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class AssetOfferConfiguration : IEntityTypeConfiguration<AssetOffer>
{
    public void Configure(EntityTypeBuilder<AssetOffer> builder)
    {
        builder.ToTable("AssetOffers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.OfferValue).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.AssetUsageStartTime).IsRequired();
        builder.Property(e => e.AssetUsageEndTime).IsRequired();
        builder.Property(e => e.Duration).IsRequired();
        builder.Property(e => e.Description).HasDefaultValue(null);

        builder
            .HasOne(e => e.Asset)
            .WithMany(e => e.AssetOffers)
            .HasForeignKey(e => e.AssetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.Project)
            .WithMany(e => e.AssetOffers)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
