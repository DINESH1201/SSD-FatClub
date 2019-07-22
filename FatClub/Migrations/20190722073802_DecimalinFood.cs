using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class DecimalinFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Food",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Food",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
