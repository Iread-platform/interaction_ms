using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace iread_interaction_ms.Migrations
{
    public partial class HighLight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HighLights",
                columns: table => new
                {
                    HighLightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    InteractionId = table.Column<int>(type: "int", nullable: false),
                    FirstWordIndex = table.Column<int>(type: "int", nullable: false),
                    EndWordIndex = table.Column<int>(type: "int", nullable: false),
                    FirstWordTimesTamp = table.Column<string>(type: "text", nullable: false),
                    EndWordTimesTamp = table.Column<string>(type: "text", nullable: false),
                    FirstWord = table.Column<string>(type: "text", nullable: false),
                    EndWord = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighLights", x => x.HighLightId);
                    table.ForeignKey(
                        name: "FK_HighLights_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "InteractionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HighLights_InteractionId",
                table: "HighLights",
                column: "InteractionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighLights");
        }
    }
}
