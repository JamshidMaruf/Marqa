using Marqa.DataAccess.Contexts;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Employees.EmployeeServices;
using Microsoft.EntityFrameworkCore;


var emp = new EmployeeService();

var result = await emp.GetAllTeachersAsync(1,"physics",2);
//await emp.GetTeacherAsync(1);

Console.WriteLine(result.Count);