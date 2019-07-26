using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class smoollchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartID1",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ShoppingCartID1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ShoppingCartID1",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartID",
                table: "CartItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FoodID",
                table: "CartItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartID",
                table: "CartItems",
                column: "ShoppingCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartID",
                table: "CartItems",
                column: "ShoppingCartID",
                principalTable: "ShoppingCarts",
                principalColumn: "ShoppingCartID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartID",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ShoppingCartID",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartID",
                table: "CartItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "FoodID",
                table: "CartItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID1",
                table: "CartItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartID1",
                table: "CartItems",
                column: "ShoppingCartID1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartID1",
                table: "CartItems",
                column: "ShoppingCartID1",
                principalTable: "ShoppingCarts",
                principalColumn: "ShoppingCartID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
