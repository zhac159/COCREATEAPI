using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class PortfolioContentMediaConfiguration : IEntityTypeConfiguration<PortfolioContentMedia>
{
    public void Configure(EntityTypeBuilder<PortfolioContentMedia> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.PortfolioMedias)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
