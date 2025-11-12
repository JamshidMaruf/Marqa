namespace Marqa.Service.Services.Employees.Models;

/// <summary>
/// Represents the result of an employee profile picture upload operation
/// </summary>
public class EmployeeProfilePictureResult
{
    public string Path { get; set; }
    public string ImageName { get; set; }
    public string Extension { get; set; }
}
