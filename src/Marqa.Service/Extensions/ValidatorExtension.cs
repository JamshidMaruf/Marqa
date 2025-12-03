using FluentValidation;
using Marqa.Service.Exceptions;

namespace Marqa.Service.Extensions;

public static class ValidatorExtension
{
    /// <summary>
    /// Modelni FluentValidation orqali tekshiradi va agar xatolik bo‘lsa,
    /// validatsiya xabarlarini o‘z ichiga olgan <see cref="ValidateException"/>
    /// exception tashlaydi.
    /// </summary>
    /// <typeparam name="TModel">
    /// Validatsiya qilinayotgan model turi.
    /// </typeparam>
    /// <param name="validator">
    /// FluentValidation validator obyekti.
    /// </param>
    /// <param name="model">
    /// Validatsiya qilinishi kerak bo‘lgan model.
    /// </param>
    /// <returns>
    /// Asinxron operatsiyasi <see cref="Task"/>.
    /// </returns>
    /// <exception cref="ValidateException">
    /// Agar model validatsiyadan muvaffaqiyatli o‘tmasa,
    /// xatoliklar ro‘yxati bilan birga ushbu istisno tashlanadi.
    /// </exception>
    public static async Task EnsureValidatedAsync<TModel>(this IValidator<TModel> validator, TModel model)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
            throw new ValidateException(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
    }
}