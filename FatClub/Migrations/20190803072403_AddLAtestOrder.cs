using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class AddLAtestOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LatestOrder",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestOrder",
                table: "AspNetUsers");
        }
    }
}
