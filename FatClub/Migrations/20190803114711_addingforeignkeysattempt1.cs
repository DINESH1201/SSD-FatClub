using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class addingforeignkeysattempt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_FoodID",
                table: "OrderItems",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_FoodID",
                table: "CartItems",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrderID",
                table: "AspNetUsers",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers",
                column: "ShoppingCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Order_OrderID",
                table: "AspNetUsers",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers",
                column: "ShoppingCartID",
                principalTable: "ShoppingCarts",
                principalColumn: "ShoppingCartID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Food_FoodID",
                table: "CartItems",
                column: "FoodID",
                principalTable: "Food",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Food_FoodID",
                table: "OrderItems",
                column: "FoodID",
                principalTable: "Food",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Order_OrderID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Food_FoodID",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Food_FoodID",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_FoodID",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_FoodID",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OrderID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingCartID",
                table: "AspNetUsers");
        }
    }
}
