using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewColumnFor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rating",
                table: "teacher_assessments");

            migrationBuilder.RenameColumn(
                name: "payment_type",
                table: "teacher_salarys",
                newName: "salary_type");

            migrationBuilder.AddColumn<bool>(
                name: "is_un_frozen",
                table: "enrollment_frozens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_un_frozen",
                table: "enrollment_frozens");

            migrationBuilder.RenameColumn(
                name: "salary_type",
                table: "teacher_salarys",
                newName: "payment_type");

            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "teacher_assessments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
