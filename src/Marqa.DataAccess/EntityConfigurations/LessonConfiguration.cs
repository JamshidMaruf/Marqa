using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
          builder.HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(l => l.Teacher)
            .WithMany()
            .HasForeignKey(l => l.TeacherId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasMany(l => l.Files)
            .WithOne(f => f.Lesson)
            .HasForeignKey(f => f.LessonId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(l => l.Videos)
            .WithOne(v => v.Lesson)
            .HasForeignKey(v => v.LessonId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();


        builder.HasData(new List<Lesson>
        {
            new ()
            {
                Id = 1,
                Name = "",
                Number = 1,
                StartTime = new TimeOnly(15,00),
                EndTime = new TimeOnly(18,00),
                Date = new DateOnly(2025,10,01),
                IsCompleted = false,
                Room = "uber",
                CourseId = 1,
                TeacherId = 1,
                HomeTaskStatus = HomeTaskStatus.Assigned
            },               

            new()
            {
                Id = 2,
                Name = "",
                Number = 1,
                StartTime = new TimeOnly(15,00),
                EndTime = new TimeOnly(18,00),
                Date = new DateOnly(2025,10,01),
                IsCompleted = false,
                Room = "yandex",
                CourseId = 2,
                TeacherId = 1,
                HomeTaskStatus = HomeTaskStatus.NotAssigned
            },
            new()
            {
                Id = 3,
                Name = "",
                Number = 1,
                StartTime = new TimeOnly(15,00),
                EndTime = new TimeOnly(18,00),
                Date = new DateOnly(2025,10,01),
                IsCompleted = false,
                Room = "uber",
                CourseId = 1,
                TeacherId = 1,
                HomeTaskStatus = HomeTaskStatus.NotAssigned
            },
            new()
            {
                Id = 4,
                Name = "",
                Number = 1,
                StartTime = new TimeOnly(15,00),
                EndTime = new TimeOnly(18,00),
                Date = new DateOnly(2025,10,01),
                IsCompleted = false,
                Room = "uber",
                CourseId = 1,
                TeacherId = 1,
                HomeTaskStatus = HomeTaskStatus.Assigned
            }
        });
    }
}
