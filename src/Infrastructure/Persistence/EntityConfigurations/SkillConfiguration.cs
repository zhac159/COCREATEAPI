using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User).WithMany(e => e.Skills).HasForeignKey(e => e.UserId);

        builder.HasIndex(e => new { e.SkillType });
    }
}
