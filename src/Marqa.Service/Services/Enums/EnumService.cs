using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enums.Models;

namespace Marqa.Service.Services.Enums;
public class EnumService : IEnumService
{
    /// <summary>
    /// Berilgan <typeparamref name="T"/> enum turining barcha qiymatlarini
    /// <see cref="EnumGetModel"/> modeliga o‘tkazib ro‘yxat ko‘rinishida qaytaradi.
    /// </summary>
    /// <typeparam name="T">Qiymatlari olinadigan enum turi.</typeparam>
    /// <returns>
    /// Har bir enum elementining Id va Name maydonlari to‘ldirilgan 
    /// <see cref="EnumGetModel"/> obyektlari ro‘yxati.
    /// </returns>
    /// <exception cref="ArgumentIsNotValidException">
    /// Agar <typeparamref name="T"/> enum turi bo‘lmasa.
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
