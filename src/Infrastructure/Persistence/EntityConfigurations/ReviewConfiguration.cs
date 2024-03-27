using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Description).HasMaxLength(1000).IsRequired();
        builder.Property(e => e.Rating).IsRequired();
        builder.Property(e => e.ReviewerUserId).IsRequired();
        builder.Property(e => e.ReviewedUserId).IsRequired();

        builder.HasOne(e => e.ReviewerUser).WithMany(e => e.ReviewsGiven).HasForeignKey(e => e.ReviewerUserId);
        builder.HasOne(e => e.ReviewedUser).WithMany(e => e.ReviewsReceived).HasForeignKey(e => e.ReviewedUserId);
    }
}