using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class SeenMatchesConfiguration : IEntityTypeConfiguration<SeenMatches>
{
    public void Configure(EntityTypeBuilder<SeenMatches> builder)
    {
        builder.ToTable("SeenMatches");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.ProjectRoleId).IsRequired();
        builder.Property(e => e.SeenAt).IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.SeenMatches)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.ProjectRole)
            .WithMany(e => e.SeenMatches)
            .HasForeignKey(e => e.ProjectRoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
