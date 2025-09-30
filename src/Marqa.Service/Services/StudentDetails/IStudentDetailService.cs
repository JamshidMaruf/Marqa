using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.StudentDetails.Models;
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
        // Talaba mavjudligini tekshirish
        var student = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException("Student not found");

        // Agar studentda allaqachon detail bo‘lsa, qayta yaratmaslik
        var existDetail = await studentDetailRepository
            .SelectAllAsQueryable()
            .FirstOrDefaultAsync(d => d.StudentId == model.StudentId && !d.IsDeleted);

        if (existDetail is not null)
            throw new AlreadyExistsException("Student details already exist");

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
        // Student borligini tekshiramiz
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
            Id = detail.Id,
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
}
namespace Marqa.Service.Services.StudentDetails.Models;

namespace Marqa.Service.Services.StudentDetails.Models;
namespace Marqa.Service.Services.StudentDetails.Models;
