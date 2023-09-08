using Microsoft.EntityFrameworkCore.Migrations;

namespace Michelin.Migrations
{
    public partial class Dodanatabelasnajboljimreceptima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NajboljiRecepti",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    receptid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NajboljiRecepti", x => x.id);
                    table.ForeignKey(
                        name: "FK_NajboljiRecepti_Recept_receptid",
                        column: x => x.receptid,
                        principalTable: "Recept",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NajboljiRecepti_receptid",
                table: "NajboljiRecepti",
                column: "receptid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NajboljiRecepti");
        }
    }
}
