using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AdjustStudentDetailConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_student_details_father_phone_company_id",
                table: "student_details");

            migrationBuilder.DropIndex(
                name: "ix_student_details_guardian_phone_company_id",
                table: "student_details");

            migrationBuilder.DropIndex(
                name: "ix_student_details_mother_phone_company_id",
                table: "student_details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_student_details_father_phone_company_id",
                table: "student_details",
                columns: new[] { "father_phone", "company_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_student_details_guardian_phone_company_id",
                table: "student_details",
                columns: new[] { "guardian_phone", "company_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_student_details_mother_phone_company_id",
                table: "student_details",
                columns: new[] { "mother_phone", "company_id" },
                unique: true);
        }
    }
}
