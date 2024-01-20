using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {   
        builder.ToTable("Skills");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.SkillType).IsRequired();
        builder.Property(e => e.Description).HasDefaultValue("");
        builder.Property(e => e.Level).IsRequired();
        builder.Property(e => e.UserId).IsRequired();

        builder.Property(e => e.SkillType).HasConversion<string>();

        builder.HasOne(e => e.User).WithMany(e => e.Skills).HasForeignKey(e => e.UserId);

        builder.HasIndex(e => new {e.SkillType});
    }
}
