using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Courses;

public class CourseService(IUnitOfWork unitOfWork,
    IValidator<CourseCreateModel> courseCreateValidator,
    IValidator<CourseUpdateModel> courseUpdateValidator) : ICourseService
{
    public async Task CreateAsync(CourseCreateModel model)
    {
        await courseCreateValidator.EnsureValidatedAsync(model);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            Course createdCourse = new Course
            {
                Name = model.Name,
                SubjectId = model.SubjectId,
                StartDate = model.StartDate,
            //    TeacherId = model.TeacherId,
                Level = model.Level,
                Price = model.Price,
                Status = model.Status,
                Description = model.Description,
                MaxStudentCount = model.MaxStudentCount,
                CompanyId = model.CompanyId
            };

            unitOfWork.Courses.Insert(createdCourse);
            await unitOfWork.SaveAsync();


            var weekDays = new List<CourseCreateModel.Weekday>();
            var courseWeekDays = new List<CourseWeekday>();

            foreach (var weekDay in model.Weekdays)
            {
                courseWeekDays.Add(new CourseWeekday
                {
                    Weekday = weekDay.DayOfWeek,
                    StartTime =  weekDay.StartTime,
                    EndTime =  weekDay.EndTime,
                    CourseId = createdCourse.Id
                });

                weekDays.Add(weekDay);
            }

            await unitOfWork.CourseWeekdays.InsertRangeAsync(courseWeekDays);
            await unitOfWork.SaveAsync();


            await GenerateLessonAsync(
                createdCourse.Id,
                model.StartDate,
                model.EndDate,
                model.Room,
                model.TeacherId,
                weekDays);
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(int id, CourseUpdateModel model)
    {
        await courseUpdateValidator.EnsureValidatedAsync(model);

        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable(c => !c.IsDeleted,
            includes: ["Lessons", "CourseWeekdays"])
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");
        
      //  existCourse.TeacherId = model.TeacherId;
        existCourse.Price = model.Price;
        existCourse.StartDate = model.StartDate;
        existCourse.Status = model.Status;
        existCourse.MaxStudentCount = model.MaxStudentCount;
        existCourse.Description = model.Description;
        existCourse.Level = model.Level;

        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            foreach (var lesson in existCourse.Lessons)
                unitOfWork.Lessons.Remove(lesson);

            await unitOfWork.SaveAsync();

            foreach (var weekday in existCourse.CourseWeekdays)
                unitOfWork.CourseWeekdays.Remove(weekday);

            await unitOfWork.SaveAsync();

            foreach (var weekDay in model.Weekdays)
                unitOfWork.CourseWeekdays.Insert(new CourseWeekday
                {
                    CourseId = id,
                    StartTime =  weekDay.StartTime,
                    EndTime =  weekDay.EndTime,
                    Weekday = weekDay.DayOfWeek,
                });

            await unitOfWork.SaveAsync();


            await GenerateLessonAsync(
               existCourse.Id,
               model.StartDate,
               model.EndDate,
               model.Room,
               model.TeacherId,
               model.Weekdays);
            await unitOfWork.SaveAsync();

            unitOfWork.Courses.Update(existCourse);
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable(
            predicate: c => !c.IsDeleted,
            includes: new[] { "Lessons", "CourseWeekdays" })
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        foreach (var lesson in existCourse.Lessons)
            unitOfWork.Lessons.MarkAsDeleted(lesson);

        foreach (var weekday in existCourse.CourseWeekdays)
            unitOfWork.CourseWeekdays.MarkAsDeleted(weekday);

        unitOfWork.Courses.MarkAsDeleted(existCourse);

        await unitOfWork.SaveAsync();
    }

    public async Task<CourseViewModel> GetAsync(int id)
    {
        return await unitOfWork.Courses
            .SelectAllAsQueryable(
            predicate: c => !c.IsDeleted)
            .Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                LessonCount = c.LessonCount,
                StartDate = c.StartDate,
                Status = c.Status,
                Description = c.Description,
                AvailableStudentCount = c.Enrollments.Count,
                Price = c.Price,
                Subject = new CourseViewModel.SubjectInfo
                {
                    SubjectId = c.SubjectId,
                    SubjectName = c.Subject.Name,
                },
                Teacher = new CourseViewModel.TeacherInfo
                {
                 //   Id = c.TeacherId,
                 //   FirstName = c.Teacher.User.FirstName,
                  //  LastName = c.Teacher.User.LastName,
                },
                Weekdays = c.CourseWeekdays.Select(w => new CourseViewModel.WeekInfo
                {
                    Id = Convert.ToInt32(w.Weekday),
                    Name = Enum.GetName(w.Weekday),
                })
                .ToList(),
                Lessons = c.Lessons.Select(cl => new CourseViewModel.LessonInfo
                {
                    Id = cl.Id,
                    Date = cl.Date,
                    EndTime = cl.EndTime,
                    StartTime = cl.StartTime,
                    Room = cl.Room,
                })
                .OrderBy(l => l.Date)
                .ToList(),
            })
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");
    }

    public async Task<CourseUpdateViewModel> GetForUpdateAsync(int id)
    {
        return await unitOfWork.Courses
            .SelectAllAsQueryable(
            predicate: c => !c.IsDeleted)
            .Select(c => new CourseUpdateViewModel
            {
                Id = c.Id,
                Name = c.Name,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                MaxStudentCount = c.MaxStudentCount,
                Price = c.Price,
                Description = c.Description,
                Subject = new CourseUpdateViewModel.SubjectInfo
                {
                    SubjectId = c.SubjectId,
                    SubjectName = c.Subject.Name,
                },
                Teacher = new CourseUpdateViewModel.TeacherInfo
                {
                    // Id = c.TeacherId,
                    // FirstName = c.Teacher.User.FirstName,
                    // LastName = c.Teacher.User.LastName,
                },
                Weekdays = c.CourseWeekdays.Select(w => new CourseUpdateViewModel.WeekInfo
                {
                    Id = Convert.ToInt32(w.Weekday),
                    Name = Enum.GetName(w.Weekday),
                    StartTime = w.StartTime,
                    EndTime = w.EndTime
                }).ToList(),
                Lessons = c.Lessons.Select(cl => new CourseUpdateViewModel.LessonInfo
                {
                    Id = cl.Id,
                    Date = cl.Date,
                    EndTime = cl.EndTime,
                    StartTime = cl.StartTime,
                    Room = cl.Room,
                }).OrderBy(l => l.Date).ToList(),
            })
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");
    }

    public async Task<List<CourseViewModel>> GetAllAsync(int companyId, string search, int? subjectId = null)
    {
        var query = unitOfWork.Courses.SelectAllAsQueryable(
            predicate: c => c.CompanyId == companyId && !c.IsDeleted,
            includes: ["Subject", "Teacher", "Lessons", "Enrollments", "CourseWeekdays", "Teacher.User"]);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(t => t.Name.Contains(search));

        if (subjectId != null)
            query = query.Where(t => t.SubjectId == subjectId);

        return await query
            .Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                LessonCount = c.LessonCount,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                MaxStudentCount = c.MaxStudentCount,
                AvailableStudentCount = c.Enrollments.Count,
                Price = c.Price,
                Description = c.Description,                
                Subject = new CourseViewModel.SubjectInfo
                {
                    SubjectId = c.SubjectId,
                    SubjectName = c.Subject.Name,
                },
                Teacher = new CourseViewModel.TeacherInfo
                {
                    // Id = c.TeacherId,
                    // FirstName = c.Teacher.User.FirstName,
                    // LastName = c.Teacher.User.LastName,
                },
                Weekdays = c.CourseWeekdays.Select(w => new CourseViewModel.WeekInfo
                {
                    Id = Convert.ToInt32(w.Weekday),
                    Name = Enum.GetName(w.Weekday),
                }).ToList(),
                Lessons = c.Lessons.Select(cl => new CourseViewModel.LessonInfo
                {
                    Id = cl.Id,
                    Date = cl.Date,
                    EndTime = cl.EndTime,
                    StartTime = cl.StartTime,
                    Room = cl.Room,
                }).OrderBy(l => l.Date).ToList(),
            })
            .ToListAsync();
    }

    public async Task<List<MainPageCourseViewModel>> GetCoursesByStudentIdAsync(int studentId)
    {
        return await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: c => c.StudentId == studentId)
            .Include(c => c.Course)
            .ThenInclude(c => c.Lessons)
            .Select(c => new MainPageCourseViewModel
            {
                CourseId = c.CourseId,
                LessonNumber = (c.Course.Lessons.Where(l => l.IsCompleted).OrderByDescending(l => l.Date).First()).Number,
                CourseName = c.Course.Name,
                HomeTaskStatus = (c.Course.Lessons.Where(l => l.IsCompleted).OrderByDescending(l => l.Date).First()).HomeTaskStatus,
            })
            .ToListAsync();
    }

    public Task<List<CoursePageCourseViewModel>> GetCourseNamesByStudentIdAsync(int studentId)
    {
        return unitOfWork.Enrollments.SelectAllAsQueryable(
            predicate: sc => sc.StudentId == studentId)
            .Select(sc => new CoursePageCourseViewModel
            {
                Id = sc.CourseId,
                Name = sc.Course.Name
            })
            .ToListAsync();
    }

    public async Task<List<CourseNamesModel>> GetAllStudentCourseNamesAsync(int studentId)
    {
        return await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: c => c.StudentId == studentId &&
            c.Status != EnrollmentStatus.Dropped)
            .Select(c => new CourseNamesModel
            {
                Id = c.Course.Id,
                Name = c.Course.Name
            })
            .ToListAsync();
    }

    public async Task<List<MinimalCourseDataModel>> GetUnEnrolledStudentCoursesAsync(int studentId, int companyId)
    {
        return await unitOfWork.Courses
            .SelectAllAsQueryable(predicate: c =>
                c.CompanyId == companyId &&
                !c.Enrollments.Any(e => e.StudentId == studentId) &&
                (c.Status == CourseStatus.Active ||
                 c.Status == CourseStatus.Upcoming))
            .Select(c => new MinimalCourseDataModel
            {
                Id = c.Id,
                Name = c.Name,
               // TeacherFullName = $"{c.Teacher.User.FirstName} {c.Teacher.User.LastName}",
                MaxStudentCount = c.MaxStudentCount,
                CoursePrice = c.Price
            }).ToListAsync();
    }

    public async Task<List<NonFrozenEnrollmentModel>> GetActiveStudentCoursesAsync(int studentId)
    {
        return await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: c =>
                c.StudentId == studentId &&
                c.Status == EnrollmentStatus.Active &&
                (c.Course.Status == CourseStatus.Active ||
                c.Course.Status == CourseStatus.Upcoming))
            .Select(c => new NonFrozenEnrollmentModel
            {
                Id = c.Id,
                Name = c.Course.Name,
                Level = c.Course.Level
            }).ToListAsync();
    }

    public async Task<List<FrozenEnrollmentModel>> GetFrozenCoursesAsync(int studentId)
    {
        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: c =>
                c.StudentId == studentId &&
                c.Status == EnrollmentStatus.Frozen,
                includes: [ "Course", "EnrollmentFrozens" ]).ToListAsync();

        var mappedFrozenModels = new List<FrozenEnrollmentModel>();
        foreach(var enrollment in enrollments)
        {
            if(enrollment.Status == EnrollmentStatus.Frozen)
            {
                var last = enrollment.EnrollmentFrozens
                    .OrderByDescending(e => e.CreatedAt)
                    .FirstOrDefault();

                mappedFrozenModels.Add(new FrozenEnrollmentModel
                {
                    Id = enrollment.Id,
                    Name = enrollment.Course.Name,
                    Level = enrollment.Course.Level,
                    FrozenDate = last.StartDate,
                    EndDate = last.EndDate,
                    Reason = last.Reason
                });
            }
        }

        return mappedFrozenModels;
    }

    private async Task GenerateLessonAsync(
     int courseId,
     DateOnly startDate,
     DateOnly endDate,
     string room,
     int teacherId,
     List<CourseCreateModel.Weekday> weekDays)
    {
        var lessons = new List<Lesson>
        {
            new Lesson
            {
                CourseId = courseId,
                StartTime = weekDays[0].StartTime,
                EndTime = weekDays[0].EndTime,
                Room = room,
                Number = 1,
                Date = startDate,
                TeacherId = teacherId
            }
        };

        var lessonCount = 1;
        var count = 2;
        var currentDate = startDate;

        await Task.Run(() =>
        {
            while (currentDate <= endDate)
            {
                for (int i = count; i < weekDays.Count; i++)
                {
                    while (weekDays[i].DayOfWeek != currentDate.DayOfWeek)
                    {
                        currentDate = currentDate.AddDays(1);
                    }
                    lessons.Add(new Lesson
                    {
                        CourseId =  courseId,
                        StartTime = weekDays[i].StartTime,
                        EndTime = weekDays[i].EndTime,
                        Room = room,
                        Number = i,
                        Date = currentDate,
                        TeacherId = teacherId
                    });
                    lessonCount++;
                }
            }
        });

        var course = await unitOfWork.Courses.SelectAsync(c => c.Id == courseId);
        
        course.LessonCount = lessonCount;
        unitOfWork.Courses.Update(course);

        await unitOfWork.Lessons.InsertRangeAsync(lessons);
    }

    public async Task<UpcomingCourseViewModel> GetUpcomingCourseStudentsAsync(int courseId)
    {
        var course = await unitOfWork.Courses
            .SelectAllAsQueryable(c => c.Id == courseId && c.Status == CourseStatus.Upcoming)
            .FirstOrDefaultAsync();

        return new UpcomingCourseViewModel
        {
            EnrolledStudentCount = course.Enrollments.Count,
            AvailableSeats = course.MaxStudentCount - course.Enrollments.Count,
            MaxStudentCount = course.MaxStudentCount,
            Students = course.Enrollments.Select(e => new UpcomingCourseViewModel.StudentData
            {
                FullName = $"{e.Student.User.FirstName} {e.Student.User.LastName}",
                DateOfEnrollment = e.EnrolledDate
            }).ToList()
        };
    }
    public async Task BulkEnrollStudentsAsync(int courseId, List<int> studentIds, DateTime enrollmentDate)
    {
        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            var students = await unitOfWork.Students
                .SelectAllAsQueryable(s => studentIds.Contains(s.Id) && !s.IsDeleted)
                .ToListAsync();

            if (students.Count != studentIds.Count)
            {
                var foundIds = students.Select(s => s.Id).ToList();
                var notFoundIds = studentIds.Except(foundIds).ToList();
                throw new NotFoundException($"{notFoundIds.Count} student(s) not found");
            }

            var existingEnrollments = await unitOfWork.Enrollments
                .SelectAllAsQueryable(e =>
                    e.CourseId == courseId &&
                    studentIds.Contains(e.StudentId) &&
                    !e.IsDeleted)
                .Select(e => e.StudentId)
                .ToListAsync();

            if (existingEnrollments.Any())
                throw new AlreadyExistException($"{existingEnrollments.Count} student(s) already enrolled in this course");

            var enrollments = studentIds.Select(studentId => new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrolledDate = enrollmentDate,
                Status = EnrollmentStatus.Active,
                PaymentType = CoursePaymentType.DiscountFree,
                Amount = 0
            }).ToList();

            await unitOfWork.Enrollments.InsertRangeAsync(enrollments);
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
