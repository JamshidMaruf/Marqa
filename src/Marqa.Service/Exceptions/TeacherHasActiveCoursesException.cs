namespace Marqa.Service.Exceptions;
public class TeacherHasActiveCoursesException : Exception
{
    public string TeacherName { get; }
    public int ActiveCourseCount { get; }
    public List<string> CourseNames { get; }

    public TeacherHasActiveCoursesException(string teacherName, int activeCourseCount, List<string> courseNames)
        : base($"Cannot delete teacher '{teacherName}'. This teacher has {activeCourseCount} active course(s).")
    {
        TeacherName = teacherName;
        ActiveCourseCount = activeCourseCount;
        CourseNames = courseNames;
    }
}