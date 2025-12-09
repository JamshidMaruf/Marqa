using System.ComponentModel;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enums.Models;

namespace Marqa.Service.Services.Enums;
public class EnumService : IEnumService
{
    public List<EnumGetModel> GetEnumValues<T>() where T : Enum
    {
        var memberInfo = typeof(T).GetMembers();
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentIsNotValidException("The provided type parameter T is not an enum.");
        }
        var attributes = memberInfo
            .Where(mi => mi.MemberType == System.Reflection.MemberTypes.Field)
            .Select(mi =>
            {
                var descriptionAttribute = mi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;
                var enumValue = (T)Enum.Parse(typeof(T), mi.Name);
                return new EnumGetModel
                {
                    Id = Convert.ToInt32(enumValue),
                    Name = descriptionAttribute != null ? descriptionAttribute.Description : mi.Name
                };
            })
            .ToList();
        return attributes;
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
