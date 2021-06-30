using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_interaction_ms.Migrations
{
    public partial class timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Interactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "WordTimesTamp",
                table: "Comments",
                type: "text",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WordTimesTamp",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Interactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
