using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.TeacherAssessments.Models;

namespace Marqa.Service.Services.TeacherAssessments;

public class TeacherAssessmentService(
    IUnitOfWork unitOfWork,
    IValidator<TeacherAssessmentCreateModel> validator) : ITeacherAssessmentService
{
    public async Task CreateAsync(TeacherAssessmentCreateModel model)
    {
        await validator.ValidateAndThrowAsync(model);

        var teacherExists = await unitOfWork.Teachers.CheckExistAsync(t => t.Id == model.TeacherId);
        if (!teacherExists)
            throw new NotFoundException($"Teacher with ID {model.TeacherId} not found");

        var studentExists = await unitOfWork.Students.CheckExistAsync(s => s.Id == model.StudentId);
        if (!studentExists)
            throw new NotFoundException($"Student with ID {model.StudentId} not found");

        var courseExists = await unitOfWork.Courses.CheckExistAsync(c => c.Id == model.CourseId);
        if (!courseExists)
            throw new NotFoundException($"Course with ID {model.CourseId} not found");

        var assessment = new TeacherAssessment
        {
            TeacherId = model.TeacherId,
            StudentId = model.StudentId,
            CourseId = model.CourseId,
            Rate = model.Rating,
            Description = model.Description,
            SubmittedDateTime = DateTime.UtcNow
        };

        unitOfWork.TeacherAssessments.Insert(assessment);
        await unitOfWork.SaveAsync();
    }
}