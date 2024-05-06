using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ExperienceMediaConfiguration : IEntityTypeConfiguration<ExperienceMedia>
{
    public void Configure(EntityTypeBuilder<ExperienceMedia> builder)
    {
        builder.ToTable("ExperienceMedias");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Uri).IsRequired();
        builder.Property(e => e.MediaType).IsRequired();

        builder
            .HasOne(e => e.Experience)
            .WithMany(e => e.Medias)
            .HasForeignKey(e => e.ExperienceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.MediaType });
    }
}
