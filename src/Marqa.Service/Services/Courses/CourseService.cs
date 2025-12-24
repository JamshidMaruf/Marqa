using FluentValidation;
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
                Subject = model.Subject,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Level = model.Level,
                Price = model.Price,
                Status = model.Status,
                Description = model.Description,
                MaxStudentCount = model.MaxStudentCount,
                CompanyId = model.CompanyId
            };

            unitOfWork.Courses.Insert(createdCourse);
            await unitOfWork.SaveAsync();

            var teacherCourses = new List<CourseTeacher>();
            foreach (var teacherId in model.TeacherIds)
            {
                teacherCourses.Add(new CourseTeacher
                {
                    TeacherId = teacherId,
                    CourseId = createdCourse.Id
                });
            }

            await unitOfWork.CourseTeachers.InsertRangeAsync(teacherCourses);
            await unitOfWork.SaveAsync();

            var weekDays = new List<CourseWeekdayModel>();
            var courseWeekDays = new List<CourseWeekday>();

            foreach (var weekDay in model.Weekdays)
            {
                courseWeekDays.Add(new CourseWeekday
                {
                    Weekday = weekDay.DayOfWeek,
                    StartTime = weekDay.StartTime,
                    EndTime = weekDay.EndTime,
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
                model.TeacherIds,
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

    public async Task<List<CourseMinimalListModel>> GetMinimalListAsync(int companyId)
    {
        var courses = await unitOfWork.Courses
            .SelectAllAsQueryable(c => c.CompanyId == companyId && !c.IsDeleted)
            .Select(c => new CourseMinimalListModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

        return courses;
    }

    public async Task UpdateAsync(int id, CourseUpdateModel model)
    {
        await courseUpdateValidator.EnsureValidatedAsync(model);

        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable(c => !c.IsDeleted,
            includes: ["Lessons", "CourseWeekdays", "Enrollments"])
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        existCourse.Price = model.Price;
        existCourse.StartDate = model.StartDate;
        existCourse.EndDate = model.Status == CourseStatus.Closed ? DateOnly.FromDateTime(DateTime.UtcNow) : model.EndDate;
        existCourse.Status = model.Status;
        existCourse.Subject = model.Subject;
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

            foreach (var courseTeacher in existCourse.CourseTeachers)
                unitOfWork.CourseTeachers.MarkAsDeleted(courseTeacher);

            await unitOfWork.SaveAsync();

            if (existCourse.Price != model.Price)
            {
                foreach (var enrollment in existCourse.Enrollments)
                {
                    enrollment.CoursePrice = model.Price;
                    unitOfWork.Enrollments.Update(enrollment);
                }

                await unitOfWork.SaveAsync();
            }

            var teacherCourses = new List<CourseTeacher>();
            foreach (var teacherId in model.TeacherIds)
                teacherCourses.Add(new CourseTeacher
                {
                    TeacherId = teacherId,
                    CourseId = existCourse.Id
                });

            await unitOfWork.CourseTeachers.InsertRangeAsync(teacherCourses);
            await unitOfWork.SaveAsync();

            foreach (var weekDay in model.Weekdays)
                unitOfWork.CourseWeekdays.Insert(new CourseWeekday
                {
                    CourseId = id,
                    StartTime = weekDay.StartTime,
                    EndTime = weekDay.EndTime,
                    Weekday = weekDay.DayOfWeek,
                });

            await unitOfWork.SaveAsync();


            await GenerateLessonAsync(
               existCourse.Id,
               model.StartDate,
               model.EndDate,
               model.Room,
               model.TeacherIds,
               model.Weekdays);

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
            includes: ["Lessons", "Lessons.LessonTeachers", "CourseWeekdays", "Enrollments", "TeacherCourses"])
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        foreach (var lesson in existCourse.Lessons)
            unitOfWork.Lessons.MarkAsDeleted(lesson);

        foreach (var weekday in existCourse.CourseWeekdays)
            unitOfWork.CourseWeekdays.MarkAsDeleted(weekday);

        foreach (var enrollment in existCourse.Enrollments)
            unitOfWork.Enrollments.MarkAsDeleted(enrollment);

        foreach (var lessonTeacher in existCourse.Lessons.SelectMany(l => l.Teachers))
            unitOfWork.LessonTeachers.MarkAsDeleted(lessonTeacher);

        foreach (var courseTeacher in existCourse.CourseTeachers)
            unitOfWork.CourseTeachers.MarkAsDeleted(courseTeacher);

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
                Subject = c.Subject,
                Teachers = c.CourseTeachers.Select(ct => new CourseViewModel.TeacherInfo
                {
                    Id = ct.Teacher.Id,
                    FirstName = ct.Teacher.User.FirstName,
                    LastName = ct.Teacher.User.LastName
                }),
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
            predicate: c => c.Id == id)
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
                Subject = c.Subject,
                Teachers = c.CourseTeachers.Select(ct => new CourseUpdateViewModel.TeacherInfo
                {
                    Id = ct.Teacher.Id,
                    FirstName = ct.Teacher.User.FirstName,
                    LastName = ct.Teacher.User.LastName
                }),
                Weekdays = c.CourseWeekdays.Select(w => new CourseUpdateViewModel.WeekInfo
                {
                    Id = Convert.ToInt32(w.Weekday),
                    Name = Enum.GetName(w.Weekday),
                    StartTime = w.StartTime,
                    EndTime = w.EndTime
                }),
                Lessons = c.Lessons.Select(cl => new CourseUpdateViewModel.LessonInfo
                {
                    Id = cl.Id,
                    Date = cl.Date,
                    EndTime = cl.EndTime,
                    StartTime = cl.StartTime,
                    Room = cl.Room,
                }).OrderBy(l => l.Date)
            })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Course is not found with this ID {id}");
    }

    public async Task<List<CourseTableViewModel>> GetAllAsync(
        int companyId,
        string search,
        CourseStatus? status = null)
    {
        var query = unitOfWork.Courses.SelectAllAsQueryable(c => c.CompanyId == companyId);

        if (!string.IsNullOrEmpty(search))
        {
            var searchText = search.ToLower();
            query = query.Where(t =>
                t.Name.ToLower().Contains(searchText) ||
                t.Description.ToLower().Contains(searchText) ||
                t.Subject.ToLower().Contains(searchText));
        }

        if (status != null && status != 0)
            query = query.Where(t => t.Status == status);

        return await query
            .Select(c => new CourseTableViewModel
            {
                Id = c.Id,
                Name = c.Name,
                LessonCount = c.LessonCount,
                Status = c.Status,
                Level = c.Level,
                MaxStudentCount = c.MaxStudentCount,
                AvailableStudentCount = c.Enrollments.Count,
                Price = c.Price,
                Subject = c.Subject,
                Teachers = c.CourseTeachers.Select(ct => new CourseTableViewModel.TeacherInfo
                {
                    Id = ct.Teacher.Id,
                    FirstName = ct.Teacher.User.FirstName,
                    LastName = ct.Teacher.User.LastName
                }),
                Weekdays = c.CourseWeekdays.Select(w => new CourseTableViewModel.WeekInfo
                {
                    Id = Convert.ToInt32(w.Weekday),
                    Name = Enum.GetName(w.Weekday),
                })
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

    public async Task<List<CourseMinimalListModel>> GetUnEnrolledStudentCoursesAsync(int companyId, int studentId)
    {
        var courses = await unitOfWork.Courses
            .SelectAllAsQueryable(predicate: c =>
                c.CompanyId == companyId &&
                (c.Status == CourseStatus.Active ||
                 c.Status == CourseStatus.Upcoming),
                 includes: "Enrollments").ToListAsync();


         return courses.Where(c => !c.Enrollments.Any(e => e.StudentId == studentId))
            .Select(c => new CourseMinimalListModel
            { 
                Id = c.Id,
                Name = c.Name
            }).ToList();
    }

    public async Task<List<CourseNamesModel>> GetActiveStudentCoursesAsync(int studentId)
    {
        return await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: c =>
                c.StudentId == studentId &&
                c.Status == EnrollmentStatus.Active)
            .Select(c => new CourseNamesModel
            {
                Id = c.Id,
                Name = c.Course.Name
            })
            .ToListAsync();
    }

    public async Task<List<FrozenEnrollmentModel>> GetFrozenCoursesAsync(int studentId)
    {
        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: c =>
                c.StudentId == studentId &&
                c.Status == EnrollmentStatus.Frozen,
                includes: ["Course", "EnrollmentFrozens"]).ToListAsync();

        var mappedFrozenModels = new List<FrozenEnrollmentModel>();
        foreach (var enrollment in enrollments)
        {
            if (enrollment.Status == EnrollmentStatus.Frozen)
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

    public async Task<UpcomingCourseViewModel> GetUpcomingCourseStudentsAsync(int courseId)
    {
        return await unitOfWork.Courses
                   .SelectAllAsQueryable(c => c.Id == courseId && c.Status == CourseStatus.Upcoming)
                   .Select(c => new UpcomingCourseViewModel
                   {
                       MaxStudentCount = c.MaxStudentCount,
                       EnrolledStudentCount = c.Enrollments.Count,
                       AvailableSeats = c.MaxStudentCount - c.Enrollments.Count,
                       Students = c.Enrollments.Select(e => new UpcomingCourseViewModel.StudentData
                       {
                           FullName = (e.Student.User.FirstName + " " + e.Student.User.LastName).Trim(),
                           DateOfEnrollment = e.EnrolledDate
                       }).ToList()
                   }).FirstOrDefaultAsync()
               ?? throw new NotFoundException("Course was not found");
    }

    public async Task<List<StudentList>> GetStudentsListAsync(int courseId)
    {
        return await unitOfWork.Students
            .SelectAllAsQueryable(s => s.Enrollments.Any(e => e.CourseId == courseId && e.Status == EnrollmentStatus.Active))
            .Select(s => new StudentList
            {
                Id = s.Id,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName
            }).ToListAsync();
    }

    public async Task BulkEnrollStudentsAsync(BulkEnrollStudentsModel model)
    {
        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var students = await unitOfWork.Students
                .SelectAllAsQueryable(s => model.StudentIds.Contains(s.Id) && !s.IsDeleted)
                .ToListAsync();

            if (students.Count != model.StudentIds.Count)
            {
                var foundIds = students.Select(s => s.Id).ToList();
                var notFoundIds = model.StudentIds.Except(foundIds).ToList();
                throw new NotFoundException($"{notFoundIds.Count} student(s) not found");
            }

            var existingEnrollments = await unitOfWork.Enrollments
                .SelectAllAsQueryable(e =>
                    e.CourseId == model.CourseId &&
                    model.StudentIds.Contains(e.StudentId) &&
                    !e.IsDeleted)
                .Select(e => e.StudentId)
                .ToListAsync();

            if (existingEnrollments.Any())
                throw new AlreadyExistException($"{existingEnrollments.Count} student(s) already enrolled in this course");

            var enrollments = model.StudentIds.Select(studentId => new Enrollment
            {
                StudentId = studentId,
                CourseId = model.CourseId,
                EnrolledDate = model.EnrollmentDate,
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

    public Task CreateTeacherAssessmentAsync(TeacherAssessment model)
    {
        throw new NotImplementedException();
    }

    private async Task GenerateLessonAsync(
     int courseId,
     DateOnly startDate,
     DateOnly endDate,
     string room,
     IEnumerable<int> teacherIds,
     List<CourseWeekdayModel> weekDays)
    {
        var lessons = new List<Lesson>();
        var initialLesson = new Lesson
        {
            CourseId = courseId,
            StartTime = weekDays[0].StartTime,
            EndTime = weekDays[0].EndTime,
            Room = room,
            Number = 1,
            Date = startDate,

        };

        lessons.Add(initialLesson);

        var lessonTeachers = new List<LessonTeacher>();

        foreach (var teacherId in teacherIds)
        {
            lessonTeachers.Add(new LessonTeacher
            {
                TeacherId = teacherId,
                LessonId = initialLesson.Id
            });
        }

        await unitOfWork.LessonTeachers.InsertRangeAsync(lessonTeachers);

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
                        CourseId = courseId,
                        StartTime = weekDays[i].StartTime,
                        EndTime = weekDays[i].EndTime,
                        Room = room,
                        Number = i,
                        Date = currentDate,
                        Teachers = lessonTeachers
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
}
