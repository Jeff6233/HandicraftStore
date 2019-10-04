using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicraft.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ShoppingCar",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "ShoppingCar",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
