using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Students.Models;
using Marqa.Service.Services.Students.Models.DetailModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Students;

public class StudentService(
    IUnitOfWork unitOfWork,
    IValidator<StudentCreateModel> createValidator,
    IValidator<StudentUpdateModel> updateValidator) : IStudentService
{
    public async Task CreateAsync(StudentCreateModel model)
    {
        await createValidator.EnsureValidatedAsync(model);

        var existingStudent = await unitOfWork.Students
            .CheckExistAsync(e => e.User.Phone == model.Phone && e.User.CompanyId == model.CompanyId);

        if (existingStudent)
            throw new AlreadyExistException($"Student with this phone {model.Phone} already exists");

        var studentPhoneResult = model.Phone.TrimPhoneNumber();
        if (!studentPhoneResult.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        var fatherPhoneResult = model.StudentDetailCreateModel.FatherPhone.TrimPhoneNumber();
        var motherPhoneResult = model.StudentDetailCreateModel.MotherPhone.TrimPhoneNumber();
        var guardianPhoneResult = model.StudentDetailCreateModel.GuardianPhone.TrimPhoneNumber();

        bool anyParentPhoneValid =
            fatherPhoneResult.IsSuccessful ||
            motherPhoneResult.IsSuccessful ||
            guardianPhoneResult.IsSuccessful;

        if (!anyParentPhoneValid)
            throw new ArgumentIsNotValidException("At least one parent phone number must be valid");

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var user = unitOfWork.Users.Insert(new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = studentPhoneResult.Phone,
                Email = model.Email,
                Role = UserRole.Student,
                CompanyId = model.CompanyId
            });

            await unitOfWork.SaveAsync();

            var createdStudent = new Student()
            {
                UserId = user.Id,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender
            };

            unitOfWork.Students.Insert(createdStudent);
            await unitOfWork.SaveAsync();

            unitOfWork.StudentDetails.Insert(new StudentDetail
            {
                StudentId = createdStudent.Id,
                FatherFirstName = model.StudentDetailCreateModel.FatherFirstName,
                FatherLastName = model.StudentDetailCreateModel.FatherLastName,
                FatherPhone = fatherPhoneResult.IsSuccessful ? fatherPhoneResult.Phone : null,
                MotherFirstName = model.StudentDetailCreateModel.MotherFirstName,
                MotherLastName = model.StudentDetailCreateModel.MotherLastName,
                MotherPhone = motherPhoneResult.IsSuccessful ? motherPhoneResult.Phone : null,
                GuardianFirstName = model.StudentDetailCreateModel.GuardianFirstName,
                GuardianLastName = model.StudentDetailCreateModel.GuardianLastName,
                GuardianPhone = guardianPhoneResult.IsSuccessful ? guardianPhoneResult.Phone : null,
                CompanyId = model.CompanyId
            });

            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(int id, StudentUpdateModel model)
    {
        await updateValidator.EnsureValidatedAsync(model);

        var studentPhoneResult = model.Phone.TrimPhoneNumber();
        if (!studentPhoneResult.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        var fatherPhoneResult = model.StudentDetailUpdateModel.FatherPhone.TrimPhoneNumber();
        var motherPhoneResult = model.StudentDetailUpdateModel.MotherPhone.TrimPhoneNumber();
        var guardianPhoneResult = model.StudentDetailUpdateModel.GuardianPhone.TrimPhoneNumber();

        bool anyParentPhoneValid =
            fatherPhoneResult.IsSuccessful ||
            motherPhoneResult.IsSuccessful ||
            guardianPhoneResult.IsSuccessful;

        if (!anyParentPhoneValid)
            throw new ArgumentIsNotValidException("At least one parent phone number must be valid");

        var existStudent = await unitOfWork.Students
            .SelectAsync(
                predicate: s => s.Id == id,
                includes: ["StudentDetail", "User"])
            ?? throw new NotFoundException($"Student is not found");

        var phoneExists = await unitOfWork.Students
            .CheckExistAsync(e => e.User.Phone == model.Phone
                          && e.User.CompanyId == existStudent.User.CompanyId
                          && e.Id != id);

        if (phoneExists)
            throw new AlreadyExistException($"Student with this phone {model.Phone} already exists");


        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            existStudent.User.FirstName = model.FirstName;
            existStudent.User.LastName = model.LastName;
            existStudent.Gender = model.Gender;
            existStudent.DateOfBirth = model.DateOfBirth;
            existStudent.User.Phone = studentPhoneResult.Phone;
            existStudent.User.Email = model.Email;

            if (existStudent.StudentDetail != null)
            {
                existStudent.StudentDetail.FatherFirstName = model.StudentDetailUpdateModel.FatherFirstName;
                existStudent.StudentDetail.FatherLastName = model.StudentDetailUpdateModel.FatherLastName;
                existStudent.StudentDetail.FatherPhone = fatherPhoneResult.Phone;
                existStudent.StudentDetail.MotherFirstName = model.StudentDetailUpdateModel.MotherFirstName;
                existStudent.StudentDetail.MotherLastName = model.StudentDetailUpdateModel.MotherLastName;
                existStudent.StudentDetail.MotherPhone = motherPhoneResult.Phone;
                existStudent.StudentDetail.GuardianFirstName = model.StudentDetailUpdateModel.GuardianFirstName;
                existStudent.StudentDetail.GuardianLastName = model.StudentDetailUpdateModel.GuardianLastName;
                existStudent.StudentDetail.GuardianPhone = guardianPhoneResult.Phone;
            }

            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(
                predicate: s => s.Id == id,
                includes: "StudentDetail")
            ?? throw new NotFoundException($"Student is not found");

        if (existStudent.StudentDetail != null)
        {
            unitOfWork.StudentDetails.MarkAsDeleted(existStudent.StudentDetail);
        }

        unitOfWork.Students.MarkAsDeleted(existStudent);
        await unitOfWork.SaveAsync();
    }

    public async Task<int> GetByPhoneAsync(string phone)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.User.Phone == phone)
            ?? throw new NotFoundException($"Student with phone {phone} not found");

        return student.Id;
    }

    public async Task<int> GetStudentParentByPhoneAsync(string phone)
    {
        var studentDetail = await unitOfWork.StudentDetails
            .SelectAsync(sd => sd.FatherPhone == phone ||
                        sd.MotherPhone == phone ||
                        sd.GuardianPhone == phone)
            ?? throw new NotFoundException($"Student parent with phone {phone} not found");

        return studentDetail.StudentId;
    }

    public async Task<StudentViewModel> GetAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(
                predicate: t => t.Id == id,
                includes: ["StudentDetail", "User"])
            ?? throw new NotFoundException($"Student is not found");

        if (existStudent.StudentDetail == null)
            throw new NotFoundException("Student details not found");

        return new StudentViewModel
        {
            Id = existStudent.Id,
            FirstName = existStudent.User.FirstName,
            LastName = existStudent.User.LastName,
            DateOfBirth = existStudent.DateOfBirth,
            Gender = existStudent.Gender,
            Phone = existStudent.User.Phone,
            Email = existStudent.User.Email,
            Detail = new StudentDetailViewModel
            {
                StudentId = existStudent.Id,
                FatherFirstName = existStudent.StudentDetail.FatherFirstName,
                FatherLastName = existStudent.StudentDetail.FatherLastName,
                FatherPhone = existStudent.StudentDetail.FatherPhone,
                MotherFirstName = existStudent.StudentDetail.MotherFirstName,
                MotherLastName = existStudent.StudentDetail.MotherLastName,
                MotherPhone = existStudent.StudentDetail.MotherPhone,
                RelativeFirstName = existStudent.StudentDetail.GuardianFirstName,
                RelativeLastName = existStudent.StudentDetail.GuardianLastName,
                RelativePhone = existStudent.StudentDetail.GuardianPhone
            }
        };
    }

    public async Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId)
    {
        var students = await unitOfWork.Courses
            .SelectAllAsQueryable(c => c.Id == courseId && !c.IsDeleted)
            .SelectMany(c => c.Enrollments)
            .Select(sc => new StudentViewModel
            {
                Id = sc.Student.Id,
                FirstName = sc.Student.User.FirstName,
                LastName = sc.Student.User.LastName,
                DateOfBirth = sc.Student.DateOfBirth,
                Gender = sc.Student.Gender,
                Phone = sc.Student.User.Phone,
                Email = sc.Student.User.Email,
                Detail = new StudentDetailViewModel
                {
                    StudentId = sc.Student.Id,
                    FatherFirstName = sc.Student.StudentDetail.FatherFirstName,
                    FatherLastName = sc.Student.StudentDetail.FatherLastName,
                    FatherPhone = sc.Student.StudentDetail.FatherPhone,
                    MotherFirstName = sc.Student.StudentDetail.MotherFirstName,
                    MotherLastName = sc.Student.StudentDetail.MotherLastName,
                    MotherPhone = sc.Student.StudentDetail.MotherPhone,
                    RelativeFirstName = sc.Student.StudentDetail.GuardianFirstName,
                    RelativeLastName = sc.Student.StudentDetail.GuardianLastName,
                    RelativePhone = sc.Student.StudentDetail.GuardianPhone
                }
            })
            .ToListAsync();

        if (!students.Any())
            throw new NotFoundException("Course is not found or has no students");

        return students;
    }

    public async Task<string> UploadProfilePictureAsync(long studentId, IFormFile picture)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.Id == studentId)
           ?? throw new NotFoundException($"Student not found (ID: {studentId})");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(picture.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            throw new ArgumentIsNotValidException("Only image files are allowed (jpg, jpeg, png, webp)");

        if (picture.Length > 5 * 1024 * 1024)
            throw new ArgumentIsNotValidException("File size must not exceed 5MB");

        // if (!string.IsNullOrEmpty(student.ProfilePicture))
        // {
        //     var oldFilePath = Path.Combine("wwwroot", student.ProfilePicture.TrimStart('/'));
        //     if (File.Exists(oldFilePath))
        //         File.Delete(oldFilePath);
        // }

        var fileName = $"{studentId}_{Guid.NewGuid()}{extension}";
        var uploadsFolder = Path.Combine("wwwroot", "uploads", "students", "profiles");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await picture.CopyToAsync(stream);
        }
        var relativePath = $"/uploads/students/profiles/{fileName}";
        //student.ProfilePicture = relativePath;

        unitOfWork.Students.Update(student);
        await unitOfWork.SaveAsync();

        return relativePath;
    }

    public async Task UpdateStudentCourseStatusAsync(int studentId, int courseId, EnrollmentStatus status)
    {
        var studentCourse = await unitOfWork.Enrollments
            .SelectAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId)
            ?? throw new NotFoundException("Enrollments not found");

        studentCourse.Status = status;

        await unitOfWork.SaveAsync();
    }

    public async Task<List<StudentListModel>> GetAllAsync(StudentFilterModel filterModel)
    {
        var query = unitOfWork.Students
            .SelectAllAsQueryable(s => !s.IsDeleted);

        query = query.Where(s => s.User.CompanyId == filterModel.CompanyId);


        if (!string.IsNullOrEmpty(filterModel.SearchText))
        {
            var searchText = filterModel.SearchText.ToLower();
            query = query.Where(s => s.User.FirstName.ToLower().Contains(searchText) ||
                                     s.User.LastName.ToLower().Contains(searchText) ||
                                     s.User.Phone.ToLower().Contains(searchText) ||
                                     s.User.Email.ToLower().Contains(searchText));
        }


        if (filterModel.CourseId != null)
            query = query.Where(s => s.Courses.Any(sc => sc.CourseId == filterModel.CourseId));

        return await query.Select(x => new StudentListModel
        {
            Id = x.Id,
            Status = x.Status,
            Balance = x.Balance,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Phone = x.User.Phone,
            Courses = x.Courses.Select(c => new StudentListModel.StudentCourseData
            {
                CourseId = c.CourseId,
                CourseName = c.Course.Name,
                CourseStatus = Enum.GetName(c.Course.Status)
            }).ToList()
        }).ToListAsync();
    }

    public async Task<StudentViewForUpdateModel> GetForUpdateAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(
                predicate: t => t.Id == id,
                includes: ["StudentDetail", "User", "Courses"])
            ?? throw new NotFoundException($"Student is not found");

        if (existStudent.StudentDetail == null)
            throw new NotFoundException("Student details not found");

        return new StudentViewForUpdateModel
        {
            Id = existStudent.Id,
            FirstName = existStudent.User.FirstName,
            LastName = existStudent.User.LastName,
            DateOfBirth = existStudent.DateOfBirth,
            Gender = existStudent.Gender,
            Phone = existStudent.User.Phone,
            Email = existStudent.User.Email,
            FatherFirstName = existStudent.StudentDetail.FatherFirstName,
            FatherLastName = existStudent.StudentDetail.FatherLastName,
            MotherFirstName = existStudent.StudentDetail.MotherFirstName,
            MotherLastName = existStudent.StudentDetail.MotherLastName,
            FatherPhone = existStudent.StudentDetail.FatherPhone,
            MotherPhone = existStudent.StudentDetail.MotherPhone,
            GuardianFirstName = existStudent.StudentDetail.GuardianFirstName,
            GuardianLastName = existStudent.StudentDetail.GuardianLastName,
            GuardianPhone = existStudent.StudentDetail.GuardianPhone,
            Status = existStudent.Status,
            Courses = existStudent.Courses.Select(x => new StudentViewForUpdateModel.StudentCourseData
            {
                CourseId = x.CourseId,
                CourseName = x.Course.Name,
                CourseStatusId = ((int)x.Course.Status),
                CourseStatusName = Enum.GetName(x.Course.Status),
            }).ToList()
        };
    }
}
