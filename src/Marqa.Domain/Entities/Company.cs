﻿namespace Marqa.Domain.Entities;

public class Company : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Director { get; set; }
    
    // Navigation
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Student> Students { get; set; }
    public ICollection<Teacher> Teachers { get; set; }
    public ICollection<EmployeeRole> Roles { get; set; }
    public ICollection<Product> Products { get; set; }
}