using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentDetailConfiguration : IEntityTypeConfiguration<StudentDetail>
{
    public void Configure(EntityTypeBuilder<StudentDetail> builder)
    {
        builder.HasIndex(sd => new { sd.FatherPhone, sd.CompanyId }).IsUnique();
        builder.HasIndex(sd => new { sd.MotherPhone, sd.CompanyId }).IsUnique();
        builder.HasIndex(sd => new { sd.GuardianPhone, sd.CompanyId }).IsUnique();

        builder.HasOne(sd => sd.Student)
            .WithOne(s => s.StudentDetail)
            .HasForeignKey<StudentDetail>(sd => sd.StudentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
