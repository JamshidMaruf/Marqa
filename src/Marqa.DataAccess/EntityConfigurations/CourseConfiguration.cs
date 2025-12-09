using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property<long>("RowVersion").HasDefaultValue(1).IsRowVersion();

        builder.Property(p => p.Description)
            .HasMaxLength(5000);  
        
        builder.HasOne(c => c.Company)
            .WithMany()
            .HasForeignKey(c => c.CompanyId)
            .IsRequired();

        builder.HasMany(c => c.Exams)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(c => c.Subject)
            .WithMany()
            .HasForeignKey(c => c.SubjectId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        builder.HasMany(c => c.CourseWeekdays)
            .WithOne(cw => cw.Course)
            .HasForeignKey(cw => cw.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
