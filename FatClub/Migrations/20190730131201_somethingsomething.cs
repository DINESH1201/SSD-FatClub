using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class somethingsomething : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "AuditLogs",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AuditLogs",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeStamp",
                table: "AuditLogs",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "AuditActionType",
                table: "AuditLogs",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartID",
                table: "AspNetUsers",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingCartID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "AuditLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AuditLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeStamp",
                table: "AuditLogs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<string>(
                name: "AuditActionType",
                table: "AuditLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);
        }
    }
}
