using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentHomeTaskFeedbackConfiguration : IEntityTypeConfiguration<StudentHomeTaskFeedback>
{
    public void Configure(EntityTypeBuilder<StudentHomeTaskFeedback> builder)
    {   
        builder.HasOne(f => f.StudentHomeTask)
            .WithOne(sht => sht.StudentHomeTaskFeedback)
            .HasForeignKey<StudentHomeTaskFeedback>(f => f.StudentHomeTaskId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(f => f.Teacher)
            .WithOne()
            .HasForeignKey<StudentHomeTaskFeedback>(t => t.TeacherId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}