using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.StudentDetails;

public class StudentDetailService : IStudentDetailService
{
    private readonly IRepository<Student> studentRepository;
    private readonly IRepository<StudentDetail> studentDetailRepository;

    public StudentDetailService()
    {
        studentRepository = new Repository<Student>();
        studentDetailRepository = new Repository<StudentDetail>();
    }

    public async Task CreateAsync(StudentDetailCreateModel model)
    {
        var student = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException("Student not found");

        var existDetail = await studentDetailRepository
            .SelectAllAsQueryable()
            .FirstOrDefaultAsync(d => d.StudentId == model.StudentId && !d.IsDeleted);

        if (existDetail is not null)
            throw new AlreadyExistException("Student details already exist");

        var detail = new StudentDetail
        {
            StudentId = model.StudentId,
            FatherFirstName = model.FatherFirstName,
            FatherLastName = model.FatherLastName,
            FatherPhone = model.FatherPhone,
            MotherFirstName = model.MotherFirstName,
            MotherLastName = model.MotherLastName,
            MotherPhone = model.MotherPhone,
            RelativeFirstName = model.RelativeFirstName,
            RelativeLastName = model.RelativeLastName,
            RelativePhone = model.RelativePhone
        };

        await studentDetailRepository.InsertAsync(detail);
    }

    public async Task UpdateAsync(int studentId, StudentDetailUpdateModel model)
    {
        _ = await studentRepository.SelectAsync(studentId)
            ?? throw new NotFoundException("Student not found");

        var existDetail = await studentDetailRepository
            .SelectAllAsQueryable()
            .FirstOrDefaultAsync(d => d.StudentId == studentId && !d.IsDeleted)
            ?? throw new NotFoundException("Student details not found");

        existDetail.FatherFirstName = model.FatherFirstName;
        existDetail.FatherLastName = model.FatherLastName;
        existDetail.FatherPhone = model.FatherPhone;
        existDetail.MotherFirstName = model.MotherFirstName;
        existDetail.MotherLastName = model.MotherLastName;
        existDetail.MotherPhone = model.MotherPhone;
        existDetail.RelativeFirstName = model.RelativeFirstName;
        existDetail.RelativeLastName = model.RelativeLastName;
        existDetail.RelativePhone = model.RelativePhone;

        await studentDetailRepository.UpdateAsync(existDetail);
    }

    public async Task DeleteAsync(int studentId)
    {
        _ = await studentRepository.SelectAsync(studentId)
            ?? throw new NotFoundException("Student not found");

        var existDetail = await studentDetailRepository
            .SelectAllAsQueryable()
            .FirstOrDefaultAsync(d => d.StudentId == studentId && !d.IsDeleted)
            ?? throw new NotFoundException("Student details not found");

        await studentDetailRepository.DeleteAsync(existDetail);
    }

    public async Task<StudentDetailViewModel> GetByStudentIdAsync(int studentId)
    {
        _ = await studentRepository.SelectAsync(studentId)
            ?? throw new NotFoundException("Student not found");

        var detail = await studentDetailRepository
            .SelectAllAsQueryable()
            .FirstOrDefaultAsync(d => d.StudentId == studentId && !d.IsDeleted)
            ?? throw new NotFoundException("Student details not found");

        return new StudentDetailViewModel
        {
            StudentId = detail.StudentId,
            FatherFirstName = detail.FatherFirstName,
            FatherLastName = detail.FatherLastName,
            FatherPhone = detail.FatherPhone,
            MotherFirstName = detail.MotherFirstName,
            MotherLastName = detail.MotherLastName,
            MotherPhone = detail.MotherPhone,
            RelativeFirstName = detail.RelativeFirstName,
            RelativeLastName = detail.RelativeLastName,
            RelativePhone = detail.RelativePhone
        };
    }
    // Student borligini tekshiramiz
    public async Task<List<StudentDetailViewModel>> GetAllAsync()
    {
        var details = await studentDetailRepository
            .SelectAllAsQueryable()
            .Include(d => d.Student)
            .Where(d => !d.IsDeleted && !d.Student.IsDeleted)
            .Select(d => new StudentDetailViewModel
            {
                StudentId = d.StudentId,
                FatherFirstName = d.FatherFirstName,
                FatherLastName = d.FatherLastName,
                FatherPhone = d.FatherPhone,
                MotherFirstName = d.MotherFirstName,
                MotherLastName = d.MotherLastName,
                MotherPhone = d.MotherPhone,
                RelativeFirstName = d.RelativeFirstName,
                RelativeLastName = d.RelativeLastName,
                RelativePhone = d.RelativePhone
            })
            .ToListAsync();

        return details;
    }
}
