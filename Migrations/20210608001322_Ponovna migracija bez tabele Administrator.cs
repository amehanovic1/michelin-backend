
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Michelin.Migrations
{
    public partial class PonovnamigracijabeztabeleAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    korisnickoIme = table.Column<string>(maxLength: 30, nullable: false),
                    ime = table.Column<string>(nullable: false),
                    prezime = table.Column<string>(nullable: false),
                    brojMobitela = table.Column<string>(nullable: true),
                    kratkaBiografija = table.Column<string>(maxLength: 400, nullable: true),
                    datumAktivacije = table.Column<DateTime>(nullable: false),
                    datumDeaktivacije = table.Column<DateTime>(nullable: false),
                    aktivan = table.Column<bool>(nullable: false),
                    omiljeniRecepti = table.Column<string>(nullable: true),
                    profilnaSlika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nacin Pripreme",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    listaSastojaka = table.Column<string>(nullable: false),
                    opisPripreme = table.Column<string>(maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nacin Pripreme", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sastojak",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    naziv = table.Column<string>(nullable: false),
                    kolicina = table.Column<double>(nullable: false),
                    mjernaJedinica = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sastojak", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Korisnik_UserId",
                        column: x => x.UserId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Korisnik_UserId",
                        column: x => x.UserId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Korisnik_UserId",
                        column: x => x.UserId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Korisnik_UserId",
                        column: x => x.UserId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZahtjevZaPomoc",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    kategorija = table.Column<int>(nullable: false),
                    sadrzaj = table.Column<string>(maxLength: 1000, nullable: false),
                    korisnikId = table.Column<string>(nullable: false),
                    obradjeno = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjevZaPomoc", x => x.id);
                    table.ForeignKey(
                        name: "FK_ZahtjevZaPomoc_Korisnik_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recept",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    naziv = table.Column<string>(maxLength: 30, nullable: false),
                    vrijemePripreme = table.Column<int>(nullable: false),
                    nacionalnoJelo = table.Column<int>(nullable: false),
                    vrstaJela = table.Column<int>(nullable: false),
                    vegansko = table.Column<bool>(nullable: false),
                    autorId = table.Column<string>(nullable: false),
                    nacinPripremeid = table.Column<string>(nullable: false),
                    datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recept", x => x.id);
                    table.ForeignKey(
                        name: "FK_Recept_Korisnik_autorId",
                        column: x => x.autorId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recept_Nacin Pripreme_nacinPripremeid",
                        column: x => x.nacinPripremeid,
                        principalTable: "Nacin Pripreme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentar",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    sadrzaj = table.Column<string>(maxLength: 300, nullable: false),
                    autorId = table.Column<string>(nullable: false),
                    receptid = table.Column<string>(nullable: false),
                    datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentar", x => x.id);
                    table.ForeignKey(
                        name: "FK_Komentar_Korisnik_autorId",
                        column: x => x.autorId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentar_Recept_receptid",
                        column: x => x.receptid,
                        principalTable: "Recept",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocjena",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    korisnikId = table.Column<string>(nullable: false),
                    receptid = table.Column<string>(nullable: false),
                    vrijednost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjena", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ocjena_Korisnik_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocjena_Recept_receptid",
                        column: x => x.receptid,
                        principalTable: "Recept",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_autorId",
                table: "Komentar",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_receptid",
                table: "Komentar",
                column: "receptid");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Korisnik",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Korisnik",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_korisnikId",
                table: "Ocjena",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_receptid",
                table: "Ocjena",
                column: "receptid");

            migrationBuilder.CreateIndex(
                name: "IX_Recept_autorId",
                table: "Recept",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recept_nacinPripremeid",
                table: "Recept",
                column: "nacinPripremeid");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevZaPomoc_korisnikId",
                table: "ZahtjevZaPomoc",
                column: "korisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Komentar");

            migrationBuilder.DropTable(
                name: "Ocjena");

            migrationBuilder.DropTable(
                name: "Sastojak");

            migrationBuilder.DropTable(
                name: "ZahtjevZaPomoc");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Recept");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Nacin Pripreme");
        }
    }
}
