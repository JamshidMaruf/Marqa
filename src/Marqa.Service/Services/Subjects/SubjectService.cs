using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Subjects.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Subjects;

public class SubjectService : ISubjectService
{
    private readonly IRepository<Subject> subjectRepository;
    public SubjectService()
    {
        subjectRepository = new Repository<Subject>();
    }

    public async Task CreateAsync(SubjectCreateModel model)
    {
        var alreadyExistSubject = await subjectRepository
            .SelectAllAsQueryable()
            .Where(s => !s.IsDeleted)
            .FirstOrDefaultAsync(s => s.Name == model.Name && s.CompanyId == model.CompanyId);

        if (alreadyExistSubject != null)
            throw new AlreadyExistException("This subject already exist!");

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
            .FirstOrDefaultAsync(s => s.Id == id && s.CompanyId == model.CompanyId)
            ?? throw new NotFoundException("Subjet is not found");

        existSubject.Name = model.Name;

        await subjectRepository.UpdateAsync(existSubject);
    }

    public async Task DeleteAsync(int id)
    {
        var existSubject = await subjectRepository.SelectAsync(id)
            ?? throw new NotFoundException("Subjet is not found");

        await subjectRepository.DeleteAsync(existSubject);
    }

    public async Task<SubjectViewModel> GetAsync(int id)
    {
        var existSubject = await subjectRepository.SelectAsync(id)
            ?? throw new Exception("Subject not found");

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
            .Where(s => s.CompanyId == companyId && !s.IsDeleted)
            .Select(s => new SubjectViewModel
            {
                Id = s.Id,
                Name = s.Name,
            })
            .ToListAsync();
    }
}