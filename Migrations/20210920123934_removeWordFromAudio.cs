using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_interaction_ms.Migrations
{
    public partial class removeWordFromAudio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndWord",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "EndWordIndex",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "FirstWord",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "FirstWordIndex",
                table: "Audios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndWord",
                table: "Audios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndWordIndex",
                table: "Audios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstWord",
                table: "Audios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstWordIndex",
                table: "Audios",
                type: "int",
                nullable: true);
        }
    }
}
