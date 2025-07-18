using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zeynerp.Infrastructure.Data.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CariTanimlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KisaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EPosta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VergiDairesi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VergiNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaturaAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariTanimlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CariTurTanimlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CariTurTanimAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariTurTanimlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StokGrupTanimlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StokTanimAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokGrupTanimlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CariYetkiliTanimlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdiSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EPosta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CariTanimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariYetkiliTanimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CariYetkiliTanimlar_CariTanimlar_CariTanimId",
                        column: x => x.CariTanimId,
                        principalTable: "CariTanimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeslimatAdresTanimlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CariTanimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeslimatAdresTanimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeslimatAdresTanimlar_CariTanimlar_CariTanimId",
                        column: x => x.CariTanimId,
                        principalTable: "CariTanimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CariTanimCariTurTanim",
                columns: table => new
                {
                    CariTanimlarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CariTurTanimlarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariTanimCariTurTanim", x => new { x.CariTanimlarId, x.CariTurTanimlarId });
                    table.ForeignKey(
                        name: "FK_CariTanimCariTurTanim_CariTanimlar_CariTanimlarId",
                        column: x => x.CariTanimlarId,
                        principalTable: "CariTanimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CariTanimCariTurTanim_CariTurTanimlar_CariTurTanimlarId",
                        column: x => x.CariTurTanimlarId,
                        principalTable: "CariTurTanimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StokGrupTanimlar",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Durum", "Sira", "StokTanimAdi", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("96c0f08c-b553-4333-9e74-5c38ab87bade"), null, new DateTime(2025, 7, 18, 14, 56, 49, 916, DateTimeKind.Local).AddTicks(1647), 1, 1, "Yok", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CariTanimCariTurTanim_CariTurTanimlarId",
                table: "CariTanimCariTurTanim",
                column: "CariTurTanimlarId");

            migrationBuilder.CreateIndex(
                name: "IX_CariYetkiliTanimlar_CariTanimId",
                table: "CariYetkiliTanimlar",
                column: "CariTanimId");

            migrationBuilder.CreateIndex(
                name: "IX_TeslimatAdresTanimlar_CariTanimId",
                table: "TeslimatAdresTanimlar",
                column: "CariTanimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CariTanimCariTurTanim");

            migrationBuilder.DropTable(
                name: "CariYetkiliTanimlar");

            migrationBuilder.DropTable(
                name: "StokGrupTanimlar");

            migrationBuilder.DropTable(
                name: "TeslimatAdresTanimlar");

            migrationBuilder.DropTable(
                name: "CariTurTanimlar");

            migrationBuilder.DropTable(
                name: "CariTanimlar");
        }
    }
}
