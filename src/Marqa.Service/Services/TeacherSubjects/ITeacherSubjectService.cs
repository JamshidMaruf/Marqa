using Marqa.Service.Services.TeacherSubjects.Models;

namespace Marqa.Service.Services.TeacherSubjects;
public interface ITeacherSubjectService
{
    Task CreateAsync(TeacherSubjectCreateModel model);
    Task UpdateAsync(int id, int subjectId);
}
