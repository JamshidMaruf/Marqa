using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasData(
            new Company
            {
                Id = 1,
                Name = "name",
                Address = "address",
                Phone = "947157279",
                Email = "email",
                Director = "Salim"
            },
            new Company
            {
                Id = 2,
                Name = "test",
                Address = "address",
                Phone = "123456987",
                Email = "email",
                Director = "Murodjon"
            });
    }
}
