using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ChatMembershipConfiguration : IEntityTypeConfiguration<ChatMembership>
{
    public void Configure(EntityTypeBuilder<ChatMembership> builder)
    {
        builder.ToTable("ChatMemberships");

        builder.HasKey(e => e.Id);

        builder
            .HasOne(e => e.Chat)
            .WithMany(e => e.ChatMemberships)
            .HasForeignKey(e => e.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.ChatMemberships)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
