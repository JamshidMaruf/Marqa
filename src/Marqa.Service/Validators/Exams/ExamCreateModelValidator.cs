using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;
public class ExamCreateModelValidator : AbstractValidator<ExamCreateModel>
{
    public ExamCreateModelValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("CourseId is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must be at most 200 characters.");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow.AddMinutes(-1)) // allow small margin
            .WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be greater than start time.");

        RuleFor(x => x.ExamSetting)
            .NotNull().WithMessage("Exam settings are required.")
            .SetValidator(new ExamSettingDataValidator());
    }
}

public class ExamSettingDataValidator : AbstractValidator<ExamCreateModel.ExamSettingData>
{
    public ExamSettingDataValidator()
    {
        RuleFor(x => x.MinScore)
            .GreaterThanOrEqualTo(0).WithMessage("MinScore must be non-negative.");

        RuleFor(x => x.MaxScore)
            .GreaterThan(x => x.MinScore)
            .WithMessage("MaxScore must be greater than MinScore.");

        When(x => x.IsGivenCertificate, () =>
        {
            RuleFor(x => x.CertificateFileName)
                .NotEmpty().WithMessage("CertificateFileName is required when certificate is given.");

            RuleFor(x => x.CertificateFileExtension)
                .NotEmpty().WithMessage("CertificateFileExtension is required when certificate is given.");

            RuleFor(x => x.CertificateFilePath)
                .NotEmpty().WithMessage("CertificateFilePath is required when certificate is given.");
        });

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Exam setting items are required.");

        RuleForEach(x => x.Items)
            .SetValidator(new ExamSettingItemDataValidator());
    }
}

public class ExamSettingItemDataValidator : AbstractValidator<ExamCreateModel.ExamSettingItemData>
{
    public ExamSettingItemDataValidator()
    {
        RuleFor(x => x.Score)
            .GreaterThan(0).WithMessage("Score must be greater than 0.");

        RuleFor(x => x.GivenPoints)
            .GreaterThanOrEqualTo(0).WithMessage("GivenPoints must be non-negative.");
    }
}
