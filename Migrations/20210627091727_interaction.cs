using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_interaction_ms.Migrations
{
    public partial class interaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Interactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Interactions");
        }
    }
}
