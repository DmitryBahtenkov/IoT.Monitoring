using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iot.Main.Infrastructure.Migrations
{
    public partial class IsArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "Devices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "Constraints",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "Alerts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Alerts");
        }
    }
}
