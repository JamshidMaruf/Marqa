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
    }
}
