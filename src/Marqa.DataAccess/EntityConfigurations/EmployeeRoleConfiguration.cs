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
    }
}
