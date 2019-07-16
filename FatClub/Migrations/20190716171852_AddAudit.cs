using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FatClub.Migrations
{
    public partial class AddAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditRecords");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Audit_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditActionType = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    DateTimeStamp = table.Column<DateTime>(nullable: false),
                    foodIDField = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Audit_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.CreateTable(
                name: "AuditRecords",
                columns: table => new
                {
                    Audit_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditActionType = table.Column<string>(nullable: true),
                    DateTimeStamp = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    foodIDField = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRecords", x => x.Audit_ID);
                });
        }
    }
}
