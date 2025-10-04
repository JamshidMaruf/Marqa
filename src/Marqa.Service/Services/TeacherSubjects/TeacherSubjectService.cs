using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.TeacherSubjects.Models;

namespace Marqa.Service.Services.TeacherSubjects;

public class TeacherSubjectService(
    IRepository<TeacherSubject> teacherSubjectRepository,
    IRepository<Employee> teacherRepository,
    IRepository<Subject> subjectRepository)
    : ITeacherSubjectService
{
    public async Task CreateAsync(TeacherSubjectCreateModel model)
    {
        _ = await teacherRepository.SelectAsync(model.TeacherId)
            ?? throw new NotFoundException($"No teacher was found with ID = {model.TeacherId}.");

        _ = await subjectRepository.SelectAsync(model.SubjectId)
            ?? throw new NotFoundException($"No subject was found with ID = {model.SubjectId}.");

        await teacherSubjectRepository.InsertAsync(new TeacherSubject
        {
            TeacherId = model.TeacherId, SubjectId = model.SubjectId
        });
    }

    public async Task UpdateAsync(int id, int subjectId)
    {
        var teacherSubject = await teacherSubjectRepository.SelectAsync(id)
                             ?? throw new NotFoundException("Not Found");

        _ = await subjectRepository.SelectAsync(subjectId)
            ?? throw new NotFoundException($"No subject was found with ID = {subjectId}.");

        teacherSubject.SubjectId = subjectId;

        await teacherSubjectRepository.UpdateAsync(teacherSubject);
    }
}