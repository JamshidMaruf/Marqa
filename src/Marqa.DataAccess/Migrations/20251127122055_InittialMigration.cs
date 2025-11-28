using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InittialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Assets_AssetId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Students",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Category", "CreatedAt", "DeletedAt", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 16, "RefreshToken", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RefreshToken.Expires.RememberMe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "30" },
                    { 17, "RefreshToken", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RefreshToken.Expires.Standard", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Assets_AssetId",
                table: "Students",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Assets_AssetId",
                table: "Students");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Assets_AssetId",
                table: "Students",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
