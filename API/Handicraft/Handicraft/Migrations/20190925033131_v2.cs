using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicraft.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImage",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "DetailImage",
                table: "ProductImage");

            migrationBuilder.CreateTable(
                name: "BannerImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ProductImageId = table.Column<Guid>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerImage_ProductImage_ProductImageId",
                        column: x => x.ProductImageId,
                        principalTable: "ProductImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ProductImageId = table.Column<Guid>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailImage_ProductImage_ProductImageId",
                        column: x => x.ProductImageId,
                        principalTable: "ProductImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannerImage_ProductImageId",
                table: "BannerImage",
                column: "ProductImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailImage_ProductImageId",
                table: "DetailImage",
                column: "ProductImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerImage");

            migrationBuilder.DropTable(
                name: "DetailImage");

            migrationBuilder.AddColumn<string>(
                name: "BannerImage",
                table: "ProductImage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailImage",
                table: "ProductImage",
                nullable: true);
        }
    }
}
