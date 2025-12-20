﻿using Marqa.Domain.Enums;
using Marqa.Service.Services.Students.Models.DetailModels;

namespace Marqa.Service.Services.Students.Models;

public class StudentViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public StudentStatus Status { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public double TotalPoints { get; set; }
    public StudentDetailViewModel Detail { get; set; }
    public List<StudentCourseViewData> Courses { get; set; }
    public List<StudentExamResultData> ExamResults { get; set; }
    public List<StudentPaymentOperationData> PaymentOperations { get; set; }
    public List<StudentPointHistoryData> PointHistories { get; set; }
}

public class StudentCourseViewData
{
    public string CourseName { get; set; }
    public string Subject { get; set; }
    public string TeacherFullName { get; set; }
    public string CourseStatusName { get; set; }
    public string CourseLevel { get; set; }
}

public class StudentExamResultData
{
    public string Title { get; set; }
    public double Score { get; set; }
}

public class StudentPaymentOperationData
{
    public string PaymentNumber { get; set; } 
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }
    public PaymentOperationType PaymentOperationType { get; set; }
    public decimal CoursePrice { get; set; }
}

public class StudentPointHistoryData
{
    public int GivenPoint {  get; set; }
    public int CurrentPoint { get; set; }
    public string Note { get; set; }
    public PointHistoryOperation Operation { get; set; }
    public DateTime GivenDate { get; set; }
}