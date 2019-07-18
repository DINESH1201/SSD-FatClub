using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class AddLogsv12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodIDField",
                table: "AuditLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodIDField",
                table: "AuditLogs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
