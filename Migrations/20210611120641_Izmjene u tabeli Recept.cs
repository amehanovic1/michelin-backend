using Microsoft.EntityFrameworkCore.Migrations;

namespace Michelin.Migrations
{
    public partial class IzmjeneutabeliRecept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slika",
                table: "Recept",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "video",
                table: "Recept",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slika",
                table: "Recept");

            migrationBuilder.DropColumn(
                name: "video",
                table: "Recept");
        }
    }
}
