using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class smallchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_CartItems_CartItemID",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_CartItemID",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CartItemID",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ShoppingCarts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserName",
                table: "ShoppingCarts",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserName",
                table: "ShoppingCarts",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserName",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserName",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ShoppingCarts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartItemID",
                table: "Food",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_CartItemID",
                table: "Food",
                column: "CartItemID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers",
                column: "ShoppingCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers",
                column: "ShoppingCartID",
                principalTable: "ShoppingCarts",
                principalColumn: "ShoppingCartID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_CartItems_CartItemID",
                table: "Food",
                column: "CartItemID",
                principalTable: "CartItems",
                principalColumn: "CartItemID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
