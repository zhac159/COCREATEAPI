using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class EnquiryConfiguration : IEntityTypeConfiguration<Enquiry>
{
    public void Configure(EntityTypeBuilder<Enquiry> builder)
    {
        builder.ToTable("Enquiries");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.EnquirerId).IsRequired();
        builder.Property(e => e.ProjectRoleId).IsRequired();
        builder.Property(e => e.CreateAt).IsRequired();

        builder
            .HasOne(e => e.Enquirer)
            .WithMany(e => e.Enquiries)
            .HasForeignKey(e => e.EnquirerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(e => e.ProjectManager)
            .WithMany(e => e.EnquiriesReceived)
            .HasForeignKey(e => e.ProjectManagerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.ProjectRole)
            .WithMany(e => e.Enquiries)
            .HasForeignKey(e => e.ProjectRoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.EnquirerId, e.ProjectRoleId }).IsUnique();
    }
}
