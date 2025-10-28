using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sdfdfdssdsdss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Company_CompanyId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Subject_SubjectId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseWeekday_Courses_CourseId",
                table: "CourseWeekday");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRole_Company_CompanyId",
                table: "EmployeeRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Company_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRole_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Courses_CourseId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeTask_Lesson_LessonId",
                table: "HomeTask");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeTaskFile_HomeTask_HomeTaskId",
                table: "HomeTaskFile");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Courses_CourseId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Employees_TeacherId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonAttendance_Lesson_LessonId",
                table: "LessonAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonAttendance_Student_StudentId",
                table: "LessonAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonFile_Lesson_LessonId",
                table: "LessonFile");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonVideo_Lesson_LessonId",
                table: "LessonVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Student_StudentId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Company_CompanyId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Company_CompanyId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Courses_CourseId",
                table: "StudentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Student_StudentId",
                table: "StudentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentDetail_Student_StudentId",
                table: "StudentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamResult_Exam_ExamId",
                table: "StudentExamResult");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamResult_Student_StudentId",
                table: "StudentExamResult");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHomeTask_Student_StudentId",
                table: "StudentHomeTask");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHomeTaskFeedback_Employees_TeacherId",
                table: "StudentHomeTaskFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHomeTaskFeedback_StudentHomeTask_StudentHomeTaskId",
                table: "StudentHomeTaskFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHomeTaskFile_StudentHomeTask_StudentHomeTaskId",
                table: "StudentHomeTaskFile");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPointHistory_Student_StudentId",
                table: "StudentPointHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Company_CompanyId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Employees_TeacherId",
                table: "TeacherSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Subject_SubjectId",
                table: "TeacherSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubject",
                table: "TeacherSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPointHistory",
                table: "StudentPointHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentHomeTaskFile",
                table: "StudentHomeTaskFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentHomeTaskFeedback",
                table: "StudentHomeTaskFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentHomeTask",
                table: "StudentHomeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExamResult",
                table: "StudentExamResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentDetail",
                table: "StudentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourse",
                table: "StudentCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Setting",
                table: "Setting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointSystemSetting",
                table: "PointSystemSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointSetting",
                table: "PointSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OTP",
                table: "OTP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonVideo",
                table: "LessonVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonFile",
                table: "LessonFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonAttendance",
                table: "LessonAttendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeTaskFile",
                table: "HomeTaskFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeTask",
                table: "HomeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exam",
                table: "Exam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRole",
                table: "EmployeeRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseWeekday",
                table: "CourseWeekday");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Banner",
                table: "Banner");

            migrationBuilder.RenameTable(
                name: "TeacherSubject",
                newName: "Marqa.Domain.Entities.TeacherSubjects");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Marqa.Domain.Entities.Subjects");

            migrationBuilder.RenameTable(
                name: "StudentPointHistory",
                newName: "Marqa.Domain.Entities.StudentPointHistorys");

            migrationBuilder.RenameTable(
                name: "StudentHomeTaskFile",
                newName: "Marqa.Domain.Entities.StudentHomeTaskFiles");

            migrationBuilder.RenameTable(
                name: "StudentHomeTaskFeedback",
                newName: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks");

            migrationBuilder.RenameTable(
                name: "StudentHomeTask",
                newName: "Marqa.Domain.Entities.StudentHomeTasks");

            migrationBuilder.RenameTable(
                name: "StudentExamResult",
                newName: "Marqa.Domain.Entities.StudentExamResults");

            migrationBuilder.RenameTable(
                name: "StudentDetail",
                newName: "Marqa.Domain.Entities.StudentDetails");

            migrationBuilder.RenameTable(
                name: "StudentCourse",
                newName: "Marqa.Domain.Entities.StudentCourses");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Marqa.Domain.Entities.Students");

            migrationBuilder.RenameTable(
                name: "Setting",
                newName: "Marqa.Domain.Entities.Settings");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Marqa.Domain.Entities.Products");

            migrationBuilder.RenameTable(
                name: "PointSystemSetting",
                newName: "Marqa.Domain.Entities.PointSystemSettings");

            migrationBuilder.RenameTable(
                name: "PointSetting",
                newName: "Marqa.Domain.Entities.PointSettings");

            migrationBuilder.RenameTable(
                name: "OTP",
                newName: "Marqa.Domain.Entities.OTPs");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "Marqa.Domain.Entities.OrderItems");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Marqa.Domain.Entities.Orders");

            migrationBuilder.RenameTable(
                name: "LessonVideo",
                newName: "Marqa.Domain.Entities.LessonVideos");

            migrationBuilder.RenameTable(
                name: "LessonFile",
                newName: "Marqa.Domain.Entities.LessonFiles");

            migrationBuilder.RenameTable(
                name: "LessonAttendance",
                newName: "Marqa.Domain.Entities.LessonAttendances");

            migrationBuilder.RenameTable(
                name: "Lesson",
                newName: "Marqa.Domain.Entities.Lessons");

            migrationBuilder.RenameTable(
                name: "HomeTaskFile",
                newName: "Marqa.Domain.Entities.HomeTaskFiles");

            migrationBuilder.RenameTable(
                name: "HomeTask",
                newName: "Marqa.Domain.Entities.HomeTasks");

            migrationBuilder.RenameTable(
                name: "Exam",
                newName: "Marqa.Domain.Entities.Exams");

            migrationBuilder.RenameTable(
                name: "EmployeeRole",
                newName: "Marqa.Domain.Entities.EmployeeRoles");

            migrationBuilder.RenameTable(
                name: "CourseWeekday",
                newName: "Marqa.Domain.Entities.CourseWeekdays");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Marqa.Domain.Entities.Companys");

            migrationBuilder.RenameTable(
                name: "Banner",
                newName: "Marqa.Domain.Entities.Banners");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubject_TeacherId",
                table: "Marqa.Domain.Entities.TeacherSubjects",
                newName: "IX_Marqa.Domain.Entities.TeacherSubjects_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_CompanyId",
                table: "Marqa.Domain.Entities.Subjects",
                newName: "IX_Marqa.Domain.Entities.Subjects_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentPointHistory_StudentId",
                table: "Marqa.Domain.Entities.StudentPointHistorys",
                newName: "IX_Marqa.Domain.Entities.StudentPointHistorys_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentHomeTaskFile_StudentHomeTaskId",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                newName: "IX_Marqa.Domain.Entities.StudentHomeTaskFiles_StudentHomeTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentHomeTaskFeedback_TeacherId",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                newName: "IX_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentHomeTaskFeedback_StudentHomeTaskId",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                newName: "IX_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_StudentHomeT~");

            migrationBuilder.RenameIndex(
                name: "IX_StudentHomeTask_StudentId",
                table: "Marqa.Domain.Entities.StudentHomeTasks",
                newName: "IX_Marqa.Domain.Entities.StudentHomeTasks_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExamResult_StudentId",
                table: "Marqa.Domain.Entities.StudentExamResults",
                newName: "IX_Marqa.Domain.Entities.StudentExamResults_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExamResult_ExamId",
                table: "Marqa.Domain.Entities.StudentExamResults",
                newName: "IX_Marqa.Domain.Entities.StudentExamResults_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDetail_StudentId",
                table: "Marqa.Domain.Entities.StudentDetails",
                newName: "IX_Marqa.Domain.Entities.StudentDetails_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourse_CourseId",
                table: "Marqa.Domain.Entities.StudentCourses",
                newName: "IX_Marqa.Domain.Entities.StudentCourses_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_CompanyId",
                table: "Marqa.Domain.Entities.Students",
                newName: "IX_Marqa.Domain.Entities.Students_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Setting_Key",
                table: "Marqa.Domain.Entities.Settings",
                newName: "IX_Marqa.Domain.Entities.Settings_Key");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CompanyId",
                table: "Marqa.Domain.Entities.Products",
                newName: "IX_Marqa.Domain.Entities.Products_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ProductId",
                table: "Marqa.Domain.Entities.OrderItems",
                newName: "IX_Marqa.Domain.Entities.OrderItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "Marqa.Domain.Entities.OrderItems",
                newName: "IX_Marqa.Domain.Entities.OrderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StudentId",
                table: "Marqa.Domain.Entities.Orders",
                newName: "IX_Marqa.Domain.Entities.Orders_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonVideo_LessonId",
                table: "Marqa.Domain.Entities.LessonVideos",
                newName: "IX_Marqa.Domain.Entities.LessonVideos_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonFile_LessonId",
                table: "Marqa.Domain.Entities.LessonFiles",
                newName: "IX_Marqa.Domain.Entities.LessonFiles_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonAttendance_LessonId",
                table: "Marqa.Domain.Entities.LessonAttendances",
                newName: "IX_Marqa.Domain.Entities.LessonAttendances_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_TeacherId",
                table: "Marqa.Domain.Entities.Lessons",
                newName: "IX_Marqa.Domain.Entities.Lessons_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_CourseId",
                table: "Marqa.Domain.Entities.Lessons",
                newName: "IX_Marqa.Domain.Entities.Lessons_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeTaskFile_HomeTaskId",
                table: "Marqa.Domain.Entities.HomeTaskFiles",
                newName: "IX_Marqa.Domain.Entities.HomeTaskFiles_HomeTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeTask_LessonId",
                table: "Marqa.Domain.Entities.HomeTasks",
                newName: "IX_Marqa.Domain.Entities.HomeTasks_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Exam_CourseId",
                table: "Marqa.Domain.Entities.Exams",
                newName: "IX_Marqa.Domain.Entities.Exams_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRole_Name_CompanyId",
                table: "Marqa.Domain.Entities.EmployeeRoles",
                newName: "IX_Marqa.Domain.Entities.EmployeeRoles_Name_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRole_CompanyId",
                table: "Marqa.Domain.Entities.EmployeeRoles",
                newName: "IX_Marqa.Domain.Entities.EmployeeRoles_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseWeekday_CourseId",
                table: "Marqa.Domain.Entities.CourseWeekdays",
                newName: "IX_Marqa.Domain.Entities.CourseWeekdays_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Marqa.Domain.Entities.Subjects",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Marqa.Domain.Entities.StudentPointHistorys",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Marqa.Domain.Entities.StudentHomeTasks",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotherPhone",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotherLastName",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotherFirstName",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianPhone",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianLastName",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianFirstName",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FatherPhone",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FatherLastName",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FatherFirstName",
                table: "Marqa.Domain.Entities.StudentDetails",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentAccessId",
                table: "Marqa.Domain.Entities.Students",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Marqa.Domain.Entities.Students",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Marqa.Domain.Entities.Students",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Marqa.Domain.Entities.Students",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Marqa.Domain.Entities.Students",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Marqa.Domain.Entities.Settings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Marqa.Domain.Entities.Settings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Marqa.Domain.Entities.Settings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Marqa.Domain.Entities.Products",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Marqa.Domain.Entities.PointSystemSettings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QrCode",
                table: "Marqa.Domain.Entities.PointSettings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Marqa.Domain.Entities.PointSettings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Marqa.Domain.Entities.PointSettings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Marqa.Domain.Entities.OTPs",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Marqa.Domain.Entities.OTPs",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Marqa.Domain.Entities.LessonVideos",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Marqa.Domain.Entities.LessonVideos",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Marqa.Domain.Entities.LessonFiles",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Marqa.Domain.Entities.LessonFiles",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Room",
                table: "Marqa.Domain.Entities.Lessons",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Marqa.Domain.Entities.Lessons",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Marqa.Domain.Entities.HomeTaskFiles",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Marqa.Domain.Entities.HomeTaskFiles",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Marqa.Domain.Entities.Exams",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Marqa.Domain.Entities.EmployeeRoles",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Marqa.Domain.Entities.Companys",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Marqa.Domain.Entities.Banners",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkUrl",
                table: "Marqa.Domain.Entities.Banners",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Marqa.Domain.Entities.Banners",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Marqa.Domain.Entities.Banners",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.TeacherSubjects",
                table: "Marqa.Domain.Entities.TeacherSubjects",
                columns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Subjects",
                table: "Marqa.Domain.Entities.Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentPointHistorys",
                table: "Marqa.Domain.Entities.StudentPointHistorys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentHomeTaskFiles",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentHomeTasks",
                table: "Marqa.Domain.Entities.StudentHomeTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentExamResults",
                table: "Marqa.Domain.Entities.StudentExamResults",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentDetails",
                table: "Marqa.Domain.Entities.StudentDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentCourses",
                table: "Marqa.Domain.Entities.StudentCourses",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Students",
                table: "Marqa.Domain.Entities.Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Settings",
                table: "Marqa.Domain.Entities.Settings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Products",
                table: "Marqa.Domain.Entities.Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.PointSystemSettings",
                table: "Marqa.Domain.Entities.PointSystemSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.PointSettings",
                table: "Marqa.Domain.Entities.PointSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.OTPs",
                table: "Marqa.Domain.Entities.OTPs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.OrderItems",
                table: "Marqa.Domain.Entities.OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Orders",
                table: "Marqa.Domain.Entities.Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.LessonVideos",
                table: "Marqa.Domain.Entities.LessonVideos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.LessonFiles",
                table: "Marqa.Domain.Entities.LessonFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.LessonAttendances",
                table: "Marqa.Domain.Entities.LessonAttendances",
                columns: new[] { "StudentId", "LessonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Lessons",
                table: "Marqa.Domain.Entities.Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.HomeTaskFiles",
                table: "Marqa.Domain.Entities.HomeTaskFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.HomeTasks",
                table: "Marqa.Domain.Entities.HomeTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Exams",
                table: "Marqa.Domain.Entities.Exams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.EmployeeRoles",
                table: "Marqa.Domain.Entities.EmployeeRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.CourseWeekdays",
                table: "Marqa.Domain.Entities.CourseWeekdays",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Companys",
                table: "Marqa.Domain.Entities.Companys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Banners",
                table: "Marqa.Domain.Entities.Banners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Marqa.Domain.Entities.Companys_CompanyId",
                table: "Courses",
                column: "CompanyId",
                principalTable: "Marqa.Domain.Entities.Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Marqa.Domain.Entities.Subjects_SubjectId",
                table: "Courses",
                column: "SubjectId",
                principalTable: "Marqa.Domain.Entities.Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Marqa.Domain.Entities.Companys_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Marqa.Domain.Entities.Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Marqa.Domain.Entities.EmployeeRoles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Marqa.Domain.Entities.EmployeeRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.CourseWeekdays_Courses_CourseId",
                table: "Marqa.Domain.Entities.CourseWeekdays",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.EmployeeRoles_Marqa.Domain.Entities.C~",
                table: "Marqa.Domain.Entities.EmployeeRoles",
                column: "CompanyId",
                principalTable: "Marqa.Domain.Entities.Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.Exams_Courses_CourseId",
                table: "Marqa.Domain.Entities.Exams",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.HomeTaskFiles_Marqa.Domain.Entities.H~",
                table: "Marqa.Domain.Entities.HomeTaskFiles",
                column: "HomeTaskId",
                principalTable: "Marqa.Domain.Entities.HomeTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.HomeTasks_Marqa.Domain.Entities.Lesso~",
                table: "Marqa.Domain.Entities.HomeTasks",
                column: "LessonId",
                principalTable: "Marqa.Domain.Entities.Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonAttendances_Marqa.Domain.Entiti~",
                table: "Marqa.Domain.Entities.LessonAttendances",
                column: "LessonId",
                principalTable: "Marqa.Domain.Entities.Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonAttendances_Marqa.Domain.Entit~1",
                table: "Marqa.Domain.Entities.LessonAttendances",
                column: "StudentId",
                principalTable: "Marqa.Domain.Entities.Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonFiles_Marqa.Domain.Entities.Les~",
                table: "Marqa.Domain.Entities.LessonFiles",
                column: "LessonId",
                principalTable: "Marqa.Domain.Entities.Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.Lessons_Courses_CourseId",
                table: "Marqa.Domain.Entities.Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.Lessons_Employees_TeacherId",
                table: "Marqa.Domain.Entities.Lessons",
                column: "TeacherId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonVideos_Marqa.Domain.Entities.Le~",
                table: "Marqa.Domain.Entities.LessonVideos",
                column: "LessonId",
                principalTable: "Marqa.Domain.Entities.Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.OrderItems_Marqa.Domain.Entities.Orde~",
                table: "Marqa.Domain.Entities.OrderItems",
                column: "OrderId",
                principalTable: "Marqa.Domain.Entities.Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.OrderItems_Marqa.Domain.Entities.Prod~",
                table: "Marqa.Domain.Entities.OrderItems",
                column: "ProductId",
                principalTable: "Marqa.Domain.Entities.Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.Orders_Marqa.Domain.Entities.Students~",
                table: "Marqa.Domain.Entities.Orders",
                column: "StudentId",
                principalTable: "Marqa.Domain.Entities.Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.Products_Marqa.Domain.Entities.Compan~",
                table: "Marqa.Domain.Entities.Products",
                column: "CompanyId",
                principalTable: "Marqa.Domain.Entities.Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentCourses_Courses_CourseId",
                table: "Marqa.Domain.Entities.StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentCourses_Marqa.Domain.Entities.~",
                table: "Marqa.Domain.Entities.StudentCourses",
                column: "StudentId",
                principalTable: "Marqa.Domain.Entities.Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentDetails_Marqa.Domain.Entities.~",
                table: "Marqa.Domain.Entities.StudentDetails",
                column: "StudentId",
                principalTable: "Marqa.Domain.Entities.Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentExamResults_Marqa.Domain.Entit~",
                table: "Marqa.Domain.Entities.StudentExamResults",
                column: "ExamId",
                principalTable: "Marqa.Domain.Entities.Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentExamResults_Marqa.Domain.Enti~1",
                table: "Marqa.Domain.Entities.StudentExamResults",
                column: "StudentId",
                principalTable: "Marqa.Domain.Entities.Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_Employees_Te~",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                column: "TeacherId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_Marqa.Domain~",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                column: "StudentHomeTaskId",
                principalTable: "Marqa.Domain.Entities.StudentHomeTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTaskFiles_Marqa.Domain.Ent~",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                column: "StudentHomeTaskId",
                principalTable: "Marqa.Domain.Entities.StudentHomeTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTasks_Marqa.Domain.Entitie~",
                table: "Marqa.Domain.Entities.StudentHomeTasks",
                column: "StudentId",
                principalTable: "Marqa.Domain.Entities.Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentPointHistorys_Marqa.Domain.Ent~",
                table: "Marqa.Domain.Entities.StudentPointHistorys",
                column: "StudentId",
                principalTable: "Marqa.Domain.Entities.Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.Students_Marqa.Domain.Entities.Compan~",
                table: "Marqa.Domain.Entities.Students",
                column: "CompanyId",
                principalTable: "Marqa.Domain.Entities.Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.Subjects_Marqa.Domain.Entities.Compan~",
                table: "Marqa.Domain.Entities.Subjects",
                column: "CompanyId",
                principalTable: "Marqa.Domain.Entities.Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.TeacherSubjects_Employees_TeacherId",
                table: "Marqa.Domain.Entities.TeacherSubjects",
                column: "TeacherId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marqa.Domain.Entities.TeacherSubjects_Marqa.Domain.Entities~",
                table: "Marqa.Domain.Entities.TeacherSubjects",
                column: "SubjectId",
                principalTable: "Marqa.Domain.Entities.Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Marqa.Domain.Entities.Companys_CompanyId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Marqa.Domain.Entities.Subjects_SubjectId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Marqa.Domain.Entities.Companys_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Marqa.Domain.Entities.EmployeeRoles_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.CourseWeekdays_Courses_CourseId",
                table: "Marqa.Domain.Entities.CourseWeekdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.EmployeeRoles_Marqa.Domain.Entities.C~",
                table: "Marqa.Domain.Entities.EmployeeRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.Exams_Courses_CourseId",
                table: "Marqa.Domain.Entities.Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.HomeTaskFiles_Marqa.Domain.Entities.H~",
                table: "Marqa.Domain.Entities.HomeTaskFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.HomeTasks_Marqa.Domain.Entities.Lesso~",
                table: "Marqa.Domain.Entities.HomeTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonAttendances_Marqa.Domain.Entiti~",
                table: "Marqa.Domain.Entities.LessonAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonAttendances_Marqa.Domain.Entit~1",
                table: "Marqa.Domain.Entities.LessonAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonFiles_Marqa.Domain.Entities.Les~",
                table: "Marqa.Domain.Entities.LessonFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.Lessons_Courses_CourseId",
                table: "Marqa.Domain.Entities.Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.Lessons_Employees_TeacherId",
                table: "Marqa.Domain.Entities.Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.LessonVideos_Marqa.Domain.Entities.Le~",
                table: "Marqa.Domain.Entities.LessonVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.OrderItems_Marqa.Domain.Entities.Orde~",
                table: "Marqa.Domain.Entities.OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.OrderItems_Marqa.Domain.Entities.Prod~",
                table: "Marqa.Domain.Entities.OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.Orders_Marqa.Domain.Entities.Students~",
                table: "Marqa.Domain.Entities.Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.Products_Marqa.Domain.Entities.Compan~",
                table: "Marqa.Domain.Entities.Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentCourses_Courses_CourseId",
                table: "Marqa.Domain.Entities.StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentCourses_Marqa.Domain.Entities.~",
                table: "Marqa.Domain.Entities.StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentDetails_Marqa.Domain.Entities.~",
                table: "Marqa.Domain.Entities.StudentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentExamResults_Marqa.Domain.Entit~",
                table: "Marqa.Domain.Entities.StudentExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentExamResults_Marqa.Domain.Enti~1",
                table: "Marqa.Domain.Entities.StudentExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_Employees_Te~",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_Marqa.Domain~",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTaskFiles_Marqa.Domain.Ent~",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentHomeTasks_Marqa.Domain.Entitie~",
                table: "Marqa.Domain.Entities.StudentHomeTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.StudentPointHistorys_Marqa.Domain.Ent~",
                table: "Marqa.Domain.Entities.StudentPointHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.Students_Marqa.Domain.Entities.Compan~",
                table: "Marqa.Domain.Entities.Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.Subjects_Marqa.Domain.Entities.Compan~",
                table: "Marqa.Domain.Entities.Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.TeacherSubjects_Employees_TeacherId",
                table: "Marqa.Domain.Entities.TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Marqa.Domain.Entities.TeacherSubjects_Marqa.Domain.Entities~",
                table: "Marqa.Domain.Entities.TeacherSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.TeacherSubjects",
                table: "Marqa.Domain.Entities.TeacherSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Subjects",
                table: "Marqa.Domain.Entities.Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Students",
                table: "Marqa.Domain.Entities.Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentPointHistorys",
                table: "Marqa.Domain.Entities.StudentPointHistorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentHomeTasks",
                table: "Marqa.Domain.Entities.StudentHomeTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentHomeTaskFiles",
                table: "Marqa.Domain.Entities.StudentHomeTaskFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                table: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentExamResults",
                table: "Marqa.Domain.Entities.StudentExamResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentDetails",
                table: "Marqa.Domain.Entities.StudentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.StudentCourses",
                table: "Marqa.Domain.Entities.StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Settings",
                table: "Marqa.Domain.Entities.Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Products",
                table: "Marqa.Domain.Entities.Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.PointSystemSettings",
                table: "Marqa.Domain.Entities.PointSystemSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.PointSettings",
                table: "Marqa.Domain.Entities.PointSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.OTPs",
                table: "Marqa.Domain.Entities.OTPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Orders",
                table: "Marqa.Domain.Entities.Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.OrderItems",
                table: "Marqa.Domain.Entities.OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.LessonVideos",
                table: "Marqa.Domain.Entities.LessonVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Lessons",
                table: "Marqa.Domain.Entities.Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.LessonFiles",
                table: "Marqa.Domain.Entities.LessonFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.LessonAttendances",
                table: "Marqa.Domain.Entities.LessonAttendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.HomeTasks",
                table: "Marqa.Domain.Entities.HomeTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.HomeTaskFiles",
                table: "Marqa.Domain.Entities.HomeTaskFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Exams",
                table: "Marqa.Domain.Entities.Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.EmployeeRoles",
                table: "Marqa.Domain.Entities.EmployeeRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.CourseWeekdays",
                table: "Marqa.Domain.Entities.CourseWeekdays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Companys",
                table: "Marqa.Domain.Entities.Companys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marqa.Domain.Entities.Banners",
                table: "Marqa.Domain.Entities.Banners");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.TeacherSubjects",
                newName: "TeacherSubject");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.StudentPointHistorys",
                newName: "StudentPointHistory");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.StudentHomeTasks",
                newName: "StudentHomeTask");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.StudentHomeTaskFiles",
                newName: "StudentHomeTaskFile");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.StudentHomeTaskFeedbacks",
                newName: "StudentHomeTaskFeedback");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.StudentExamResults",
                newName: "StudentExamResult");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.StudentDetails",
                newName: "StudentDetail");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.StudentCourses",
                newName: "StudentCourse");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Settings",
                newName: "Setting");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.PointSystemSettings",
                newName: "PointSystemSetting");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.PointSettings",
                newName: "PointSetting");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.OTPs",
                newName: "OTP");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.OrderItems",
                newName: "OrderItem");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.LessonVideos",
                newName: "LessonVideo");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Lessons",
                newName: "Lesson");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.LessonFiles",
                newName: "LessonFile");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.LessonAttendances",
                newName: "LessonAttendance");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.HomeTasks",
                newName: "HomeTask");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.HomeTaskFiles",
                newName: "HomeTaskFile");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Exams",
                newName: "Exam");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.EmployeeRoles",
                newName: "EmployeeRole");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.CourseWeekdays",
                newName: "CourseWeekday");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Companys",
                newName: "Company");

            migrationBuilder.RenameTable(
                name: "Marqa.Domain.Entities.Banners",
                newName: "Banner");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.TeacherSubjects_TeacherId",
                table: "TeacherSubject",
                newName: "IX_TeacherSubject_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Subjects_CompanyId",
                table: "Subject",
                newName: "IX_Subject_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Students_CompanyId",
                table: "Student",
                newName: "IX_Student_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentPointHistorys_StudentId",
                table: "StudentPointHistory",
                newName: "IX_StudentPointHistory_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTasks_StudentId",
                table: "StudentHomeTask",
                newName: "IX_StudentHomeTask_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTaskFiles_StudentHomeTaskId",
                table: "StudentHomeTaskFile",
                newName: "IX_StudentHomeTaskFile_StudentHomeTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_TeacherId",
                table: "StudentHomeTaskFeedback",
                newName: "IX_StudentHomeTaskFeedback_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentHomeTaskFeedbacks_StudentHomeT~",
                table: "StudentHomeTaskFeedback",
                newName: "IX_StudentHomeTaskFeedback_StudentHomeTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentExamResults_StudentId",
                table: "StudentExamResult",
                newName: "IX_StudentExamResult_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentExamResults_ExamId",
                table: "StudentExamResult",
                newName: "IX_StudentExamResult_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentDetails_StudentId",
                table: "StudentDetail",
                newName: "IX_StudentDetail_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.StudentCourses_CourseId",
                table: "StudentCourse",
                newName: "IX_StudentCourse_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Settings_Key",
                table: "Setting",
                newName: "IX_Setting_Key");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Products_CompanyId",
                table: "Product",
                newName: "IX_Product_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Orders_StudentId",
                table: "Order",
                newName: "IX_Order_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.OrderItems_ProductId",
                table: "OrderItem",
                newName: "IX_OrderItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.OrderItems_OrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.LessonVideos_LessonId",
                table: "LessonVideo",
                newName: "IX_LessonVideo_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Lessons_TeacherId",
                table: "Lesson",
                newName: "IX_Lesson_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Lessons_CourseId",
                table: "Lesson",
                newName: "IX_Lesson_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.LessonFiles_LessonId",
                table: "LessonFile",
                newName: "IX_LessonFile_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.LessonAttendances_LessonId",
                table: "LessonAttendance",
                newName: "IX_LessonAttendance_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.HomeTasks_LessonId",
                table: "HomeTask",
                newName: "IX_HomeTask_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.HomeTaskFiles_HomeTaskId",
                table: "HomeTaskFile",
                newName: "IX_HomeTaskFile_HomeTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.Exams_CourseId",
                table: "Exam",
                newName: "IX_Exam_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.EmployeeRoles_Name_CompanyId",
                table: "EmployeeRole",
                newName: "IX_EmployeeRole_Name_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.EmployeeRoles_CompanyId",
                table: "EmployeeRole",
                newName: "IX_EmployeeRole_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Marqa.Domain.Entities.CourseWeekdays_CourseId",
                table: "CourseWeekday",
                newName: "IX_CourseWeekday_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subject",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentAccessId",
                table: "Student",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Student",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Student",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Student",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Student",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StudentPointHistory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "StudentHomeTask",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "StudentHomeTaskFile",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "StudentHomeTaskFile",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotherPhone",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotherLastName",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotherFirstName",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianPhone",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianLastName",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuardianFirstName",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FatherPhone",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FatherLastName",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FatherFirstName",
                table: "StudentDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Setting",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Setting",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Setting",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PointSystemSetting",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QrCode",
                table: "PointSetting",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PointSetting",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PointSetting",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "OTP",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "OTP",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "LessonVideo",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "LessonVideo",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Room",
                table: "Lesson",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lesson",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "LessonFile",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "LessonFile",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "HomeTaskFile",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "HomeTaskFile",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Exam",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmployeeRole",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Company",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Banner",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkUrl",
                table: "Banner",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Banner",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Banner",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubject",
                table: "TeacherSubject",
                columns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPointHistory",
                table: "StudentPointHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentHomeTask",
                table: "StudentHomeTask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentHomeTaskFile",
                table: "StudentHomeTaskFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentHomeTaskFeedback",
                table: "StudentHomeTaskFeedback",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExamResult",
                table: "StudentExamResult",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentDetail",
                table: "StudentDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourse",
                table: "StudentCourse",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Setting",
                table: "Setting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointSystemSetting",
                table: "PointSystemSetting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointSetting",
                table: "PointSetting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OTP",
                table: "OTP",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonVideo",
                table: "LessonVideo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonFile",
                table: "LessonFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonAttendance",
                table: "LessonAttendance",
                columns: new[] { "StudentId", "LessonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeTask",
                table: "HomeTask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeTaskFile",
                table: "HomeTaskFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exam",
                table: "Exam",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRole",
                table: "EmployeeRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseWeekday",
                table: "CourseWeekday",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banner",
                table: "Banner",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Company_CompanyId",
                table: "Courses",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Subject_SubjectId",
                table: "Courses",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseWeekday_Courses_CourseId",
                table: "CourseWeekday",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRole_Company_CompanyId",
                table: "EmployeeRole",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Company_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRole_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "EmployeeRole",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Courses_CourseId",
                table: "Exam",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeTask_Lesson_LessonId",
                table: "HomeTask",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeTaskFile_HomeTask_HomeTaskId",
                table: "HomeTaskFile",
                column: "HomeTaskId",
                principalTable: "HomeTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Courses_CourseId",
                table: "Lesson",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Employees_TeacherId",
                table: "Lesson",
                column: "TeacherId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAttendance_Lesson_LessonId",
                table: "LessonAttendance",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAttendance_Student_StudentId",
                table: "LessonAttendance",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFile_Lesson_LessonId",
                table: "LessonFile",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonVideo_Lesson_LessonId",
                table: "LessonVideo",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Student_StudentId",
                table: "Order",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Company_CompanyId",
                table: "Product",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Company_CompanyId",
                table: "Student",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Courses_CourseId",
                table: "StudentCourse",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Student_StudentId",
                table: "StudentCourse",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDetail_Student_StudentId",
                table: "StudentDetail",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamResult_Exam_ExamId",
                table: "StudentExamResult",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamResult_Student_StudentId",
                table: "StudentExamResult",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHomeTask_Student_StudentId",
                table: "StudentHomeTask",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHomeTaskFeedback_Employees_TeacherId",
                table: "StudentHomeTaskFeedback",
                column: "TeacherId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHomeTaskFeedback_StudentHomeTask_StudentHomeTaskId",
                table: "StudentHomeTaskFeedback",
                column: "StudentHomeTaskId",
                principalTable: "StudentHomeTask",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHomeTaskFile_StudentHomeTask_StudentHomeTaskId",
                table: "StudentHomeTaskFile",
                column: "StudentHomeTaskId",
                principalTable: "StudentHomeTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPointHistory_Student_StudentId",
                table: "StudentPointHistory",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Company_CompanyId",
                table: "Subject",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Employees_TeacherId",
                table: "TeacherSubject",
                column: "TeacherId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Subject_SubjectId",
                table: "TeacherSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
