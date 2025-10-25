using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(p => p.PasswordHash)
            .HasMaxLength(400);

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

        builder.HasMany(s => s.StudentPointHistories)
            .WithOne(sph => sph.Student)
            .HasForeignKey(sph => sph.StudentId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
