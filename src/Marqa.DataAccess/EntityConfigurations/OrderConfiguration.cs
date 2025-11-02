using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasIndex(o => o.Number).IsUnique();

        builder.HasOne(o => o.Student)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StudentId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
