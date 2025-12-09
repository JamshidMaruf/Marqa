using Marqa.DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Extensions;

public static class ModelConfigurationBuilderExtensions
{
    public static void ApplyDefaultConventions(this ModelConfigurationBuilder configurationBuilder)
    {
        // DateTime -> UTC
        configurationBuilder
            .Properties<DateTime>()
            .HaveConversion<UtcDateTimeConverter>();

        configurationBuilder
            .Properties<DateTime?>()
            .HaveConversion<UtcNullableDateTimeConverter>();

        // decimal -> default Precision and Scale
        configurationBuilder
            .Properties<decimal>()
            .HavePrecision(18, 3);
        configurationBuilder
            .Properties<decimal?>()
            .HavePrecision(18, 3);

        // string -> SetMaxLength
        configurationBuilder
            .Properties<string>()
            .HaveMaxLength(255);
    }
}
