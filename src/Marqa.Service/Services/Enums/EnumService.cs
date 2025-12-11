using System.ComponentModel;
using System.Reflection;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enums.Models;
using Newtonsoft.Json.Linq;

namespace Marqa.Service.Services.Enums;
public class EnumService : IEnumService
{
    public List<EnumGetModel> GetEnumValues<T>() where T : Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentIsNotValidException("The provided type parameter T is not an enum.");

        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
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

    public YearlyMonths GetCurrentYearlyMonths()
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

    public string GetEnumDescription(Enum value)
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

        var attributes = (DescriptionAttribute[])fieldInfo
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}
