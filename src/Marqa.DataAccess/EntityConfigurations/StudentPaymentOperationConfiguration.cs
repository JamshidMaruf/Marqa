using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentPaymentOperationConfiguration : IEntityTypeConfiguration<StudentPaymentOperation>
{
    public void Configure(EntityTypeBuilder<StudentPaymentOperation> builder)
    {
        builder.HasIndex(s => s.PaymentNumber).IsUnique();

        builder.Property<long>("RowVersion").IsRowVersion();
    }
}

