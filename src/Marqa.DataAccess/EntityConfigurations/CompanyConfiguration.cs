using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasData(new List<Company>
        {
            new()
            {
                Id = 1,
                Name = "Result School",
            },
            new()
            {
                Id = 2,
                Name = "Cambridge school",
            },
            new()
            {
                Id = 3,
                Name = "Pdp Academy",
            },
            new()
            {
                Id = 4,
                Name = "Najot Ta'lim",
            }
        });
    }
}
