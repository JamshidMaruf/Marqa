using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ss : Migration
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
                name: "StudentCourses",
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
                    table.PrimaryKey("PK_StudentCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Marqa.Domain.Entities.Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Marqa.Domain.Entities.Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Marqa.Domain.Entities.Students_StudentId",
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
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");

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
                name: "StudentCourses");

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
