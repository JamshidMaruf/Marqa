using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
    public void Configure(EntityTypeBuilder<EmployeeRole> builder)
    {
        builder.HasIndex(er => new { er.CompanyId, er.Name }).IsUnique();

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Roles)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(new List<EmployeeRole>
        {
            new()
            {
                Id = 1,
                Name = "Teacher",
                CompanyId = 1
            },
            new()
            {
                Id = 2,
                Name = "Teacher",
                CompanyId = 2
            },
            new()
            {
                Id = 3,
                Name = "Teacher",
                CompanyId = 3
            },
            new()
            {
                Id = 4,
                Name = "Teacher",
                CompanyId = 4
            }
        });
    }
}
