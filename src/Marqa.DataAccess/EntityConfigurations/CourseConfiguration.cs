using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(p => p.Description)
            .HasMaxLength(5000);  
        
        builder.HasOne(c => c.Company)
            .WithMany()
            .HasForeignKey(c => c.CompanyId)
            .IsRequired();

        builder.HasMany(c => c.Exams)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(c => c.Subject)
            .WithMany()
            .HasForeignKey(c => c.SubjectId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(c => c.Teacher)
            .WithMany()
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasMany(c => c.CourseWeekdays)
            .WithOne(cw => cw.Course)
            .HasForeignKey(cw => cw.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(new List<Course>
        {
            new()
            {
                Id = 1,
                Name = ".Net C#",
                LessonCount = 72,
                StartDate = new DateOnly(2025,10,01),
                StartTime = new TimeOnly(15,00),
                EndTime = new TimeOnly(18,00),
                Description = "Zo'r kurs",
                MaxStudentCount = 24,
                Status = CourseStatus.Active,
                CompanyId = 3,
                SubjectId = 1,
                TeacherId = 1
            },
            new()
            {
                Id = 2,
                Name = "Flutter butcamp",
                LessonCount = 80,
                StartDate = new DateOnly(2025,11,01),
                StartTime = new TimeOnly(15,00),
                EndTime = new TimeOnly(18,00),
                Description = "hali bunaqasi bo'lmagan",
                MaxStudentCount = 20,
                Status = CourseStatus.Upcoming,
                CompanyId = 4,
                SubjectId = 2,
                TeacherId = 2
            },
            new()
            {
                Id = 3,
                Name = "Intensive Ielts 1",
                LessonCount = 72,
                StartDate = new DateOnly(2025,10,01),
                StartTime = new TimeOnly(15,00),
                EndTime = new TimeOnly(18,00),
                Description = "Zo'r kurs",
                MaxStudentCount = 24,
                Status = CourseStatus.Active,
                CompanyId = 2,
                SubjectId = 4,
                TeacherId = 3
            },
            new()
            {
                Id = 4,
                Name = "General English",
                LessonCount = 72,
                StartDate = new DateOnly(2025,11,01),
                StartTime = new TimeOnly(11,00),
                EndTime = new TimeOnly(13,00),
                Description = "Zo'r kurs",
                MaxStudentCount = 24,
                Status = CourseStatus.Upcoming,
                CompanyId = 1,
                SubjectId = 3,
                TeacherId = 4
            }
        });
    }
}
