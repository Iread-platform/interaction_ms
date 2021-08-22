using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_interaction_ms.Migrations
{
    public partial class deleteintindexdrawing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxX",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "MaxY",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "MinX",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "MinY",
                table: "Drawings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxX",
                table: "Drawings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxY",
                table: "Drawings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinX",
                table: "Drawings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinY",
                table: "Drawings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
