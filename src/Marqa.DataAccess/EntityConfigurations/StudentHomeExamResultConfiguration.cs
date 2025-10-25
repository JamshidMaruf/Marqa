using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentHomeExamResultConfiguration : IEntityTypeConfiguration<StudentExamResult>
{
    public void Configure(EntityTypeBuilder<StudentExamResult> builder)
    {
        builder.Property(s => s.TeacherFeedback)
            .HasMaxLength(5000);

        builder.HasOne(ser => ser.Student)
            .WithMany(s => s.ExamResults)
            .HasForeignKey(ser => ser.StudentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(ser => ser.Exam)
            .WithMany(e => e.ExamResults)
            .HasForeignKey(ser => ser.ExamId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

