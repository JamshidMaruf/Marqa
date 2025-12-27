using System.Threading.Tasks;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Lessons;

public class LessonService(
    IUnitOfWork unitOfWork,
    IFileService fileService) : ILessonService
{
    public async Task UpdateAsync(int id, LessonUpdateModel model)
    {
        var lessonForUpdation = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        lessonForUpdation.StartTime = model.StartTime;
        lessonForUpdation.EndTime = model.EndTime;
        lessonForUpdation.Date = model.Date;

        foreach (var lessonteacher in lessonForUpdation.Teachers)
            unitOfWork.LessonTeachers.MarkAsDeleted(lessonteacher);

        var lessonTeachers = new List<LessonTeacher>();
        foreach (var teacherId in model.TeacherIds)
            lessonTeachers.Add(new LessonTeacher
            {
                TeacherId = teacherId,
                LessonId = lessonForUpdation.Id
            });

        await unitOfWork.LessonTeachers.InsertRangeAsync(lessonTeachers);

        unitOfWork.Lessons.Update(lessonForUpdation);

        await unitOfWork.SaveAsync();
    }

    public async Task ModifyAsync(int id, string name, HomeTaskStatus homeTaskStatus)
    {
        var lesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        lesson.Name = name;
        lesson.HomeTaskStatus = homeTaskStatus;

        unitOfWork.Lessons.Update(lesson);

        await unitOfWork.SaveAsync();
    }

    public async Task VideoUploadAsync(int id, IFormFile video)
    {
        var existLesson = await unitOfWork.Lessons.CheckExistAsync(l => l.Id == id);

        if (!existLesson)
            throw new NotFoundException($"Lesson was not found with this ID = {id}");

        if (fileService.IsVideoExtension(Path.GetExtension(video.FileName)))
            throw new ArgumentIsNotValidException("Not supported video format");

        var result = await fileService.UploadAsync(video, "Files/Videos");

        unitOfWork.LessonVideos.Insert(new LessonVideo
        {
            Asset = new Asset
            {
                FileName = result.FileName,
                FilePath = result.FilePath,
            },
            LessonId = id
        });

        await unitOfWork.SaveAsync();
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        var lesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        await EnsureAllStudentsExistAsync(lesson.CourseId, model.Students.Select(s => s.Id));

        if (lesson.Number == 1)
        {
            var course = await unitOfWork.Courses.SelectAsync(c => c.Id == lesson.CourseId);
            course.Status = CourseStatus.Active;
        }

        if (!lesson.IsCompleted)
            lesson.IsCompleted = true;

        if (!lesson.IsAttended)
        {
            foreach (var studentData in model.Students)
            {
                unitOfWork.LessonAttendances.Insert(new LessonAttendance
                {
                    LessonId = model.LessonId,
                    StudentId = studentData.Id,
                    Status = studentData.Status,
                    LateTimeInMinutes = studentData.Status == AttendanceStatus.Late ? studentData.LateTimeInMinutes.Value : 0
                });

            }

            lesson.IsAttended = true;
            unitOfWork.Lessons.Update(lesson);
        }
        else
        {
            var lessonAttendances = await unitOfWork.LessonAttendances
                .SelectAllAsQueryable(la => la.LessonId == model.LessonId &&
                    model.Students.Select(s => s.Id).Contains(la.StudentId))
                .ToListAsync();

            foreach (var lessonAttendance in lessonAttendances)
            {
                var student = model.Students.Where(s => s.Id == lessonAttendance.StudentId).FirstOrDefault()
                    ?? throw new NotFoundException($"No Student was not found with ID {lessonAttendance.StudentId}");

                lessonAttendance.Status = student.Status;
                lessonAttendance.LateTimeInMinutes = student.Status == AttendanceStatus.Late ? student.LateTimeInMinutes.Value : 0;
            }

            await unitOfWork.LessonAttendances.UpdateRangeAsync(lessonAttendances);
        }

        await unitOfWork.SaveAsync();
    }

    public Task<List<LessonViewModel>> GetByCourseIdAsync(int courseId)
    {
        return unitOfWork.Lessons.SelectAllAsQueryable(l => l.CourseId == courseId)
            .Select(l => new LessonViewModel
            {
                Id = l.Id,
                Name = l.Name,
                Date = l.Date,
                Number = l.Number,
                HomeTaskStatus = l.HomeTaskStatus
            })
            .ToListAsync();
    }

    public async Task<List<StudentAttendanceModel>> GetCourseStudentsForCheckUpAsync(int lessonId)
    {
        var lesson = await unitOfWork.Lessons
            .SelectAsync(l => l.Id == lessonId);

        if (lesson.IsAttended)
        {
            var studentAttendaceData = await unitOfWork.LessonAttendances
                .SelectAllAsQueryable(la => la.LessonId == lesson.Id)
                .Select(l => new StudentAttendanceModel
                {
                    StudentId = l.StudentId,
                    StudentName = $"{l.Student.User.FirstName} {l.Student.User.LastName}".Trim(),
                    Status = l.Status,
                    LateTimeInMinutes = l.LateTimeInMinutes
                }).ToListAsync();

            var studentIds = studentAttendaceData.Select(s => s.StudentId);

            var lessonAttendances = await unitOfWork.Enrollments
                .SelectAllAsQueryable(e => 
                    e.CourseId == lesson.CourseId && 
                    !studentIds.Contains(e.StudentId) &&
                    e.Status != EnrollmentStatus.Dropped && 
                    e.Status != EnrollmentStatus.Completed)
                .Select(l => new StudentAttendanceModel
                {
                    StudentId = l.StudentId,
                    StudentName = $"{l.Student.User.FirstName} {l.Student.User.LastName}".Trim(),
                    Status = l.Status == EnrollmentStatus.Frozen ? AttendanceStatus.Frozen : AttendanceStatus.None
                })
                .ToListAsync();

            studentAttendaceData.AddRange(lessonAttendances);

            return studentAttendaceData;
        }
        else
        {
            return await unitOfWork.Enrollments
                .SelectAllAsQueryable(e => 
                    e.CourseId == lesson.CourseId && 
                    e.Status != EnrollmentStatus.Dropped && 
                    e.Status != EnrollmentStatus.Completed)
                .Select(l => new StudentAttendanceModel
                {
                    StudentId = l.StudentId,
                    StudentName = $"{l.Student.User.FirstName} {l.Student.User.LastName}".Trim(),
                    Status = l.Status == EnrollmentStatus.Frozen ? AttendanceStatus.Frozen : AttendanceStatus.None
                })
                .ToListAsync();
        }
    }

    private async Task EnsureAllStudentsExistAsync(int courseId, IEnumerable<int> studentIds)
    {
        var enrollments = new HashSet<int>(
            await unitOfWork.Courses.SelectAllAsQueryable(c => c.Id == courseId)
            .SelectMany(c => c.Enrollments.Select(e => e.StudentId))
            .FirstOrDefaultAsync());

        //TODO:check for enrollment status too
        var missings = studentIds.Where(id => !enrollments.Contains(id)).ToList();

        if (missings.Count > 0)
            throw new ArgumentIsNotValidException($"These students were not found for this course: {string.Join(", ", missings)}");
    }
    public async Task<List<CourseLesson>> GetCoursesLessonsAsync(DateOnly date)
    {
        return await unitOfWork.Lessons
            .SelectAllAsQueryable(l => l.Date == date)
            .Select(l => new CourseLesson
                {
                    CourseId = l.CourseId,
                    CourseName = l.Course.Name,

                    TeacherId = l.TeacherId,
                    TeacherName = l.Teachers
                        .Where(t => t.TeacherId == l.TeacherId)
                        .Select(t => t.Teacher.User.FirstName)
                        .FirstOrDefault(),

                    CourseStudentsCount = l.Course.MaxStudentCount,
                    CoursePresentStudentsCount = l.Attendances
                        .Count(a => a.Status == AttendanceStatus.Present),

                    AttendPercentage = l.Course.MaxStudentCount == 0
                        ? 0
                        : Math.Round(
                            (decimal)l.Attendances.Count(a => a.Status == AttendanceStatus.Present) * 100
                            / l.Course.MaxStudentCount,
                            2),

                    IsCheckedUp = l.IsAttended,
                    LessonId = l.Id,
                    LessonNumber = l.Number
                })
            .ToListAsync();
    }

    public async Task<CurrentAttendanceStatistics> GetStatisticsAsync(int companyId)
    {
        var totalStudentsCount = await unitOfWork.Students
            .SelectAllAsQueryable(s =>  s.CompanyId == companyId && 
            s.Status == StudentStatus.Active).CountAsync();

        var totalPresentStudentsCount = await unitOfWork.LessonAttendances
            .SelectAllAsQueryable(la => 
                la.Student.CompanyId == companyId &&
                la.Lesson.Date == DateOnly.FromDateTime(DateTime.UtcNow) &&
                (la.Status == AttendanceStatus.Present || 
                la.Status == AttendanceStatus.Late))
            .CountAsync();

        var totalAbsentStudentsCount = await unitOfWork.LessonAttendances
            .SelectAllAsQueryable(la =>
                la.Student.CompanyId == companyId &&
                la.Lesson.Date == DateOnly.FromDateTime(DateTime.UtcNow) &&
                (la.Status == AttendanceStatus.Absent ||
                la.Status == AttendanceStatus.Excused))
            .CountAsync();

        var attendancePercentage = Math.Round((((double)totalPresentStudentsCount / totalStudentsCount) * 100), 1);

        return new CurrentAttendanceStatistics
        {
            TotalStudentCount = totalStudentsCount,
            TotalPresentStudentCount = totalPresentStudentsCount,
            TotalAbsentStudentCount = totalAbsentStudentsCount,
            AttendancePercentage = attendancePercentage
        };
    }
}