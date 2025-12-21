using System.ComponentModel.DataAnnotations;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.TeacherAssessments.Models;

public class TeacherAssessmentCreateModel
{
    public int TeacherId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public Rate Rating { get; set; }

    public string Description { get; set; }
}