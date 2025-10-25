using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
    public void Configure(EntityTypeBuilder<EmployeeRole> builder)
    {
        builder.HasOne(e => e.Company)
            .WithMany(c => c.Roles)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
