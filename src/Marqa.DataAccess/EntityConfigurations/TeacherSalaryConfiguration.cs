using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class TeacherSalaryConfiguration : IEntityTypeConfiguration<TeacherSalary>
{
    public void Configure(EntityTypeBuilder<TeacherSalary> builder)
    {
        builder.ToTable("TeacherSalaries");
    }
}