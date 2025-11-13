using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Subjects.Models;

namespace Marqa.Service.Validators.Subjects;
public class SubjectUpdateModelValidator : AbstractValidator<SubjectUpdateModel>
{
    public SubjectUpdateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100).WithMessage("Name length must not exceed word limit.");
    }
}
