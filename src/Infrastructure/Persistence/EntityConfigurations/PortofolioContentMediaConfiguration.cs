using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class PortofolioContentMediaConfiguration : IEntityTypeConfiguration<PortofolioContentMedia>
{
    public void Configure(EntityTypeBuilder<PortofolioContentMedia> builder)
    {
        builder.ToTable("PortofolioContentMedias");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Uri).IsRequired();
        builder.Property(e => e.MediaType).IsRequired();
        builder.Property(e => e.Order).IsRequired();

        builder
            .HasOne(e => e.PortofolioContent)
            .WithMany(e => e.Medias)
            .HasForeignKey(e => e.PortofolioContentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
