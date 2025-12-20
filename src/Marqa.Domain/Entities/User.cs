﻿using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    public bool IsUseSystem { get; set; }
    public UserRole Role { get; set; }
}