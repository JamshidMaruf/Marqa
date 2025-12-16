using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marqa.DataAccess.Helpers;

internal sealed class UtcNullableDateTimeConverter : ValueConverter<DateTime?, DateTime?>
{
    public UtcNullableDateTimeConverter()
        : base(
            v => v.HasValue
                ? v.Value.Kind == DateTimeKind.Utc ? v : v.Value.ToUniversalTime()
                : v,
            v => v.HasValue
                ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
                : v)
    {
    }
}