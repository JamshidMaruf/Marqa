using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Marqa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_assets_asset_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_products_asset_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "asset_id",
                table: "products");

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    image_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_images_assets_image_id",
                        column: x => x.image_id,
                        principalTable: "assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_images_image_id",
                table: "product_images",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_images_product_id",
                table: "product_images",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.AddColumn<int>(
                name: "asset_id",
                table: "products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_asset_id",
                table: "products",
                column: "asset_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_assets_asset_id",
                table: "products",
                column: "asset_id",
                principalTable: "assets",
                principalColumn: "id");
        }
    }
}
