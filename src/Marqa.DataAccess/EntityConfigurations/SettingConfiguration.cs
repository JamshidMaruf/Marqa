using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.Property(s => s.Key).IsRequired();
        builder.Property(s => s.Value).IsRequired();
        builder.Property(s => s.Category).IsRequired();

        builder.HasIndex(s => s.Key).IsUnique();

        builder.Property(s => s.IsEncrypted).HasDefaultValue(false);

        builder.Property(s => s.IsDeleted).HasDefaultValue(false);

        builder.HasData(new List<Setting>()
        {
            // JWT
            new Setting
            {
                Id = 1,
                Key = "JWT.Issuer",
                Value = "https://marqa.uz",
                Category = "JWT",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 2,
                Key = "JWT.Audience",
                Value = "https://marqa.uz",
                Category = "JWT",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 3,
                Key = "JWT.Key",
                Value = "949eddf8-4560-4cf2-8efe-2f6daea075e9",
                Category = "JWT",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 4,
                Key = "JWT.Expires",
                Value = "24",
                Category = "JWT",
                IsEncrypted = false,
                IsDeleted = false
            },
            
            // Eskiz
            new Setting
            {
                Id = 5,
                Key = "Eskiz.Email",
                Value = "wonderboy1w3@gmail.com",
                Category = "Eskiz",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 6,
                Key = "Eskiz.SecretKey",
                Value = "kWq42ByF3HK+8IL9H6qlL4LVzwTS5VF9dfd41ePZ9T2khUGm9AO6ju1aRVIvnCUr",
                Category = "Eskiz",
                IsEncrypted = true,
                IsDeleted = false
            },
            new Setting
            {
                Id = 7,
                Key = "Eskiz.From",
                Value = "4546",
                Category = "Eskiz",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 8,
                Key = "Eskiz.SendMessageUrl",
                Value = "https://notify.eskiz.uz/api/message/sms/send",
                Category = "Eskiz",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 9,
                Key = "Eskiz.LoginUrl",
                Value = "https://notify.eskiz.uz/api/auth/login",
                Category = "Eskiz",
                IsEncrypted = false,
                IsDeleted = false
            },
            
            // StudentApp
            new Setting
            {
                Id = 10,
                Key = "StudentApp.AppId",
                Value = "ffae892ea37a4cb2b029da12957df65a",
                Category = "App",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 11,
                Key = "StudentApp.SecretKey",
                Value = "OwN7bATuNDPZCzTMw1Ua4g==",
                Category = "App",
                IsEncrypted = false,
                IsDeleted = false
            },
            
            // TeacherApp
            new Setting
            {
                Id = 12,
                Key = "TeacherApp.SecretKey",
                Value = "4Xdu1cmIFi3P7GvGvFj3Zg==",
                Category = "App",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 13,
                Key = "TeacherApp.AppId",
                Value = "bb52e3fd30f84ece8bd3db686b701104",
                Category = "App",
                IsEncrypted = false,
                IsDeleted = false
            },
            
            // ParentApp
            new Setting
            {
                Id = 14,
                Key = "ParentApp.SecretKey",
                Value = "LHpMHeBIvvjxBmZOUDqg1A==",
                Category = "App",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 15,
                Key = "ParentApp.AppId",
                Value = "71286a64bc1b4c41beadbed6c0c973ec",
                Category = "App",
                IsEncrypted = false,
                IsDeleted = false
            },

            // RefreshToken
            new Setting
            {
                Id = 16,
                Key = "RefreshToken.Expires.RememberMe",
                Value = "30",
                Category = "RefreshToken",
                IsEncrypted = false,
                IsDeleted = false
            },
            new Setting
            {
                Id = 17,
                Key = "RefreshToken.Expires.Standard",
                Value = "7",
                Category = "RefreshToken",
                IsEncrypted = false,
                IsDeleted = false
            },
        });
    }
}