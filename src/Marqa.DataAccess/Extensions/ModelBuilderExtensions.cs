using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Extensions;
public static class ModelBuilderExtensions
{
    public static void ApplyGlobalTableNameConfiguration(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
            entity.SetTableName(ToSnakeCase(entity.ClrType.Name) + "s");     
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var result = new StringBuilder();
        result.Append(char.ToLowerInvariant(input[0]));

        for (int i = 1; i < input.Length - 1; i++)
        {
            if (char.IsUpper(input[i]))
            {
                if (!char.IsUpper(input[i + 1]))
                    result.Append('_');

                result.Append(char.ToLowerInvariant(input[i]));
            }
            else
            {
                result.Append(input[i]);
            }
        }

        result.Append(char.ToLowerInvariant(input[input.Length - 1]));
        return result.ToString();
    }
}