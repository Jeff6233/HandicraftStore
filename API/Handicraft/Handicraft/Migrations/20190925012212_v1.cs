using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicraft.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Create",
                table: "UserInfo",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "Create",
                table: "UserCollect",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "Create",
                table: "ShoppingCar",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "Create",
                table: "ProductImage",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "Create",
                table: "Product",
                newName: "CreateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "UserInfo",
                newName: "Create");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "UserCollect",
                newName: "Create");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "ShoppingCar",
                newName: "Create");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "ProductImage",
                newName: "Create");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Product",
                newName: "Create");
        }
    }
}
