using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentDetailConfiguration : IEntityTypeConfiguration<StudentDetail>
{
    public void Configure(EntityTypeBuilder<StudentDetail> builder)
    {
        builder
            .HasOne(d => d.Student)
            .WithOne(s => s.StudentDetail)
            .HasForeignKey<StudentDetail>(s => s.StudentId);
    }
}