using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class TeacherAssessmentConfiguration : IEntityTypeConfiguration<TeacherAssessment>
{
    public void Configure(EntityTypeBuilder<TeacherAssessment> builder)
    {
        // Relationships
        builder.HasOne(ta => ta.Teacher)
            .WithMany(t => t.TeacherAssessments)
            .HasForeignKey(ta => ta.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ta => ta.Student)
            .WithMany(s => s.TeacherAssessments)
            .HasForeignKey(ta => ta.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ta => ta.Course)
            .WithMany(c => c.TeacherAssessments)
            .HasForeignKey(ta => ta.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes for better query performance
        builder.HasIndex(ta => ta.TeacherId);
        builder.HasIndex(ta => ta.StudentId);
        builder.HasIndex(ta => ta.CourseId);
        builder.HasIndex(ta => ta.SubmittedDateTime);
    }
}