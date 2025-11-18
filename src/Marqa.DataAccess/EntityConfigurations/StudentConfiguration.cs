using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasIndex(s => new { s.Phone, s.CompanyId }).IsUnique();

        builder.Property(p => p.PasswordHash)
            .HasMaxLength(400);

        builder.HasOne(s => s.Company)
            .WithMany()
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
