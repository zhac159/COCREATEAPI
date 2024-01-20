using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class PortofolioContentConfiguration : IEntityTypeConfiguration<PortofolioContent>
{
    public void Configure(EntityTypeBuilder<PortofolioContent> builder)
    {
        builder.ToTable("PortofolioContents");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Description).HasMaxLength(500).HasDefaultValue("");
        builder.Property(e => e.Title).HasMaxLength(30).IsRequired();
        builder.Property(e => e.FileSrc).IsRequired();
        builder.Property(e => e.FileType).IsRequired();
        builder.Property(e => e.Order).IsRequired();
        builder.Property(e => e.UserId).IsRequired();

        builder.Property(e => e.FileType).HasConversion<string>();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.PortofolioContents)
            .HasForeignKey(e => e.UserId);
    }
}
