using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class full : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teacher_assessments_courses_course_id",
                table: "teacher_assessments");

            migrationBuilder.DropForeignKey(
                name: "fk_teacher_assessments_students_student_id",
                table: "teacher_assessments");

            migrationBuilder.DropForeignKey(
                name: "fk_teacher_assessments_teachers_teacher_id",
                table: "teacher_assessments");

            migrationBuilder.AddColumn<int>(
                name: "rate",
                table: "teacher_assessments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "submitted_date_time",
                table: "teacher_assessments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "ix_teacher_assessments_submitted_date_time",
                table: "teacher_assessments",
                column: "submitted_date_time");

            migrationBuilder.AddForeignKey(
                name: "fk_teacher_assessments_courses_course_id",
                table: "teacher_assessments",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_teacher_assessments_students_student_id",
                table: "teacher_assessments",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_teacher_assessments_teachers_teacher_id",
                table: "teacher_assessments",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teacher_assessments_courses_course_id",
                table: "teacher_assessments");

            migrationBuilder.DropForeignKey(
                name: "fk_teacher_assessments_students_student_id",
                table: "teacher_assessments");

            migrationBuilder.DropForeignKey(
                name: "fk_teacher_assessments_teachers_teacher_id",
                table: "teacher_assessments");

            migrationBuilder.DropIndex(
                name: "ix_teacher_assessments_submitted_date_time",
                table: "teacher_assessments");

            migrationBuilder.DropColumn(
                name: "rate",
                table: "teacher_assessments");

            migrationBuilder.DropColumn(
                name: "submitted_date_time",
                table: "teacher_assessments");

            migrationBuilder.AddForeignKey(
                name: "fk_teacher_assessments_courses_course_id",
                table: "teacher_assessments",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_teacher_assessments_students_student_id",
                table: "teacher_assessments",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_teacher_assessments_teachers_teacher_id",
                table: "teacher_assessments",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
