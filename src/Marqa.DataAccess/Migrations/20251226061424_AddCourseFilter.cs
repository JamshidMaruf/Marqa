using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "student_count",
                table: "courses",
                newName: "current_student_count");

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 1,
                column: "current_student_count",
                value: 3);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 2,
                column: "current_student_count",
                value: 2);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 3,
                column: "current_student_count",
                value: 2);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 4,
                column: "current_student_count",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "current_student_count",
                table: "courses",
                newName: "student_count");

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 1,
                column: "student_count",
                value: 0);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 2,
                column: "student_count",
                value: 0);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 3,
                column: "student_count",
                value: 0);

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 4,
                column: "student_count",
                value: 0);
        }
    }
}
