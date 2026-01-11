using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Description).HasDefaultValue(null);
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.ExperienceType).IsRequired();

        builder.HasOne(e => e.ProjectRole)
            .WithMany(pr => pr.Experiences)
            .HasForeignKey(e => e.ProjectRoleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.Project)
            .WithMany(p => p.Experiences)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.User)
            .WithMany(u => u.Experiences)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
 