using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.Ignore(la => la.Id);

        builder.HasKey(sc => new { sc.StudentId, sc.CourseId });
        
        builder.HasOne(sc => sc.Student)
            .WithMany(s => s.Courses)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(new List<StudentCourse>
        {
            new()
            {
                StudentId = 1,
                CourseId = 1
            },
            new()
            {
                StudentId = 2,
                CourseId = 1
            },
            new()
            {
                StudentId = 3,
                CourseId = 1
            },
            new()
            {
                StudentId = 4,
                CourseId = 1
            }
        });
    }
}
