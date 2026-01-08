using System.Text.RegularExpressions;
using FluentValidation;
using Marqa.Service.Services.Permissions.Models;

namespace Marqa.Service.Validators.Permissions;

public class PermissionCreateValidator : AbstractValidator<PermissionCreateModel>
{
    private static readonly string[] _validActions = { "Create", "Read", "Update", "Delete", "View", "Manage", "Execute", "Export", "Import" };

    public PermissionCreateValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty();

        RuleFor(p => p.Module)
            .NotEmpty();

        RuleFor(p => p.Action)
            .NotEmpty().WithMessage("Action is required")
            .Must(BeValidAction).WithMessage($"Action must be one of: {string.Join(", ", _validActions)}");
    }

    private bool BeValidAction(string action)
    {
        return !string.IsNullOrWhiteSpace(action) &&
               _validActions.Contains(action, StringComparer.OrdinalIgnoreCase);
    }
}
