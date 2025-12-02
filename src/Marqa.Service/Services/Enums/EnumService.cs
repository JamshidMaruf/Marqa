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
            .Select(X => new EnumGetModel
            {
                Id = Convert.ToInt32(X),
                Name = X.ToString()
            })
            .ToList();
    }
}
