using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class HomeTaskConfiguration : IEntityTypeConfiguration<HomeTask>
{
    public void Configure(EntityTypeBuilder<HomeTask> builder)
    {
        builder.Property(ht => ht.Description)
            .HasMaxLength(5000);

        builder.HasOne(c => c.Lesson)
            .WithMany(l => l.HomeTasks)
            .HasForeignKey(c => c.LessonId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(h => h.HomeTaskFile)
            .WithOne(h => h.HomeTask)
            .HasForeignKey<HomeTaskFile>(hf => hf.HomeTaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(new List<HomeTask>
        {
            new ()
            {
                Id = 1,
                LessonId = 1,
                Deadline = new DateTime(2025,11,03,15,00,00, DateTimeKind.Utc),
                Description = "Description",
            },
            new()
            {
                Id = 2,
                LessonId = 2,
                Deadline = new DateTime(2025,11,05,15,00,00, DateTimeKind.Utc),
                Description = "Description",
            },
            new()
            {
                Id = 3,
                LessonId = 3,
                Deadline = new DateTime(2025,11,07,15,00,00, DateTimeKind.Utc),
                Description = "Description",
            },
            new()
            {
                Id = 4,
                LessonId = 4,
                Deadline = new DateTime(2025,11,10,15,00,00, DateTimeKind.Utc),
                Description = "Description",
            }
        });
    }
}
