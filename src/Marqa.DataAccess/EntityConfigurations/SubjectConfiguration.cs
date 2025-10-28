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

        builder.HasData(new List<Subject>
        {
            new()
            {
                Id = 1,
                Name = "backend development",
                CompanyId = 4,
   
            },
            new()
            {
                Id = 2,
                Name = "Mobile Delevopment",
                CompanyId = 4,

            },
            new()
            {
                Id = 3,
                Name = "English",
                CompanyId = 1,

            },
            new()
            {
                Id = 4,
                Name = "English",
                CompanyId = 2,
            }
        });
    }
}

