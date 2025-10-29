using Marqa.Domain.Enums;
using Marqa.Service.Services.Students.Models.DetailModels;

namespace Marqa.Service.Services.Students.Models;

public class StudentViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    //details

    public StudentDetailViewModel StudentDetailViewModel { get; set; }
}
