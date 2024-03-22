using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class EnquiryMessageConfiguration : IEntityTypeConfiguration<EnquiryMessage>
{
    public void Configure(EntityTypeBuilder<EnquiryMessage> builder)
    {
        builder.ToTable("EnquiryMessages");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.Message);
        builder.Property(e => e.Uri);
        builder.Property(e => e.MediaType);
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.SenderId).IsRequired();

        builder
            .HasOne(e => e.Enquiry)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => e.EnquiryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.EnquiryId);
        builder.HasIndex(e => e.Date);
    }
}
