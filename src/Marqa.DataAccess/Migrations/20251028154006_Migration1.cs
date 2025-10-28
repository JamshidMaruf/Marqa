using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LinkUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.OTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.OTPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.PointSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Point = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    QrCode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.PointSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.PointSystemSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Point = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.PointSystemSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsEncrypted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.EmployeeRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.EmployeeRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.EmployeeRoles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentAccessId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    ProfilePicture = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Students_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Subjects_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    JoiningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Specialization = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Info = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Employees_Marqa.Domain.Entities.Emplo~",
                        column: x => x.RoleId,
                        principalTable: "Marqa.Domain.Entities.EmployeeRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Orders_Marqa.Domain.Entities.Students~",
                        column: x => x.StudentId,
                        principalTable: "Marqa.Domain.Entities.Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.StudentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FatherFirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FatherLastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FatherPhone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MotherFirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MotherLastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MotherPhone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    GuardianFirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    GuardianLastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    GuardianPhone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.StudentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentDetails_Marqa.Domain.Entities.~",
                        column: x => x.StudentId,
                        principalTable: "Marqa.Domain.Entities.Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.StudentHomeTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    HomeTaskId = table.Column<int>(type: "integer", nullable: false),
                    Info = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.StudentHomeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentHomeTasks_Marqa.Domain.Entitie~",
                        column: x => x.StudentId,
                        principalTable: "Marqa.Domain.Entities.Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentPointHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    PreviousPoint = table.Column<int>(type: "integer", nullable: false),
                    GivenPoint = table.Column<int>(type: "integer", nullable: false),
                    CurrentPoint = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPointHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPointHistories_Marqa.Domain.Entities.Students_Studen~",
                        column: x => x.StudentId,
                        principalTable: "Marqa.Domain.Entities.Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LessonCount = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    MaxStudentCount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Courses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Courses_Marqa.Domain.Entities.Employe~",
                        column: x => x.TeacherId,
                        principalTable: "Marqa.Domain.Entities.Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Courses_Marqa.Domain.Entities.Subject~",
                        column: x => x.SubjectId,
                        principalTable: "Marqa.Domain.Entities.Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.TeacherSubjects",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.TeacherSubjects", x => new { x.SubjectId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.TeacherSubjects_Marqa.Domain.Entities~",
                        column: x => x.SubjectId,
                        principalTable: "Marqa.Domain.Entities.Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.TeacherSubjects_Marqa.Domain.Entitie~1",
                        column: x => x.TeacherId,
                        principalTable: "Marqa.Domain.Entities.Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    InlinePrice = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.OrderItems_Marqa.Domain.Entities.Orde~",
                        column: x => x.OrderId,
                        principalTable: "Marqa.Domain.Entities.Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.OrderItems_Marqa.Domain.Entities.Prod~",
                        column: x => x.ProductId,
                        principalTable: "Marqa.Domain.Entities.Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FeedBack = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    StudentHomeTaskId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_Marqa.Domain~",
                        column: x => x.StudentHomeTaskId,
                        principalTable: "Marqa.Domain.Entities.StudentHomeTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_Marqa.Domai~1",
                        column: x => x.TeacherId,
                        principalTable: "Marqa.Domain.Entities.Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StudentHomeTaskId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.StudentHomeTaskFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentHomeTaskFiles_Marqa.Domain.Ent~",
                        column: x => x.StudentHomeTaskId,
                        principalTable: "Marqa.Domain.Entities.StudentHomeTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.CourseWeekdays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Weekday = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.CourseWeekdays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.CourseWeekdays_Marqa.Domain.Entities.~",
                        column: x => x.CourseId,
                        principalTable: "Marqa.Domain.Entities.Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Exams_Marqa.Domain.Entities.Courses_C~",
                        column: x => x.CourseId,
                        principalTable: "Marqa.Domain.Entities.Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Room = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    HomeTaskStatus = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Lessons_Marqa.Domain.Entities.Courses~",
                        column: x => x.CourseId,
                        principalTable: "Marqa.Domain.Entities.Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.Lessons_Marqa.Domain.Entities.Employe~",
                        column: x => x.TeacherId,
                        principalTable: "Marqa.Domain.Entities.Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.StudentCourses",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.StudentCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentCourses_Marqa.Domain.Entities.~",
                        column: x => x.CourseId,
                        principalTable: "Marqa.Domain.Entities.Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentCourses_Marqa.Domain.Entities~1",
                        column: x => x.StudentId,
                        principalTable: "Marqa.Domain.Entities.Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.StudentExamResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    ExamId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<double>(type: "double precision", nullable: false),
                    TeacherFeedback = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.StudentExamResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentExamResults_Marqa.Domain.Entit~",
                        column: x => x.ExamId,
                        principalTable: "Marqa.Domain.Entities.Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.StudentExamResults_Marqa.Domain.Enti~1",
                        column: x => x.StudentId,
                        principalTable: "Marqa.Domain.Entities.Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.HomeTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.HomeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.HomeTasks_Marqa.Domain.Entities.Lesso~",
                        column: x => x.LessonId,
                        principalTable: "Marqa.Domain.Entities.Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.LessonAttendances",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    LateTimeInMinutes = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.LessonAttendances", x => new { x.StudentId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.LessonAttendances_Marqa.Domain.Entiti~",
                        column: x => x.LessonId,
                        principalTable: "Marqa.Domain.Entities.Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.LessonAttendances_Marqa.Domain.Entit~1",
                        column: x => x.StudentId,
                        principalTable: "Marqa.Domain.Entities.Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.LessonFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.LessonFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.LessonFiles_Marqa.Domain.Entities.Les~",
                        column: x => x.LessonId,
                        principalTable: "Marqa.Domain.Entities.Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.LessonVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.LessonVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.LessonVideos_Marqa.Domain.Entities.Le~",
                        column: x => x.LessonId,
                        principalTable: "Marqa.Domain.Entities.Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marqa.Domain.Entities.HomeTaskFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    HomeTaskId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marqa.Domain.Entities.HomeTaskFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marqa.Domain.Entities.HomeTaskFiles_Marqa.Domain.Entities.H~",
                        column: x => x.HomeTaskId,
                        principalTable: "Marqa.Domain.Entities.HomeTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Result School", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cambridge school", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Pdp Academy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Najot Ta'lim", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Settings",
                columns: new[] { "Id", "Category", "CreatedAt", "DeletedAt", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, "JWT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JWT.Issuer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://marqa.uz" },
                    { 2, "JWT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JWT.Audience", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://marqa.uz" },
                    { 3, "JWT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JWT.Key", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "949eddf8-4560-4cf2-8efe-2f6daea075e9" },
                    { 4, "JWT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JWT.Expires", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "24" },
                    { 5, "Eskiz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eskiz.Email", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wonderboy1w3@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Settings",
                columns: new[] { "Id", "Category", "CreatedAt", "DeletedAt", "IsEncrypted", "Key", "UpdatedAt", "Value" },
                values: new object[] { 6, "Eskiz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Eskiz.SecretKey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kWq42ByF3HK+8IL9H6qlL4LVzwTS5VF9dfd41ePZ9T2khUGm9AO6ju1aRVIvnCUr" });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Settings",
                columns: new[] { "Id", "Category", "CreatedAt", "DeletedAt", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 7, "Eskiz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eskiz.From", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4546" },
                    { 8, "Eskiz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eskiz.SendMessageUrl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://notify.eskiz.uz/api/message/sms/send" },
                    { 9, "Eskiz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eskiz.LoginUrl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://notify.eskiz.uz/api/auth/login" },
                    { 10, "App", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StudentApp.AppId", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ffae892ea37a4cb2b029da12957df65a" },
                    { 11, "App", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StudentApp.SecretKey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OwN7bATuNDPZCzTMw1Ua4g==" },
                    { 12, "App", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeacherApp.SecretKey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4Xdu1cmIFi3P7GvGvFj3Zg==" },
                    { 13, "App", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TeacherApp.AppId", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bb52e3fd30f84ece8bd3db686b701104" },
                    { 14, "App", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ParentApp.SecretKey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LHpMHeBIvvjxBmZOUDqg1A==" },
                    { 15, "App", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ParentApp.AppId", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "71286a64bc1b4c41beadbed6c0c973ec" }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.EmployeeRoles",
                columns: new[] { "Id", "CompanyId", "CreatedAt", "DeletedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Teacher", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Teacher", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Teacher", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Teacher", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Students",
                columns: new[] { "Id", "CompanyId", "CreatedAt", "DateOfBirth", "DeletedAt", "Email", "FirstName", "Gender", "IsDeleted", "LastName", "PasswordHash", "Phone", "ProfilePicture", "StudentAccessId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "zzz777@gmail.com", "Zokirjon", 1, false, "Tulqunov", "hashlangan password", "+998900000000", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dilya003@gmail.com", "Dilmurod", 1, false, "Jabborov", "hashlangan password", "+998975771111", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xasanchik007@gmail.com", "Xasanxon", 1, false, "Savriddinov", "hashlangan password", "+998944441111", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "murodxon1@gmail.com", "Murodjon", 1, false, "Sharobiddinov", "hashlangan password", "+998933331111", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Subjects",
                columns: new[] { "Id", "CompanyId", "CreatedAt", "DeletedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "backend development", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Mobile Delevopment", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "English", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "English", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Employees",
                columns: new[] { "Id", "CompanyId", "CreatedAt", "DateOfBirth", "DeletedAt", "Email", "FirstName", "Gender", "Info", "IsDeleted", "JoiningDate", "LastName", "PasswordHash", "Phone", "RoleId", "Specialization", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wonderboy3@gmail.com", "Jamshid", 1, "ajoyib", false, new DateOnly(2020, 8, 8), "Ho'jaqulov", "hashlangan password", "+998975777552", 1, "Software engineering", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KarimBoy@gmail.com", "Muhammad Karim", 1, "MVP", false, new DateOnly(2020, 8, 8), "To'xtaboyev", "hashlangan password", "+998975771111", 1, "Computer Science", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AbdumalikA@gmail.com", "Abdumalik", 1, "Niner", false, new DateOnly(2021, 8, 8), "Abdulvohidov", "hashlangan password", "+998922221111", 1, "Teaching English", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AbdumalikA@gmail.com", "Pismadonchi", 1, "Niner", false, new DateOnly(2021, 8, 8), "Palonchiyev", "hashlangan password", "+998922221111", 1, "Teaching English", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Courses",
                columns: new[] { "Id", "CompanyId", "CreatedAt", "DeletedAt", "Description", "EndTime", "IsDeleted", "LessonCount", "MaxStudentCount", "Name", "StartDate", "StartTime", "Status", "SubjectId", "TeacherId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zo'r kurs", new TimeOnly(18, 0, 0), false, 72, 24, ".Net C#", new DateOnly(2025, 10, 1), new TimeOnly(15, 0, 0), 1, 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hali bunaqasi bo'lmagan", new TimeOnly(18, 0, 0), false, 80, 20, "Flutter butcamp", new DateOnly(2025, 11, 1), new TimeOnly(15, 0, 0), 2, 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zo'r kurs", new TimeOnly(18, 0, 0), false, 72, 24, "Intensive Ielts 1", new DateOnly(2025, 10, 1), new TimeOnly(15, 0, 0), 1, 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zo'r kurs", new TimeOnly(13, 0, 0), false, 72, 24, "General English", new DateOnly(2025, 11, 1), new TimeOnly(11, 0, 0), 2, 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.TeacherSubjects",
                columns: new[] { "SubjectId", "TeacherId", "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.Lessons",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Date", "DeletedAt", "EndTime", "HomeTaskStatus", "IsCompleted", "IsDeleted", "Name", "Number", "Room", "StartTime", "TeacherId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2025, 10, 1), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeOnly(18, 0, 0), 0, false, false, "", 1, "uber", new TimeOnly(15, 0, 0), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2025, 10, 1), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeOnly(18, 0, 0), 1, false, false, "", 1, "yandex", new TimeOnly(15, 0, 0), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2025, 10, 1), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeOnly(18, 0, 0), 1, false, false, "", 1, "uber", new TimeOnly(15, 0, 0), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2025, 10, 1), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeOnly(18, 0, 0), 0, false, false, "", 1, "uber", new TimeOnly(15, 0, 0), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.StudentCourses",
                columns: new[] { "CourseId", "StudentId", "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.HomeTasks",
                columns: new[] { "Id", "CreatedAt", "Deadline", "DeletedAt", "Description", "IsDeleted", "LessonId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description", false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description", false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 7, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description", false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description", false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Marqa.Domain.Entities.LessonAttendances",
                columns: new[] { "LessonId", "StudentId", "CreatedAt", "DeletedAt", "IsDeleted", "LateTimeInMinutes", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 10, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Courses_CompanyId",
                table: "Marqa.Domain.Entities.Courses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Courses_SubjectId",
                table: "Marqa.Domain.Entities.Courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Courses_TeacherId",
                table: "Marqa.Domain.Entities.Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.CourseWeekdays_CourseId",
                table: "Marqa.Domain.Entities.CourseWeekdays",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.EmployeeRoles_CompanyId_Name",
                table: "Marqa.Domain.Entities.EmployeeRoles",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Employees_CompanyId",
                table: "Marqa.Domain.Entities.Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Employees_RoleId",
                table: "Marqa.Domain.Entities.Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Exams_CourseId",
                table: "Marqa.Domain.Entities.Exams",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.HomeTaskFiles_HomeTaskId",
                table: "Marqa.Domain.Entities.HomeTaskFiles",
                column: "HomeTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.HomeTasks_LessonId",
                table: "Marqa.Domain.Entities.HomeTasks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.LessonAttendances_LessonId",
                table: "Marqa.Domain.Entities.LessonAttendances",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.LessonFiles_LessonId",
                table: "Marqa.Domain.Entities.LessonFiles",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Lessons_CourseId",
                table: "Marqa.Domain.Entities.Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Lessons_TeacherId",
                table: "Marqa.Domain.Entities.Lessons",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.LessonVideos_LessonId",
                table: "Marqa.Domain.Entities.LessonVideos",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.OrderItems_OrderId",
                table: "Marqa.Domain.Entities.OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.OrderItems_ProductId",
                table: "Marqa.Domain.Entities.OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Orders_StudentId",
                table: "Marqa.Domain.Entities.Orders",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Products_CompanyId",
                table: "Marqa.Domain.Entities.Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Products_Name",
                table: "Marqa.Domain.Entities.Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Settings_Key",
                table: "Marqa.Domain.Entities.Settings",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentCourses_CourseId",
                table: "Marqa.Domain.Entities.StudentCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentDetails_StudentId",
                table: "Marqa.Domain.Entities.StudentDetails",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentExamResults_ExamId",
                table: "Marqa.Domain.Entities.StudentExamResults",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentExamResults_StudentId",
                table: "Marqa.Domain.Entities.StudentExamResults",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_StudentHomeT~",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                column: "StudentHomeTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_TeacherId",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTaskFiles_StudentHomeTaskId",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                column: "StudentHomeTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTasks_StudentId",
                table: "Marqa.Domain.Entities.StudentHomeTasks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Students_CompanyId",
                table: "Marqa.Domain.Entities.Students",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.Subjects_CompanyId",
                table: "Marqa.Domain.Entities.Subjects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Marqa.Domain.Entities.TeacherSubjects_TeacherId",
                table: "Marqa.Domain.Entities.TeacherSubjects",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentPointHistories_StudentId",
                table: "StudentPointHistories",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Banners");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.CourseWeekdays");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.HomeTaskFiles");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.LessonAttendances");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.LessonFiles");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.LessonVideos");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.OrderItems");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.OTPs");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.PointSettings");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.PointSystemSettings");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Settings");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.StudentCourses");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.StudentDetails");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.StudentExamResults");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.StudentHomeTaskFiles");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.TeacherSubjects");

            migrationBuilder.DropTable(
                name: "StudentPointHistories");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.HomeTasks");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Orders");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Products");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Exams");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.StudentHomeTasks");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Lessons");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Students");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Courses");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Employees");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.Subjects");

            migrationBuilder.DropTable(
                name: "Marqa.Domain.Entities.EmployeeRoles");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
