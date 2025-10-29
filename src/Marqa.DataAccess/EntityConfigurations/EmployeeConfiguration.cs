using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(p => p.Info)
            .HasMaxLength(4000);

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(e => e.Role)
            .WithMany()
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasData(new List<Employee>
        {
            new()
            {
                Id = 1,
                FirstName = "Jamshid",
                LastName = "Ho'jaqulov",
                Status = EmployeeStatus.Active,
                CompanyId = 3,
                DateOfBirth = new DateOnly(2001,01,01),
                Email = "wonderboy3@gmail.com",
                Gender = Gender.Male,  
                Info = "ajoyib",
                PasswordHash = "hashlangan password",
                JoiningDate = new DateOnly(2020,08,08),
                Phone = "+998975777552",
                Specialization = "Software engineering",
                RoleId = 1            
            },
            new()
            {
                Id = 2,
                FirstName = "Muhammad Karim",
                LastName = "To'xtaboyev",
                Status = EmployeeStatus.Active,
                CompanyId = 4,
                DateOfBirth = new DateOnly(1999,01,01),
                Email = "KarimBoy@gmail.com",
                Gender = Gender.Male,
                Info = "MVP",
                PasswordHash = "hashlangan password",
                JoiningDate = new DateOnly(2020,08,08),
                Phone = "+998975771111",
                Specialization = "Computer Science",
                RoleId = 1
            },
            new()
            {
                Id = 3,
                FirstName = "Abdumalik",
                LastName = "Abdulvohidov",
                Status = EmployeeStatus.Active,
                CompanyId = 1,
                DateOfBirth = new DateOnly(2002,01,01),
                Email = "AbdumalikA@gmail.com",
                Gender = Gender.Male,
                Info = "Niner",
                PasswordHash = "hashlangan password",
                JoiningDate = new DateOnly(2021,08,08),
                Phone = "+998922221111",
                Specialization = "Teaching English",
                RoleId = 1
            },
            new()
            {
                Id = 4,
                FirstName = "Pismadonchi",
                LastName = "Palonchiyev",
                Status = EmployeeStatus.Active,
                CompanyId = 2,
                DateOfBirth = new DateOnly(2002,01,01),
                Email = "AbdumalikA@gmail.com",
                Gender = Gender.Male,
                Info = "Niner",
                PasswordHash = "hashlangan password",
                JoiningDate = new DateOnly(2021,08,08),
                Phone = "+998922221111",
                Specialization = "Teaching English",
                RoleId = 1
            }
        });

    }
}
