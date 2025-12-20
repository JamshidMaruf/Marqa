using System.ComponentModel.DataAnnotations;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.TeacherAssessments.Models;

public class TeacherAssessmentCreateModel
{
    [Required]
    public int TeacherId { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    public Rating Rating { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; }
}