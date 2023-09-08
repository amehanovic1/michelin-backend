using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Michelin.Migrations
{
    public partial class DodavanjedatumautabeluZahtjevZaPomoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<string>(
                name: "korisnikId",
                table: "ZahtjevZaPomoc",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(767)");

            migrationBuilder.AddColumn<DateTime>(
                name: "datum",
                table: "ZahtjevZaPomoc",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZahtjevZaPomoc_Korisnik_korisnikId",
                table: "ZahtjevZaPomoc");

            migrationBuilder.DropColumn(
                name: "datum",
                table: "ZahtjevZaPomoc");

            migrationBuilder.AlterColumn<string>(
                name: "korisnikId",
                table: "ZahtjevZaPomoc",
                type: "varchar(767)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ZahtjevZaPomoc_Korisnik_korisnikId",
                table: "ZahtjevZaPomoc",
                column: "korisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
