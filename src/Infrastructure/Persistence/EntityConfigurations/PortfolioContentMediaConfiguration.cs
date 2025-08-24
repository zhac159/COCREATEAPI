using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class PortfolioContentMediaConfiguration : IEntityTypeConfiguration<PortfolioContentMedia>
{
    public void Configure(EntityTypeBuilder<PortfolioContentMedia> builder)
    {
        builder.ToTable("PortfolioContentMedias");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Uri).IsRequired();
        builder.Property(e => e.MediaType).IsRequired();
        builder.Property(e => e.Order).IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.PortfolioMedias)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
