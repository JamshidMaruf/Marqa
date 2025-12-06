using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enums.Models;

namespace Marqa.Service.Services.Enums;
public class EnumService : IEnumService
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
    public List<EnumGetModel> GetEnumValues<T>() where T : Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentIsNotValidException("Argument must be an enum");

        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(X => new EnumGetModel
            {
                Id = Convert.ToInt32(X),
                Name = X.ToString()
            })
            .ToList();
    }
}
