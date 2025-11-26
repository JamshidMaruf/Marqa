using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Subjects.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Subjects;

public class SubjectService(IUnitOfWork unitOfWork,
    IValidator<SubjectCreateModel> subjectCreateValidator,
    IValidator<SubjectUpdateModel> subjectUpdateValidator) : ISubjectService
{

    public async Task CreateAsync(SubjectCreateModel model)
    {
        await subjectCreateValidator.EnsureValidatedAsync(model);

        var alreadyExistSubject = await unitOfWork.Subjects
            .SelectAsync(s => s.Name == model.Name && s.CompanyId == model.CompanyId);

        if (alreadyExistSubject != null)
            throw new AlreadyExistException("This subject already exist!");

        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
            ?? throw new NotFoundException($"No company was found with ID = {model.CompanyId}");

        unitOfWork.Subjects.Insert(new Subject
        {
            Name = model.Name,
            CompanyId = model.CompanyId
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, SubjectUpdateModel model)
    {
        await subjectUpdateValidator.EnsureValidatedAsync(model);

        var existSubject = await unitOfWork.Subjects.SelectAsync(s => s.Id == id)
            ?? throw new NotFoundException("Subjet was not found");

        existSubject.Name = model.Name;

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existSubject = await unitOfWork.Subjects.SelectAsync(s => s.Id == id)
            ?? throw new NotFoundException("Subjet was not found");

        unitOfWork.Subjects.MarkAsDeleted(existSubject);

        await unitOfWork.SaveAsync();
    }

    public async Task<SubjectViewModel> GetAsync(int id)
    {
        var existSubject = await unitOfWork.Subjects.SelectAsync(
            predicate: s => s.Id == id && !s.IsDeleted,
            includes: "Company")
              ?? throw new NotFoundException("Subject was not found");

        return new SubjectViewModel
        {
            Id = existSubject.Id,
            Name = existSubject.Name,
            CompanyId = existSubject.CompanyId,
            CompanyName = existSubject.Company.Name
        };
    }

    public async Task<List<SubjectViewModel>> GetAllAsync(int companyId)
    {
        return await unitOfWork.Subjects.SelectAllAsQueryable(
            predicate: s => s.CompanyId == companyId && !s.IsDeleted,
            includes: "Company")
            .Select(s => new SubjectViewModel
            {
                Id = s.Id,
                Name = s.Name,
                CompanyId = s.CompanyId,
                CompanyName = s.Company.Name,
            })
            .ToListAsync();
    }

    public async Task AttachAsync(int teacherId, int subjectId)
    {
        _ = await unitOfWork.Employees.SelectAsync(e => e.Id == teacherId)
            ?? throw new NotFoundException($"No teacher was found with ID = {teacherId}.");

        _ = await unitOfWork.Subjects.SelectAsync(s => s.Id == subjectId)
            ?? throw new NotFoundException($"No subject was found with ID = {subjectId}.");

        var exitAttachment = await unitOfWork.TeacherSubjects.SelectAsync(a =>
            a.TeacherId == teacherId &&
            a.SubjectId == subjectId);

        if (exitAttachment != null)
            throw new AlreadyExistException("The teacher has this subject already!");

        unitOfWork.TeacherSubjects.Insert(new TeacherSubject
        {
            TeacherId = teacherId,
            SubjectId = subjectId
        });

        await unitOfWork.SaveAsync();
    }


    public async Task BulkAttachAsync(int teacherId, List<int> subjectIds)
    {
        var teacher = await unitOfWork.Employees.SelectAsync(e => e.Id == teacherId && e.Role.CanTeach)
            ?? throw new NotFoundException($"No teacher was found with ID = {teacherId}.");

        List<int> existSubjects = await unitOfWork.Subjects
            .SelectAllAsQueryable(s => s.CompanyId == teacher.User.CompanyId && !s.IsDeleted)
            .Select(s => s.Id)
            .Distinct()
            .ToListAsync();

        for (int i = 0; i < subjectIds.Count; i++)
        {
            if (!existSubjects.Contains(subjectIds[i]))
                throw new NotFoundException($"There is no subject with ID = {subjectIds[i]}");
        }

        var alreadyAttachedSubjects = await unitOfWork.TeacherSubjects
            .SelectAllAsQueryable(s => s.TeacherId == teacherId && !s.IsDeleted)
            .Select(s => new TeacherSubject
            { // i am not getting id property of this entity, as it might have been enough, because there is no corresponding column in table
                TeacherId = s.TeacherId,
                SubjectId = s.SubjectId
            })
            .ToListAsync();

        foreach (var subject in alreadyAttachedSubjects)
        {
            unitOfWork.TeacherSubjects.Remove(subject);
        }

        List<TeacherSubject> teacherSubjects = new List<TeacherSubject>();
        foreach (var subjectId in subjectIds)
        {
            teacherSubjects.Add(new TeacherSubject
            {
                TeacherId = teacherId,
                SubjectId = subjectId
            });
        }

        await unitOfWork.TeacherSubjects.InsertRangeAsync(teacherSubjects);
        await unitOfWork.SaveAsync();
    }

    public async Task DetachAsync(int teacherId, int subjectId)
    {
        var teacherSubject = await unitOfWork.TeacherSubjects
            .SelectAsync(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId)
            ?? throw new NotFoundException($"No attachment was found with teacherID: {teacherId} and subjectID: {subjectId}.");

        unitOfWork.TeacherSubjects.MarkAsDeleted(teacherSubject);

        await unitOfWork.SaveAsync();
    }
}
