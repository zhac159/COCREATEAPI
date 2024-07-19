using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class VoucherCodeConfiguration : IEntityTypeConfiguration<VoucherCode>
{
    public void Configure(EntityTypeBuilder<VoucherCode> builder)
    {
        builder.ToTable("VoucherCodes");


        builder.HasKey(e => e.Code);
        builder.Property(e => e.Amount).IsRequired();

    }
}
