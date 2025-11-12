namespace Marqa.Service.Services.Students.Models;

/// <summary>
/// Represents the result of a student profile picture upload operation
/// </summary>
public class StudentProfilePictureResult
{
    public string Path { get; set; }
    public string ImageName { get; set; }
    public string Extension { get; set; }
}