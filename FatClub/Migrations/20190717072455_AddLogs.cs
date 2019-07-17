using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class AddLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "foodIDField",
                table: "AuditLogs",
                newName: "FoodIDField");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AuditLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "FoodIDField",
                table: "AuditLogs",
                newName: "foodIDField");
        }
    }
}
