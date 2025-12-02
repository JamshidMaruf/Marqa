using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Employees.Models;

namespace Marqa.Service.Validators.Employees;
public class EmployeeCreateModelValidator : AbstractValidator<EmployeeCreateModel>
{
    public EmployeeCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("CompanyId is required.");
        RuleFor(x => x.CompanyId)
            .Must(companyId => unitOfWork.Companies.CheckExist(c => c.Id == companyId))
            .WithMessage("Company with given Id does not exist.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(255).WithMessage("First name must be at most 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(255).WithMessage("Last name must be at most 50 characters.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of Birth must be in the past.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+998\d{9}$")
            .WithMessage("Phone number is invalid format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender value.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value.");

        RuleFor(x => x.JoiningDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Joining date cannot be in the future.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId is required.");
        RuleFor(x => x.RoleId).Must(roleId => unitOfWork.EmployeeRoles.CheckExist(r => r.Id == roleId))
            .WithMessage("Employee role with given Id does not exist.");
    }
}
