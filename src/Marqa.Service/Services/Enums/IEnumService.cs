using Marqa.Service.Services.Enums.Models;

namespace Marqa.Service.Services.Enums;
public interface IEnumService
{

    /// <summary>
    /// The given enum type's values are retrieved and returned as a list of <see cref="EnumGetModel"/> objects.
    /// </summary>
    /// <typeparam name="T">
    /// The enum type whose values are to be retrieved.
    /// </typeparam>
    /// <returns>
    /// The method returns a list of <see cref="EnumGetModel"/> objects,
    /// Where each object contains the integer value and name of each enum member.
    /// </returns>
    /// <exception cref="ArgumentIsNotValidException">
    /// If the provided type parameter T is not an enum,
    /// </exception>
    public List<EnumGetModel> GetEnumValues<T>() where T : Enum;

    /// <summary>
    /// The method gets year and month data which is internal number + name as int and string data types
    /// </summary>
    /// <returns>
    /// Returns current year and name + id for each month in the enum <see cref="YearlyMonths"/> 
    /// </returns>
    public YearlyMonths GetYearlyMonths();
}
