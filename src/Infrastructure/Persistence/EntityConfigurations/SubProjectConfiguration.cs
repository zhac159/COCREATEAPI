using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class SubProjectConfiguration : IEntityTypeConfiguration<SubProject>
{
    public void Configure(EntityTypeBuilder<SubProject> builder)
    {
        builder.ToTable("SubProjects");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Cost).IsRequired();
        builder.Property(e => e.Effort).IsRequired();
        builder.Property(e => e.Remote).IsRequired();
        builder.Property(e => e.Longitude).HasDefaultValue(null);
        builder.Property(e => e.Latitude).HasDefaultValue(null);
        
    }
}