using System.ComponentModel;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enums.Models;

namespace Marqa.Service.Services.Enums;
public class EnumService : IEnumService
{
    public List<EnumGetModel> GetEnumValues<T>() where T : Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentIsNotValidException("The provided type parameter T is not an enum.");

        return typeof(T)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Select(field =>
            {
                var value = (T)field.GetValue(null);

                var description = field
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>()
                    .FirstOrDefault()?.Description ?? field.Name;

                return new EnumGetModel
                {
                    Id = Convert.ToInt32(value),
                    Name = description
                };
            })
            .ToList();
    }

    public YearlyMonths GetYearlyMonths()
    {
        return new YearlyMonths
        {
            CurrentYear = DateTime.UtcNow.Year,

            Months = Enum.GetValues(typeof(Month))
            .Cast<Month>()
            .Select(m => new YearlyMonths.MonthData
            {
                Id = Convert.ToByte(m),
                Name = m.ToString()
            }).ToList()
        };
    }
}
