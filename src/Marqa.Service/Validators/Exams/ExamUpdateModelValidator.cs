using FluentValidation;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;
public class ExamUpdateModelValidator : AbstractValidator<ExamUpdateModel>
{
    public ExamUpdateModelValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("CourseId is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title must be at most 255 characters.");

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("Start time must be earlier than end time.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be greater than start time.");

        RuleFor(x => x.ExamSetting)
            .NotNull().WithMessage("Exam settings are required.")
            .SetValidator(new ExamSettingUpdateDataValidator());
    }
}

public class ExamSettingUpdateDataValidator : AbstractValidator<ExamUpdateModel.ExamSettingUpdateData>
{
    public ExamSettingUpdateDataValidator()
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
            .SetValidator(new ExamSettingItemUpdateDataValidator());
    }
}

public class ExamSettingItemUpdateDataValidator : AbstractValidator<ExamUpdateModel.ExamSettingItemUpdateData>
{
    public ExamSettingItemUpdateDataValidator()
    {
        RuleFor(x => x.Score)
            .GreaterThan(0).WithMessage("Score must be greater than 0.");

        RuleFor(x => x.GivenPoints)
            .GreaterThanOrEqualTo(0).WithMessage("GivenPoints must be non-negative.");
    }
}
