using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class TeacherSubjectConfiguration : IEntityTypeConfiguration<TeacherSubject>
{
    public void Configure(EntityTypeBuilder<TeacherSubject> builder)
    {
        builder.Ignore(ts => ts.Id);

        builder.HasKey(ts => new { ts.SubjectId, ts.TeacherId });

        builder.HasOne(ts => ts.Subject)
            .WithMany()
            .HasForeignKey(ts => ts.SubjectId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(ts => ts.Teacher)
            .WithOne()
            .HasForeignKey<TeacherSubject>(ts => ts.TeacherId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(new List<TeacherSubject>
        {
            new()
            {
                TeacherId = 1,
                SubjectId = 1
            },
            new()
            {
                Id = 2,
                TeacherId = 2,
                SubjectId = 2
            },
            new()
            {
                Id = 3,
                TeacherId = 3,
                SubjectId = 1
            },
            new()
            {
                Id = 4,
                TeacherId = 4,
                SubjectId = 1
            }
        });
    }
}