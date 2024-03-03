using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.ToTable("ProjectRoles");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Cost).IsRequired();
        builder.Property(e => e.Effort).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
        builder.Property(e => e.SkillType).IsRequired();
        builder.Property(e => e.Location).IsRequired();
        builder.Property(e => e.Address).IsRequired();
        builder.Property(e => e.Keywords).IsRequired();
        builder.Property(e => e.Remote).IsRequired();
        
        builder
            .HasOne(e => e.Assignee)
            .WithMany(e => e.ProjectRoles)
            .HasForeignKey(e => e.AssigneeId)
            .IsRequired(false);
            
        builder
            .HasOne(e => e.Project)
            .WithMany(e => e.ProjectRoles)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
