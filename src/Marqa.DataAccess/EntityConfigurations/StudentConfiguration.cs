using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(p => p.PasswordHash)
            .HasMaxLength(400);

        builder.HasOne(s => s.Company)
            .WithMany()
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(s => s.StudentDetail)
            .WithOne(sd => sd.Student)
            .HasForeignKey<StudentDetail>(sd => sd.StudentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(new List<Student>
        {
            new()
            {
                Id = 1,
                FirstName = "Zokirjon",
                LastName = "Tulqunov",
                CompanyId = 4,
                DateOfBirth = new DateTime(2006,01,01),
                Email = "zzz777@gmail.com",
                Gender = Gender.Male,
                PasswordHash = "hashlangan password",
                Phone = "+998900000000",
            },
            new()
            {
                Id = 2,
                FirstName = "Dilmurod",
                LastName = "Jabborov",
                CompanyId = 4,
                DateOfBirth = new DateTime(1999,01,01),
                Email = "dilya003@gmail.com",
                Gender = Gender.Male,
                PasswordHash = "hashlangan password",
                Phone = "+998975771111",
            },
            new()
            {
                Id = 3,
                FirstName = "Xasanxon",
                LastName = "Savriddinov",
                CompanyId = 4,
                DateOfBirth = new DateTime(2002,01,01),
                Email = "Xasanchik007@gmail.com",
                Gender = Gender.Male,
                PasswordHash = "hashlangan password",
                Phone = "+998944441111",
            },
            new()
            {
                Id = 4,
                FirstName = "Murodjon",
                LastName = "Sharobiddinov",
                CompanyId = 4,
                DateOfBirth = new DateTime(2002,01,01),
                Email = "murodxon1@gmail.com",
                Gender = Gender.Male,
                PasswordHash = "hashlangan password",
                Phone = "+998933331111",
            }
        });
    }
}
