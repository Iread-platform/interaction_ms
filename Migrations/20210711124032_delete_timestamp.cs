using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_interaction_ms.Migrations
{
    public partial class delete_timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndWordTimesTamp",
                table: "HighLights");

            migrationBuilder.DropColumn(
                name: "FirstWordTimesTamp",
                table: "HighLights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndWordTimesTamp",
                table: "HighLights",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstWordTimesTamp",
                table: "HighLights",
                type: "text",
                nullable: false);
        }
    }
}
