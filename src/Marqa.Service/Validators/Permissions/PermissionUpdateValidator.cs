using System.Text.RegularExpressions;
using FluentValidation;
using Marqa.Service.Services.Permissions.Models;

namespace Marqa.Service.Validators.Permissions;
public class PermissionUpdateValidator : AbstractValidator<PermissionUpdateModel>
{
    private static readonly string[] ValidActions = { "Create", "Read", "Update", "Delete", "View", "Manage", "Execute", "Export", "Import" };

    public PermissionUpdateValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty();
        ;

        RuleFor(p => p.Module)
            .NotEmpty();

        RuleFor(p => p.Action)
            .NotEmpty().WithMessage("Action is required")
            .Must(BeValidAction).WithMessage($"Action must be one of: {string.Join(", ", ValidActions)}");
    }

    private bool BeValidAction(string action)
    {
        return !string.IsNullOrWhiteSpace(action) &&
               ValidActions.Contains(action, StringComparer.OrdinalIgnoreCase);
    }
}
