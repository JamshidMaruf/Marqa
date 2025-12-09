using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enums.Models;

namespace Marqa.Service.Services.Enums;
public class EnumService : IEnumService
{
    public List<EnumGetModel> GetEnumValues<T>() where T : Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentIsNotValidException("Argument must be an enum");

        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(x => new EnumGetModel
            {
                Id = Convert.ToInt32(x),
                Name = x.ToString()
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
