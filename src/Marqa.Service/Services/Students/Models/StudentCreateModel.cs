using System.ComponentModel.DataAnnotations;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Students.Models;

public class StudentCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int CompanyId { get; set; }
}