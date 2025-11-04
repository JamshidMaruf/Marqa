using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Subjects.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Subjects;

public class SubjectService(
    IUnitOfWork unitOfWork) : ISubjectService
{
    public async Task CreateAsync(SubjectCreateModel model)
    {
        var alreadyExistSubject = await unitOfWork.Subjects
            .SelectAllAsQueryable()
            .Where(s => !s.IsDeleted)
            .FirstOrDefaultAsync(s => s.Name == model.Name && s.CompanyId == model.CompanyId);

        if (alreadyExistSubject != null)
            throw new AlreadyExistException("This subject already exist!");
       
        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
            ?? throw new NotFoundException($"No company was found with ID = {model.CompanyId}");

        unitOfWork.Subjects.Insert(new Subject
        {
            CompanyId = model.CompanyId,
            Name = model.Name,
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, SubjectUpdateModel model)
    {
        var existSubject = await unitOfWork.Subjects
            .SelectAllAsQueryable()
            .Where(s => !s.IsDeleted)
            .FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new NotFoundException("Subjet was not found");
       
        existSubject.Name = model.Name;

        unitOfWork.Subjects.Update(existSubject);

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
        var existSubject =  await unitOfWork.Subjects
            .SelectAllAsQueryable()
            .Include(s => s.Company)
            .Where(s => s.Id == id && !s.IsDeleted)
            .Select(s => new SubjectViewModel
            {
                Id = s.Id,
                CompanyId = s.CompanyId,
                CompanyName = s.Company.Name,
                Name = s.Name,
            }).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Subject was not found");

        return new SubjectViewModel
        {
            Id = existSubject.Id,
            Name = existSubject.Name,
            CompanyId = existSubject.CompanyId,
            CompanyName = existSubject.CompanyName
        };
    }

    public async Task<List<SubjectViewModel>> GetAllAsync(int companyId)
    {
        return await unitOfWork.Subjects
            .SelectAllAsQueryable()
            .Include(s => s.Company)
            .Where(s => s.CompanyId == companyId && !s.IsDeleted)
            .Select(s => new SubjectViewModel
            {
                Id = s.Id,
                CompanyId = s.CompanyId,
                CompanyName = s.Company.Name,
                Name = s.Name,
            })
            .ToListAsync();
    }

    public async Task AttachAsync(TeacherSubjectCreateModel model)
    {
        _ = await unitOfWork.Employees.SelectAsync(e => e.Id == model.TeacherId)
            ?? throw new NotFoundException($"No teacher was found with ID = {model.TeacherId}.");

        _ = await unitOfWork.Subjects.SelectAsync(s => s.Id == model.SubjectId)
            ?? throw new NotFoundException($"No subject was found with ID = {model.SubjectId}.");

        unitOfWork.TeacherSubjects.Insert(new TeacherSubject
        {
            TeacherId = model.TeacherId,
            SubjectId = model.SubjectId
        });

        await unitOfWork.SaveAsync();
    }

    public async Task DetachAsync(int teacherId, int subjectId)
    {
        var teacherSubject = unitOfWork.TeacherSubjects
            .SelectAllAsQueryable()
            .Where(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId) 
            .FirstOrDefault()
            ?? throw new NotFoundException($"No attachment was found with teacherID: {teacherId} and subjectID: {subjectId}.");

        unitOfWork.TeacherSubjects.MarkAsDeleted(teacherSubject);

        await unitOfWork.SaveAsync();
    }
}