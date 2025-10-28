using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class LessonAttendanceConfiguration : IEntityTypeConfiguration<LessonAttendance>
{
    public void Configure(EntityTypeBuilder<LessonAttendance> builder)
    {
         builder.Ignore(la => la.Id);

        builder.HasKey(l => new { l.StudentId, l.LessonId });
         
        builder.Property(la => la.Status)
            .HasDefaultValue(AttendanceStatus.None);

        builder.HasOne(la => la.Lesson)
            .WithMany(l => l.Attendances)
            .HasForeignKey(la => la.LessonId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(la => la.Student)
            .WithMany()
            .HasForeignKey(la => la.StudentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasData(new List<LessonAttendance>
        {
            new()
            {
                Id = 1,
                LateTimeInMinutes = 0,
                LessonId = 1,
                Status = AttendanceStatus.Present,
                StudentId = 1, 
            },
            new()
            {
                Id = 2,
                LateTimeInMinutes = 1,
                LessonId = 1,
                Status = AttendanceStatus.Late,
                StudentId = 2
            },
            new()
            {
                Id = 3,
                LateTimeInMinutes = 10,
                LessonId = 1,
                Status = AttendanceStatus.Late,
                StudentId = 3
            },
            new()
            {
                Id = 4,
                LateTimeInMinutes = 0,
                LessonId = 1,
                Status = AttendanceStatus.Present,
                StudentId = 4
            }
        });
    }
}
