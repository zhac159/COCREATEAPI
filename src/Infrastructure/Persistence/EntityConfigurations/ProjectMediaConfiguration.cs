using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ProjectMediaConfiguration : IEntityTypeConfiguration<ProjectMedia>
{
    public void Configure(EntityTypeBuilder<ProjectMedia> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasOne(e => e.Project)
            .WithMany(p => p.ProjectMedias)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
