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
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired();
    }
}
