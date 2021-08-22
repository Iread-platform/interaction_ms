using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_interaction_ms.Migrations
{
    public partial class adddoubleindexdrawing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Max_X",
                table: "Drawings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Max_Y",
                table: "Drawings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Min_X",
                table: "Drawings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Min_Y",
                table: "Drawings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Max_X",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "Max_Y",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "Min_X",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "Min_Y",
                table: "Drawings");
        }
    }
}
