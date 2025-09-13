using Marqa.Domain.Entities;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Students.Models;

public class StudentCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int CompanyId { get; set; }
}