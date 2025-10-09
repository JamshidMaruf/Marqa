using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Subjects.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Subjects;

public class SubjectService(
    IRepository<TeacherSubject> teacherSubjectRepository,
    IRepository<Employee> teacherRepository,
    IRepository<Subject> subjectRepository,
    IRepository<Company> companyRepository) : ISubjectService
{
    public async Task CreateAsync(SubjectCreateModel model)
    {
        var alreadyExistSubject = await subjectRepository
            .SelectAllAsQueryable()
            .Where(s => !s.IsDeleted)
            .FirstOrDefaultAsync(s => s.Name == model.Name && s.CompanyId == model.CompanyId);

        if (alreadyExistSubject != null)
            throw new AlreadyExistException("This subject already exist!");
       
        _ = await companyRepository.SelectAsync(model.CompanyId)
            ?? throw new NotFoundException($"No company was found with ID = {model.CompanyId}");

        await subjectRepository.InsertAsync(new Subject
        {
            CompanyId = model.CompanyId,
            Name = model.Name,
        });
    }

    public async Task UpdateAsync(int id, SubjectUpdateModel model)
    {
        var existSubject = await subjectRepository
            .SelectAllAsQueryable()
            .Where(s => !s.IsDeleted)
            .FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new NotFoundException("Subjet was not found");
       
        existSubject.Name = model.Name;

        await subjectRepository.UpdateAsync(existSubject);
    }

    public async Task DeleteAsync(int id)
    {
        var existSubject = await subjectRepository.SelectAsync(id)
            ?? throw new NotFoundException("Subjet was not found");

        await subjectRepository.DeleteAsync(existSubject);
    }

    public async Task<SubjectViewModel> GetAsync(int id)
    {
        var existSubject =  await subjectRepository
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
        };
    }

    public async Task<List<SubjectViewModel>> GetAllAsync(int companyId)
    {
        return await subjectRepository
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
        _ = await teacherRepository.SelectAsync(model.TeacherId)
            ?? throw new NotFoundException($"No teacher was found with ID = {model.TeacherId}.");

        _ = await subjectRepository.SelectAsync(model.SubjectId)
            ?? throw new NotFoundException($"No subject was found with ID = {model.SubjectId}.");

        await teacherSubjectRepository.InsertAsync(new TeacherSubject
        {
            TeacherId = model.TeacherId,
            SubjectId = model.SubjectId
        });
    }

    public async Task DetachAsync(int teacherId, int subjectId)
    {
        var teacherSubject = teacherSubjectRepository
            .SelectAllAsQueryable()
            .Where(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId) 
            .FirstOrDefault()
            ?? throw new NotFoundException($"No attachment was found with teacherID: {teacherId} and subjectID: {subjectId}.");

        await teacherSubjectRepository.DeleteAsync(teacherSubject);
    }
}