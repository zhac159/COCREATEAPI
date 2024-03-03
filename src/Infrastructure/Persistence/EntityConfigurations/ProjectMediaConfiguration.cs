using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ProjectMediaConfiguration : IEntityTypeConfiguration<ProjectMedia>
{
    public void Configure(EntityTypeBuilder<ProjectMedia> builder)
    {
        builder.ToTable("ProjectMedias");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Uri).IsRequired();
        builder.Property(e => e.MediaType).IsRequired();
        builder.Property(e => e.Order).IsRequired();

        builder
            .HasOne(e => e.Project)
            .WithMany(e => e.Medias)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.MediaType });
    }
}
