using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ProjectRoleMediaConfiguration : IEntityTypeConfiguration<ProjectRoleMedia>
{
    public void Configure(EntityTypeBuilder<ProjectRoleMedia> builder)
    {
        builder.ToTable("ProjectRoleMedias");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Uri).IsRequired();
        builder.Property(e => e.MediaType).IsRequired();

        builder
            .HasOne(e => e.ProjectRole)
            .WithMany(e => e.Medias)
            .HasForeignKey(e => e.ProjectRoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.MediaType });
    }
}
