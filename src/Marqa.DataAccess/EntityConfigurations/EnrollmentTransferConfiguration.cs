using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class EnrollmentTransferConfiguration : IEntityTypeConfiguration<EnrollmentTransfer>
{
    public void Configure(EntityTypeBuilder<EnrollmentTransfer> builder)
    {
        builder.HasOne(t => t.ToEnrollment)
            .WithOne()
            .HasForeignKey<EnrollmentTransfer>(t => t.ToEnrollmentId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(t => t.FromEnrollment)
            .WithOne()
            .HasForeignKey<EnrollmentTransfer>(t => t.FromEnrollmentId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
