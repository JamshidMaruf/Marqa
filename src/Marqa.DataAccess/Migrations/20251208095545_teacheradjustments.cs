using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Teacheradjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ParentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ParentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "EmployeeSalarys",
                newName: "Amount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "EnrollmentFrozens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EnrollmentFrozens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "EmployeeSalarys",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "EmployeeSalarys");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "EmployeeSalarys",
                newName: "Salary");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "EnrollmentFrozens",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "EnrollmentFrozens",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ParentId",
                table: "Employees",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ParentId",
                table: "Employees",
                column: "ParentId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
