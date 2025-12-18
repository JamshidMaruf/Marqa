using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Subjects.Models;

namespace Marqa.Service.Validators.Subjects;
public class SubjectCreateModelValidator : AbstractValidator<SubjectCreateModel>
{
    public SubjectCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.CompanyId).GreaterThan(0);
        RuleFor(x => x.CompanyId).MustAsync(async (companyId, cancellation) => await unitOfWork.Companies.CheckExistAsync(p => p.Id == companyId))
            .WithMessage("Company not found");
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name cannot be empty.")
           .MaximumLength(255).WithMessage("Name length must not exceed word limit.");
    }
}
