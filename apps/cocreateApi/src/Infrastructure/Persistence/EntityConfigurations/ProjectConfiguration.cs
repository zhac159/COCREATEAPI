using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(2000);
        builder.Property(e => e.Completed).HasDefaultValue(false);
        builder.Property(e => e.Address).HasMaxLength(500);

        builder
            .HasOne(e => e.ProjectManager)
            .WithMany(u => u.Projects)
            .HasForeignKey(e => e.ProjectManagerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
