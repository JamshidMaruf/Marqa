using FluentValidation;
using Marqa.Service.Exceptions;

namespace Marqa.Service.Extensions;
<<<<<<< HEAD
=======

>>>>>>> d4af47f8b7962328f603dbad791bea9ec04b1db5
public static class ValidatorExtension
{
    public static async Task EnsureValidatedAsync<TModel>(this IValidator<TModel> validator, TModel model)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
            throw new ArgumentIsNotValidException(validationResult.Errors?.FirstOrDefault()?.ErrorMessage);
    }
<<<<<<< HEAD
}

=======
}
>>>>>>> d4af47f8b7962328f603dbad791bea9ec04b1db5
