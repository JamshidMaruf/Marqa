using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Enums;
using Marqa.Service.Services.StudentPointHistories;
using Marqa.Service.Services.Students.Models;
using Marqa.Service.Services.Students.Models.DetailModels;
using Marqa.Shared.Models;
using Marqa.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Students;

public class StudentService(
    IUnitOfWork unitOfWork,
    IStudentPointHistoryService studentPointHistoryService,
    IFileService fileService,
    IEnumService enumService,
    IPaginationService paginationService,
    IValidator<StudentCreateModel> createValidator,
    IValidator<StudentUpdateModel> updateValidator) : IStudentService
{
    public async Task CreateAsync(StudentCreateModel model)
    {
        await createValidator.EnsureValidatedAsync(model);

        var existingStudent = await unitOfWork.Students
            .CheckExistAsync(e => e.User.Phone == model.Phone && e.CompanyId == model.CompanyId);

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
                Role = UserRole.Student
            });

            await unitOfWork.SaveAsync();

            var createdStudent = new Student()
            {
                UserId = user.Id,
                CompanyId = model.CompanyId,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Status = model.Status
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
                GuardianPhone = guardianPhoneResult.IsSuccessful ? guardianPhoneResult.Phone : null
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
                          && e.CompanyId == existStudent.CompanyId
                          && e.Id != id);

        if (phoneExists)
            throw new AlreadyExistException($"Student with this phone {model.Phone} already exists");


        var studentCourses = await unitOfWork.Enrollments
            .SelectAllAsQueryable(e => e.StudentId == existStudent.Id)
            .ToListAsync();


        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var studentCourse in studentCourses)
            {
                foreach (var course in model.Courses)
                {
                    if (studentCourse.CourseId == course.CourseId)
                    {
                        studentCourse.Status = (EnrollmentStatus)course.CourseStatusId;
                        unitOfWork.Enrollments.Update(studentCourse);
                    }
                }
            }

            await unitOfWork.SaveAsync();

            existStudent.User.FirstName = model.FirstName;
            existStudent.User.LastName = model.LastName;
            existStudent.Gender = model.Gender;
            existStudent.Status = model.Status;
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
                includes: ["StudentDetail", "User"])
            ?? throw new NotFoundException($"Student with ID {id} not found");

        bool hasActiveEnrollments = await unitOfWork.Enrollments
            .CheckExistAsync(e =>
                e.StudentId == id &&
                e.Status == EnrollmentStatus.Active);

        if (hasActiveEnrollments)
        {
            throw new RequestRefusedException("Student has active courses. " +
                "first detach the student from his courses, then delete!");
        }

        unitOfWork.StudentDetails.MarkAsDeleted(existStudent.StudentDetail);

        unitOfWork.Users.MarkAsDeleted(existStudent.User);

        unitOfWork.Students.MarkAsDeleted(existStudent);
        var studentEnrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(e => e.StudentId == id && !e.IsDeleted)
            .ToListAsync();

        foreach (var enrollment in studentEnrollments)
        {
            enrollment.Status = EnrollmentStatus.Dropped;
            unitOfWork.Enrollments.Update(enrollment);
        }

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
                includes: [
                    "StudentDetail",
                    "User",
                    "Enrollments.Course",
                    "Enrollments.Course.CourseTeachers.Teacher.User",
                    "PaymentOperations",
                    "PaymentOperations.Course",
                    "Enrollments",
                    "ExamResults",
                    "PointHistories"])
            ?? throw new NotFoundException($"Student is not found");

        return new StudentViewModel
        {
            Id = existStudent.Id,
            FirstName = existStudent.User.FirstName,
            LastName = existStudent.User.LastName,
            DateOfBirth = existStudent.DateOfBirth,
            Gender = existStudent.Gender,
            Phone = existStudent.User.Phone,
            Email = existStudent.User.Email,
            Balance = existStudent.Balance,
            Status = existStudent.Status,
            TotalPoints = await studentPointHistoryService.GetAsync(existStudent.Id),
            Detail = new StudentDetailViewModel
            {
                StudentId = existStudent.Id,
                FatherFirstName = existStudent.StudentDetail.FatherFirstName,
                FatherLastName = existStudent.StudentDetail.FatherLastName,
                FatherPhone = existStudent.StudentDetail.FatherPhone,
                MotherFirstName = existStudent.StudentDetail.MotherFirstName,
                MotherLastName = existStudent.StudentDetail.MotherLastName,
                MotherPhone = existStudent.StudentDetail.MotherPhone,
                GuardianFirstName = existStudent.StudentDetail.GuardianFirstName,
                GuardianLastName = existStudent.StudentDetail.GuardianLastName,
                GuardianPhone = existStudent.StudentDetail.GuardianPhone
            },
            Courses = existStudent.Enrollments.Select(c => new StudentCourseViewData
            {
                CourseName = c.Course.Name,
                Subject = c.Course.Subject,
                TeachersFullName = c.Course.CourseTeachers.Select(c => $"{c.Teacher.User.FirstName} {c.Teacher.User.LastName}"),
                CourseStatusName = Enum.GetName(c.Status),
                CourseLevel = c.Course.Level
            })
            .ToList(),
            ExamResults = existStudent.ExamResults.Select(r => new StudentExamResultData
            {
                Title = r.Exam.Title,
                Score = r.Score
            })
            .ToList(),
            PaymentOperations = existStudent.PaymentOperations.Select(p => new StudentPaymentOperationData
            {
                PaymentNumber = p.PaymentNumber,
                PaymentMethod = p.PaymentMethod,
                Amount = p.Amount,
                DateTime = p.DateTime,
                Description = p.Description,
                PaymentOperationType = p.PaymentOperationType,
                CoursePrice = p.Course.Price
            })
            .ToList(),
            PointHistories = existStudent.PointHistories.Select(p => new StudentPointHistoryData
            {
                CurrentPoint = p.CurrentPoint,
                GivenDate = p.GivenDateTime,
                GivenPoint = p.GivenPoint,
                Note = p.Note,
                Operation = p.Operation
            })
            .ToList()
        };
    }

    public async Task<StudentViewForUpdateModel> GetForUpdateAsync(int id)
    {
        var existStudent = await unitOfWork.Students
            .SelectAsync(
                predicate: t => t.Id == id,
                includes: ["StudentDetail", "User", "Enrollments"])
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
            Courses = existStudent.Enrollments.Select(x => new StudentViewForUpdateModel.StudentCourseData
            {
                CourseId = x.CourseId,
                CourseName = x.Course.Name,
                CourseStatusId = ((int)x.Course.Status),
                CourseStatusName = enumService.GetEnumDescription(x.Course.Status)
            }).ToList()
        };
    }

    public async Task<List<StudentListModel>> GetAllAsync(
        PaginationParams @params,
        int companyId,
        string search = null,
        int? courseId = null,
        StudentFilteringStatus? status = null)
    {
        var query = unitOfWork.Students
            .SelectAllAsQueryable(
                predicate: s => s.CompanyId == companyId,
                includes: ["User", "Enrollments", "Enrollments.Course"]);

        if (!string.IsNullOrEmpty(search))
        {
            var searchText = search.ToLower();
            query = query.Where(s =>
                s.User.FirstName.ToLower().Contains(searchText) ||
                s.User.LastName.ToLower().Contains(searchText) ||
                s.User.Phone.ToLower().Contains(searchText) ||
                s.User.Email.ToLower().Contains(searchText));
        }

        if (courseId.HasValue)
            query = query.Where(s => s.Enrollments.Any(sc => sc.CourseId == courseId));

        if (status.HasValue)
        {
            if (status == StudentFilteringStatus.Active)
            {
                query = query.Where(s => s.Status == StudentStatus.Active);
            }
            else if (status == StudentFilteringStatus.Completed)
            {
                query = query.Where(s => s.Status == StudentStatus.Completed);
            }
            else if (status == StudentFilteringStatus.Frozen)
            {
                return await unitOfWork.Enrollments
                    .SelectAllAsQueryable(e => e.Status == EnrollmentStatus.Frozen)
                    .Select(e => new StudentListModel
                    {
                        Id = e.Student.Id,
                        Status = e.Student.Status,
                        Balance = e.Student.Balance,
                        FirstName = e.Student.User.FirstName,
                        LastName = e.Student.User.LastName,
                        Phone = e.Student.User.Phone,
                        Courses = e.Student.Enrollments.Select(c => new StudentCourseListData
                        {
                            CourseId = c.CourseId,
                            CourseName = c.Course.Name,
                            CourseStatus = enumService.GetEnumDescription(c.Status)
                        }).ToList()
                    })
                    .ToListAsync();
            }
            else if (status == StudentFilteringStatus.Completed)
            {
                return await unitOfWork.Courses.SelectAllAsQueryable(c =>
                c.Status == CourseStatus.Completed)
                    .SelectMany(c => c.Enrollments.Select(e => new StudentListModel
                    {
                        Id = e.Student.Id,
                        Status = e.Student.Status,
                        Balance = e.Student.Balance,
                        FirstName = e.Student.User.FirstName,
                        LastName = e.Student.User.LastName,
                        Phone = e.Student.User.Phone,
                        Courses = e.Student.Enrollments.Select(c => new StudentCourseListData
                        {
                            CourseId = c.CourseId,
                            CourseName = c.Course.Name,
                            CourseStatus = enumService.GetEnumDescription(c.Status)
                        }).ToList()
                    }))
                    .ToListAsync();
            }
            else if (status == StudentFilteringStatus.GroupLess)
            {
                query = query.Where(s => s.Enrollments.Count == 0);
            }
        }

        var pagedStudents = await paginationService.Paginate(query, @params).ToListAsync();

        return pagedStudents.Select(x => new StudentListModel
        {
            Id = x.Id,
            Status = x.Status,
            Balance = x.Balance,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Phone = x.User.Phone,
            Courses = x.Enrollments.Select(c => new StudentCourseListData
            {
                CourseId = c.CourseId,
                CourseName = c.Course.Name,
                CourseStatus = enumService.GetEnumDescription(c.Status)
            }).ToList()
        }).ToList();
    }

    public async ValueTask<StudentsInfo> GetStudentsInfo(int companyid)
    {
        var courses = unitOfWork.Courses.SelectAllAsQueryable(c => c.CompanyId == companyid);

        var students = new List<Student>();
        //foreach (var course in courses)
        //{
        //    students.AddRange(unitOfWork.Students.SelectAllAsQueryable(s => s.Courses.Any(c => c.CourseId == course.Id)));
        //}

        students = students.Distinct().ToList();

        return new StudentsInfo
        {
            TotalStudents = students.Count,
            TotalDroppedStudents = students.Count(s => s.Status == StudentStatus.Dropped),
            TotalCompletedStudents = students.Count(s => s.Status == StudentStatus.Completed),
            TotalActiveStudents = students.Count(s => s.Status == StudentStatus.Active),
            TotalInactiveStudents = students.Count(s => s.Status == StudentStatus.Active)
        };
    }

    public async Task<List<StudentViewModel>> GetAllByCourseIdAsync(int courseId)
    {
        var students = await unitOfWork.Courses
            .SelectAllAsQueryable(c => c.Id == courseId)
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
                    GuardianFirstName = sc.Student.StudentDetail.GuardianFirstName,
                    GuardianLastName = sc.Student.StudentDetail.GuardianLastName,
                    GuardianPhone = sc.Student.StudentDetail.GuardianPhone
                }
            })
            .ToListAsync();

        if (!students.Any())
            throw new NotFoundException("Course is not found or has no students");

        return students;
    }

    public async Task UploadProfilePictureAsync(int studentId, IFormFile picture)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.Id == studentId,
            includes: "Asset")
            ?? throw new NotFoundException("Student was not found!");

        if (!fileService.IsImageExtension(Path.GetExtension(picture.FileName)))
        {
            throw new ArgumentIsNotValidException("file format is not valid! Load only image file!");
        }

        var fileData = await fileService.UploadAsync(picture, "images/students");

        student.Asset.FileName = fileData.FileName;
        student.Asset.FilePath = fileData.FilePath;
        student.Asset.FileExtension = Path.GetExtension(picture.FileName);

        unitOfWork.Students.Update(student);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateStudentCourseStatusAsync(int studentId, int courseId, EnrollmentStatus status)
    {
        var studentCourse = await unitOfWork.Enrollments
            .SelectAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId)
            ?? throw new NotFoundException("Enrollments not found");

        studentCourse.Status = status;

        await unitOfWork.SaveAsync();
    }
}
