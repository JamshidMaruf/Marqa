using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;
public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // builder.HasData(new List<Permission>
        // {
        //     new()
        //     {
        //         Id = 1,
        //         Name = "View.Users",
        //         Module = "Users",
        //         Action = "Read",
        //         Description = "Allows viewing the list of users",
        //         CreatedAt = DateTime.UtcNow
        //     },
        //     new()
        //     {
        //          Id = 2,
        //          Name = "Create.User",
        //          Module = "Users",
        //          Action = "Create",
        //          Description = "Allows creating new users",
        //          CreatedAt = DateTime.UtcNow
        //     },
        //     new()
        //     {
        //          Id = 3,
        //          Name = "Update.User",
        //          Module = "Users",
        //          Action = "Update",
        //          Description = "Allows updating existing users",
        //          CreatedAt = DateTime.UtcNow
        //     },
        //     new()
        //     {
        //          Id = 4,
        //          Name = "Delete.User",
        //          Module = "Users",
        //          Action = "Delete",
        //          Description = "Allows deleting users",
        //          CreatedAt = DateTime.UtcNow
        //     },
        //     new()
        //     {
        //          Id = 5,
        //          Name = "View.Roles",
        //          Module = "Roles",
        //          Action = "Read",
        //          Description = "Allows viewing roles",
        //          CreatedAt = DateTime.UtcNow
        //     },
        //     new()
        //     {
        //          Id = 6,
        //          Name = "Create.Role",
        //          Module = "Roles",
        //          Action = "Create",
        //          Description = "Allows creating roles",
        //          CreatedAt = DateTime.UtcNow
        //     },
        //     new()
        //     {
        //          Id = 7,
        //          Name = "Update.Role",
        //          Module = "Roles",
        //          Action = "Update",
        //          Description = "Allows updating roles",
        //          CreatedAt = DateTime.UtcNow
        //     },
        //     new()
        //     {
        //          Id = 8,
        //          Name = "Delete.Role",
        //          Module = "Roles",
        //          Action = "Delete",
        //          Description = "Allows deleting roles",
        //          CreatedAt = DateTime.UtcNow
        //     },
        //     // new()
        //     // {
        //     //      Id = 9,
        //     //      Name = "View.Companies",
        //     //      Module = "Companies",
        //     //      Action = "Read",
        //     //      Description = "Allows viewing company list",
        //     //      CreatedAt = DateTime.UtcNow
        //     // },
        //     // new()
        //     // {
        //     //      Id = 10,
        //     //      Name = "Create.Company",
        //     //      Module = "Companies",
        //     //      Action = "Create",
        //     //      Description = "Allows adding new company",
        //     //      CreatedAt = DateTime.UtcNow
        //     // },
        //     // new()
        //     // {
        //     //      Id = 11,
        //     //      Name = "Update.Company",
        //     //      Module = "Companies",
        //     //      Action = "Update",
        //     //      Description = "Allows editing company data",
        //     //      CreatedAt = DateTime.UtcNow
        //     // },
        //     // new()
        //     // {
        //     //      Id = 12,
        //     //      Name = "Delete.Company",
        //     //      Module = "Companies",
        //     //      Action = "Delete",
        //     //      Description = "Allows removing companies",
        //     //      CreatedAt = DateTime.UtcNow
        //     // }
        // });
    }
}
