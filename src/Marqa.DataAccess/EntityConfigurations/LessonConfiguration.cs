using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
          builder.Property(l => l.Name).IsRequired(false);

          builder.HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
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
    }
}
