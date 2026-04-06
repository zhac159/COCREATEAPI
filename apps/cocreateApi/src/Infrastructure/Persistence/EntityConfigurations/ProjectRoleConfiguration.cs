using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(1000);

        builder
            .HasOne(e => e.Project)
            .WithMany(p => p.ProjectRoles)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.Assignee)
            .WithMany(u => u.ProjectRoles)
            .HasForeignKey(e => e.AssigneeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
