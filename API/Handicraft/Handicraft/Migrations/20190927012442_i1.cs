using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicraft.Migrations
{
    public partial class i1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCar_UserId",
                table: "ShoppingCar");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCar_UserId",
                table: "ShoppingCar",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCar_UserId",
                table: "ShoppingCar");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCar_UserId",
                table: "ShoppingCar",
                column: "UserId",
                unique: true);
        }
    }
}
