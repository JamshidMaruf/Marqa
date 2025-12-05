using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NextMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPointHistories_Students_StudentId",
                table: "StudentPointHistories");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentFrozens_EnrollmentId",
                table: "EnrollmentFrozens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPointHistories",
                table: "StudentPointHistories");

            migrationBuilder.RenameTable(
                name: "StudentPointHistories",
                newName: "PointHistories");

            migrationBuilder.RenameIndex(
                name: "IX_StudentPointHistories_StudentId",
                table: "PointHistories",
                newName: "IX_PointHistories_StudentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "GivenDateTime",
                table: "PointHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointHistories",
                table: "PointHistories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EnrollmentTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromEnrollmentId = table.Column<int>(type: "integer", nullable: false),
                    ToEnrollmentId = table.Column<int>(type: "integer", nullable: false),
                    TransferTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentTransfers_Enrollments_FromEnrollmentId",
                        column: x => x.FromEnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnrollmentTransfers_Enrollments_ToEnrollmentId",
                        column: x => x.ToEnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentFrozens_EnrollmentId",
                table: "EnrollmentFrozens",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentTransfers_FromEnrollmentId",
                table: "EnrollmentTransfers",
                column: "FromEnrollmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentTransfers_ToEnrollmentId",
                table: "EnrollmentTransfers",
                column: "ToEnrollmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PointHistories_Students_StudentId",
                table: "PointHistories",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointHistories_Students_StudentId",
                table: "PointHistories");

            migrationBuilder.DropTable(
                name: "EnrollmentTransfers");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentFrozens_EnrollmentId",
                table: "EnrollmentFrozens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointHistories",
                table: "PointHistories");

            migrationBuilder.DropColumn(
                name: "GivenDateTime",
                table: "PointHistories");

            migrationBuilder.RenameTable(
                name: "PointHistories",
                newName: "StudentPointHistories");

            migrationBuilder.RenameIndex(
                name: "IX_PointHistories_StudentId",
                table: "StudentPointHistories",
                newName: "IX_StudentPointHistories_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPointHistories",
                table: "StudentPointHistories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentFrozens_EnrollmentId",
                table: "EnrollmentFrozens",
                column: "EnrollmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPointHistories_Students_StudentId",
                table: "StudentPointHistories",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
