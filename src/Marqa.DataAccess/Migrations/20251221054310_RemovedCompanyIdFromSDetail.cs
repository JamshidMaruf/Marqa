using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCompanyIdFromSDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_details_companys_company_id",
                table: "student_details");

            migrationBuilder.DropIndex(
                name: "ix_student_details_company_id",
                table: "student_details");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "student_details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "company_id",
                table: "student_details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "student_details",
                keyColumn: "id",
                keyValue: 1,
                column: "company_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "student_details",
                keyColumn: "id",
                keyValue: 2,
                column: "company_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "student_details",
                keyColumn: "id",
                keyValue: 3,
                column: "company_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "student_details",
                keyColumn: "id",
                keyValue: 4,
                column: "company_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "student_details",
                keyColumn: "id",
                keyValue: 5,
                column: "company_id",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "ix_student_details_company_id",
                table: "student_details",
                column: "company_id");

            migrationBuilder.AddForeignKey(
                name: "fk_student_details_companys_company_id",
                table: "student_details",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
