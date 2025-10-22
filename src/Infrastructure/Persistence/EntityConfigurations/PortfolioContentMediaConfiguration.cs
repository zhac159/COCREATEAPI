using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class PortfolioContentMediaConfiguration : IEntityTypeConfiguration<PortflioContentMedia>
{
    public void Configure(EntityTypeBuilder<PortflioContentMedia> builder)
    {
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
