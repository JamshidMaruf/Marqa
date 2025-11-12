using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Subjects.Models;

namespace Marqa.Service.Validators.Subjects;
public class TeacherSubjectCreateModelValidator : AbstractValidator<TeacherSubjectCreateModel>
{
    public TeacherSubjectCreateModelValidator()
    {
        RuleFor(x => x.SubjectId).GreaterThan(0);
        RuleFor(x => x.TeacherId).GreaterThan(0);
    }
}
