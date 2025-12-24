using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Marqa.DataAccess.Helpers;

public static class DatabaseSeeder
{
    private static readonly DateTime SeedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    
    // Pre-hashed password for "Password123!" using BCrypt
    private const string DefaultPasswordHash = "$2a$11$rBLRfA3oDYZwLHvPQVmFpuKvfL7R5tMKhN9mVPQxVQvMzR5F3KWXW";
    
    public static void SeedData(ModelBuilder modelBuilder)
    {
        SeedCompanies(modelBuilder);
        SeedUsers(modelBuilder);
        SeedEmployeeRoles(modelBuilder);
        SeedEmployees(modelBuilder);
        SeedTeachers(modelBuilder);
        SeedStudents(modelBuilder);
        SeedStudentDetails(modelBuilder);
        SeedCourses(modelBuilder);
    }

    private static void SeedCompanies(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = 1,
                Name = "Marqa Education Center",
                Address = "Toshkent, Chilonzor tumani, 1-kvartal",
                Phone = "+998901234567",
                Email = "info@marqa.uz",
                Director = "Alisher Karimov",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Company
            {
                Id = 2,
                Name = "Smart Academy",
                Address = "Toshkent, Mirzo Ulug'bek tumani",
                Phone = "+998901112233",
                Email = "info@smartacademy.uz",
                Director = "Sardor Rahimov",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            // Admin/Employee users
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Marqa",
                Phone = "998900000001",
                Email = "admin@marqa.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = true,
                Role = UserRole.Employee,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new User
            {
                Id = 2,
                FirstName = "Javlon",
                LastName = "Toshmatov",
                Phone = "998901111111",
                Email = "javlon@marqa.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = true,
                Role = UserRole.Employee,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new User
            {
                Id = 3,
                FirstName = "Nilufar",
                LastName = "Karimova",
                Phone = "998902222222",
                Email = "nilufar@marqa.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = true,
                Role = UserRole.Employee,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            // Teacher users
            new User
            {
                Id = 4,
                FirstName = "John",
                LastName = "Smith",
                Phone = "998903333333",
                Email = "john@marqa.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = true,
                Role = UserRole.Employee,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new User
            {
                Id = 5,
                FirstName = "Akmal",
                LastName = "Saidov",
                Phone = "998904444444",
                Email = "akmal@marqa.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = true,
                Role = UserRole.Employee,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            // Student users
            new User
            {
                Id = 6,
                FirstName = "Ali",
                LastName = "Valiyev",
                Phone = "998905555555",
                Email = "ali@student.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = false,
                Role = UserRole.Student,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new User
            {
                Id = 7,
                FirstName = "Malika",
                LastName = "Rahimova",
                Phone = "998906666666",
                Email = "malika@student.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = false,
                Role = UserRole.Student,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new User
            {
                Id = 8,
                FirstName = "Bobur",
                LastName = "Ergashev",
                Phone = "998907777777",
                Email = "bobur@student.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = false,
                Role = UserRole.Student,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new User
            {
                Id = 9,
                FirstName = "Zarina",
                LastName = "Usmonova",
                Phone = "998908888888",
                Email = "zarina@student.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = false,
                Role = UserRole.Student,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new User
            {
                Id = 10,
                FirstName = "Jasur",
                LastName = "Qodirov",
                Phone = "998909999999",
                Email = "jasur@student.uz",
                PasswordHash = DefaultPasswordHash,
                IsActive = true,
                IsUseSystem = false,
                Role = UserRole.Student,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }

    private static void SeedEmployeeRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeRole>().HasData(
            new EmployeeRole
            {
                Id = 1,
                Name = "Administrator",
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new EmployeeRole
            {
                Id = 2,
                Name = "Manager",
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new EmployeeRole
            {
                Id = 3,
                Name = "Receptionist",
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new EmployeeRole
            {
                Id = 4,
                Name = "Administrator",
                CompanyId = 2,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }

    private static void SeedEmployees(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                UserId = 1,
                CompanyId = 1,
                DateOfBirth = new DateOnly(1985, 5, 15),
                Salary = 10000000,
                Gender = Gender.Male,
                Status = EmployeeStatus.Active,
                JoiningDate = new DateOnly(2020, 1, 1),
                Specialization = "System Administration",
                Info = "Main administrator",
                RoleId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Employee
            {
                Id = 2,
                UserId = 2,
                CompanyId = 1,
                DateOfBirth = new DateOnly(1990, 8, 20),
                Salary = 5000000,
                Gender = Gender.Male,
                Status = EmployeeStatus.Active,
                JoiningDate = new DateOnly(2023, 1, 1),
                Specialization = "Management",
                Info = "Branch manager",
                RoleId = 2,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Employee
            {
                Id = 3,
                UserId = 3,
                CompanyId = 1,
                DateOfBirth = new DateOnly(1995, 3, 10),
                Salary = 3500000,
                Gender = Gender.Female,
                Status = EmployeeStatus.Active,
                JoiningDate = new DateOnly(2023, 6, 1),
                Specialization = "Reception",
                Info = "Front desk",
                RoleId = 3,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }

    private static void SeedTeachers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>().HasData(
            new Teacher
            {
                Id = 1,
                UserId = 4,
                CompanyId = 1,
                DateOfBirth = new DateOnly(1985, 3, 10),
                Gender = Gender.Male,
                JoiningDate = new DateOnly(2022, 9, 1),
                Qualification = "IELTS 8.5, CELTA certified",
                Info = "Native English speaker with 10 years experience",
                Type = TeacherType.Lead,
                Status = TeacherStatus.Active,
                PaymentType = TeacherSalaryType.Fixed,
                FixSalary = 8000000,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Teacher
            {
                Id = 2,
                UserId = 5,
                CompanyId = 1,
                DateOfBirth = new DateOnly(1988, 7, 25),
                Gender = Gender.Male,
                JoiningDate = new DateOnly(2023, 2, 1),
                Qualification = "PhD in Mathematics",
                Info = "Experienced math teacher, olimpiad coach",
                Type = TeacherType.Lead,
                Status = TeacherStatus.Active,
                PaymentType = TeacherSalaryType.Percentage,
                SalaryPercentPerStudent = 30,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }

    private static void SeedStudents(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                StudentAccessId = "STU-2024-001",
                Balance = 500000,
                DateOfBirth = new DateOnly(2008, 3, 15),
                Gender = Gender.Male,
                Status = StudentStatus.Active,
                UserId = 6,
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Student
            {
                Id = 2,
                StudentAccessId = "STU-2024-002",
                Balance = 750000,
                DateOfBirth = new DateOnly(2007, 8, 22),
                Gender = Gender.Female,
                Status = StudentStatus.Active,
                UserId = 7,
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Student
            {
                Id = 3,
                StudentAccessId = "STU-2024-003",
                Balance = 0,
                DateOfBirth = new DateOnly(2009, 12, 5),
                Gender = Gender.Male,
                Status = StudentStatus.Active,
                UserId = 8,
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Student
            {
                Id = 4,
                StudentAccessId = "STU-2024-004",
                Balance = 1200000,
                DateOfBirth = new DateOnly(2006, 5, 18),
                Gender = Gender.Female,
                Status = StudentStatus.Active,
                UserId = 9,
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Student
            {
                Id = 5,
                StudentAccessId = "STU-2024-005",
                Balance = 300000,
                DateOfBirth = new DateOnly(2008, 11, 30),
                Gender = Gender.Male,
                Status = StudentStatus.Active,
                UserId = 10,
                CompanyId = 1,
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }

    private static void SeedStudentDetails(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentDetail>().HasData(
            new StudentDetail
            {
                Id = 1,
                StudentId = 1,
                FatherFirstName = "Vali",
                FatherLastName = "Valiyev",
                FatherPhone = "+998901010101",
                MotherFirstName = "Gulnora",
                MotherLastName = "Valiyeva",
                MotherPhone = "+998901020102",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new StudentDetail
            {
                Id = 2,
                StudentId = 2,
                FatherFirstName = "Rahim",
                FatherLastName = "Rahimov",
                FatherPhone = "+998902010201",
                MotherFirstName = "Dilnoza",
                MotherLastName = "Rahimova",
                MotherPhone = "+998902020202",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new StudentDetail
            {
                Id = 3,
                StudentId = 3,
                FatherFirstName = "Ergash",
                FatherLastName = "Ergashev",
                FatherPhone = "+998903010301",
                MotherFirstName = "Zulayho",
                MotherLastName = "Ergasheva",
                MotherPhone = "+998903020302",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new StudentDetail
            {
                Id = 4,
                StudentId = 4,
                FatherFirstName = "Usmon",
                FatherLastName = "Usmonov",
                FatherPhone = "+998904010401",
                MotherFirstName = "Feruza",
                MotherLastName = "Usmonova",
                MotherPhone = "+998904020402",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new StudentDetail
            {
                Id = 5,
                StudentId = 5,
                FatherFirstName = "Qodir",
                FatherLastName = "Qodirov",
                FatherPhone = "+998905010501",
                MotherFirstName = "Nodira",
                MotherLastName = "Qodirova",
                MotherPhone = "+998905020502",
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }

    private static void SeedCourses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Name = "English Beginner A1",
                Subject = "English",
                CompanyId = 1,
                Price = 500000,
                MaxStudentCount = 15,
                StudentCount = 3,
                Status = CourseStatus.Active,
                Level = "Beginner",
                StartDate = new DateOnly(2024, 1, 15),
                EndDate = new DateOnly(2024, 6, 15),
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Course
            {
                Id = 2,
                Name = "English Intermediate B1",
                Subject = "English",
                CompanyId = 1,
                Price = 600000,
                MaxStudentCount = 12,
                StudentCount = 2,
                Status = CourseStatus.Active,
                Level = "Intermediate",
                StartDate = new DateOnly(2024, 2, 1),
                EndDate = new DateOnly(2024, 7, 1),
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Course
            {
                Id = 3,
                Name = "Mathematics Advanced",
                Subject = "Mathematics",
                CompanyId = 1,
                Price = 550000,
                MaxStudentCount = 10,
                StudentCount = 2,
                Status = CourseStatus.Active,
                Level = "Advanced",
                StartDate = new DateOnly(2024, 1, 20),
                EndDate = new DateOnly(2024, 6, 20),
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            },
            new Course
            {
                Id = 4,
                Name = "Physics Olympiad Prep",
                Subject = "Physics",
                CompanyId = 1,
                Price = 700000,
                MaxStudentCount = 8,
                StudentCount = 1,
                Status = CourseStatus.Active,
                Level = "Olympiad",
                StartDate = new DateOnly(2024, 3, 1),
                EndDate = new DateOnly(2024, 8, 1),
                CreatedAt = SeedDate,
                UpdatedAt = SeedDate,
                IsDeleted = false
            }
        );
    }
}
