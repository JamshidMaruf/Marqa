using FluentValidation;
using Marqa.Service.Exceptions;

namespace Marqa.Service.Extensions;

public static class ValidatorExtension
{
    /// <summary>
    /// The method validates the provided model using the specified FluentValidation validator.
    /// If the model fails validation, a ValidateException is thrown.
    /// </summary>
    /// <typeparam name="TModel">
    /// The type of the model to be validated.
    /// </typeparam>
    /// <param name="validator">
    /// The object of the FluentValidation validator to be used for validation.
    /// </param>
    /// <param name="model">
    /// The model instance to be validated.
    /// </param>
    /// <returns>
    /// Async Task representing the validation operation.
    /// </returns>
    /// <exception cref="ValidateException">
    /// If the model fails validation,
    /// The method throws a ValidateException containing the validation error messages.
    /// </exception>
    public static async Task EnsureValidatedAsync<TModel>(this IValidator<TModel> validator, TModel model)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
            throw new ValidateException(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
    }
}