using FluentValidation;
using Marqa.Service.Exceptions;

namespace Marqa.Service.Extensions;

public static class ValidatorExtension
{
    public static async Task EnsureValidatedAsync<TModel>(this IValidator<TModel> validator, TModel model)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
            throw new ValidateException(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
    }
}