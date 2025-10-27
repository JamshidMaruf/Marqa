using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentHomeTaskConfiguration : IEntityTypeConfiguration<StudentHomeTask>
{
    public void Configure(EntityTypeBuilder<StudentHomeTask> builder)
    {   
        builder.HasOne(ht => ht.Student)
            .WithMany(s => s.StudentHomeTasks)
            .HasForeignKey(ht => ht.StudentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(ht => ht.StudentHomeTaskFile)
            .WithOne(shf => shf.StudentHomeTask)
            .HasForeignKey<StudentHomeTaskFile>(shf => shf.StudentHomeTaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();  
    }
}
