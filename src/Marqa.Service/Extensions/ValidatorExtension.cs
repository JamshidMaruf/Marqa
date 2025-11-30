using FluentValidation;
using Marqa.Service.Exceptions;

namespace Marqa.Service.Extensions;

public static class ValidatorExtension
{
    public static async Task EnsureValidatedAsync<TModel>(this IValidator<TModel> validator, TModel model)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
            throw new ArgumentIsNotValidException(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
    }
}

// 1. Create custom exception fo validation
// 2. Update EnsureValidatedAsync metod
// 3. Move existValidations to Validator
// 4. Crete Handler for custom exception