using Marqa.Domain.Enums;
using Marqa.Service.Services.Students.Models.DetailModels;

namespace Marqa.Service.Services.Students.Models;

public class StudentCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int CompanyId { get; set; }


    //student details

    public StudentDetailCreateModel StudentDetailCreateModel { get; set; }

}