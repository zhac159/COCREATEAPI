using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class MediaConfiguration : IEntityTypeConfiguration<AssetMedia>
{
    public void Configure(EntityTypeBuilder<AssetMedia> builder)
    {
        builder.ToTable("Medias");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Uri).IsRequired();
        builder.Property(e => e.MediaType).IsRequired();
        builder.Property(e => e.Order).IsRequired();

        builder
            .HasOne(e => e.Asset)
            .WithMany(e => e.Medias)
            .HasForeignKey(e => e.AssetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.MediaType });
    }
}
