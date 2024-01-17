
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class TestTableConfiguration : IEntityTypeConfiguration<TestTable>
    {
        public void Configure(EntityTypeBuilder<TestTable> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}