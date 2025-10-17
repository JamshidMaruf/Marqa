using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOTPEnityt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeTaskFile_HomeTasks_HomeTaskId",
                table: "HomeTaskFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeTaskFile",
                table: "HomeTaskFile");

            migrationBuilder.RenameTable(
                name: "HomeTaskFile",
                newName: "HomeTaskFiles");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Students",
                newName: "StudentAccessId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeTaskFile_HomeTaskId",
                table: "HomeTaskFiles",
                newName: "IX_HomeTaskFiles_HomeTaskId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lessons",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeTaskFiles",
                table: "HomeTaskFiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HomeTaskFiles_HomeTasks_HomeTaskId",
                table: "HomeTaskFiles",
                column: "HomeTaskId",
                principalTable: "HomeTasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeTaskFiles_HomeTasks_HomeTaskId",
                table: "HomeTaskFiles");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeTaskFiles",
                table: "HomeTaskFiles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "HomeTaskFiles",
                newName: "HomeTaskFile");

            migrationBuilder.RenameColumn(
                name: "StudentAccessId",
                table: "Students",
                newName: "StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_HomeTaskFiles_HomeTaskId",
                table: "HomeTaskFile",
                newName: "IX_HomeTaskFile_HomeTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeTaskFile",
                table: "HomeTaskFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeTaskFile_HomeTasks_HomeTaskId",
                table: "HomeTaskFile",
                column: "HomeTaskId",
                principalTable: "HomeTasks",
                principalColumn: "Id");
        }
    }
}
