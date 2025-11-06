using FluentValidation;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;

public class ExamSettingDataValidator : AbstractValidator<ExamCreateModel.ExamSettingData>
{
    public ExamSettingDataValidator()
    {
        RuleFor(d => d.CertificateFileName).NotNull();
        RuleFor(t => t.Items).ForEach(item 
            => item.SetValidator(new ExamSettingItemDataValidator()));
    }   
}