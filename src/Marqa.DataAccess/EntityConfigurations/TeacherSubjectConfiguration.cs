using Marqa.Domain.Entities;
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
    }
}