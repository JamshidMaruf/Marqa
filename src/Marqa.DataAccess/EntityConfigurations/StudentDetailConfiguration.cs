using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentDetailConfiguration : IEntityTypeConfiguration<StudentDetail>
{
    public void Configure(EntityTypeBuilder<StudentDetail> builder)
    {
        builder.HasIndex(sd => sd.FatherPhone).IsUnique();
        builder.HasIndex(sd => sd.MotherPhone).IsUnique();
        builder.HasIndex(sd => sd.GuardianPhone).IsUnique();

        builder.HasOne(sd => sd.Student)
            .WithOne(s => s.StudentDetail)
            .HasForeignKey<StudentDetail>(sd => sd.StudentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
