using Marqa.Service.Services.TeacherAssessments.Models;

namespace Marqa.Service.Services.TeacherAssessments;

public interface ITeacherAssessmentService
{
    Task CreateAsync(TeacherAssessmentCreateModel model);
}