using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(30).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500).IsRequired();
        builder.Property(e => e.FileSrcs);
        builder.Property(e => e.ProjectManagerId).IsRequired();

        builder
            .HasOne(e => e.ProjectManager)
            .WithMany(e => e.Projects)
            .HasForeignKey(e => e.ProjectManagerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
