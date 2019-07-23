using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class updateRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rating",
                maxLength: 150,
                nullable: true);
        }
    }
}
