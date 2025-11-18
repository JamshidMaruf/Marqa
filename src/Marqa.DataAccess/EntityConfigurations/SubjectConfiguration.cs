using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasOne(s => s.Company)
            .WithMany(c => c.Subjects)
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

