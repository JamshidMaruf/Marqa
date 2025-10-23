using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasOne(s => s.Company)
            .WithMany()
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(s => s.StudentDetail)
            .WithOne(sd => sd.Student)
            .HasForeignKey<StudentDetail>(sd => sd.StudentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // xato bu relationshipni togirlash esdan chiqmasin
        builder.HasMany(s => s.Courses)
            .WithMany(c => c.Students);

        builder.HasMany(s => s.StudentPointHistories)
            .WithOne(sph => sph.Student)
            .HasForeignKey(sph => sph.StudentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
