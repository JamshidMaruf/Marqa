using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migraion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Student",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Student");
        }
    }
}
