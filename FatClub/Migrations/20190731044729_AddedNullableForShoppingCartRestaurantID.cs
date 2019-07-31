using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class AddedNullableForShoppingCartRestaurantID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RestaurantID",
                table: "ShoppingCarts",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RestaurantID",
                table: "ShoppingCarts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
