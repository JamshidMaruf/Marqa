using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marqa.Service.Services.Courses.Models;
public class StudentCoursesGetModel
{
    public List<CourseInfo> Courses { get; set; }

    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public int CourseStudentCount { get; set; }
    }

}
