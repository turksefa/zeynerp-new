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
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
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
                name: "CariTurTanimlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CariTurTanimAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    CariTanimId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariTurTanimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CariTurTanimlar_CariTanimlar_CariTanimId",
                        column: x => x.CariTanimId,
                        principalTable: "CariTanimlar",
                        principalColumn: "Id");
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

            migrationBuilder.InsertData(
                table: "StokGrupTanimlar",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Durum", "Sira", "StokTanimAdi", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("34622805-4546-42ac-a940-3352e929a7b8"), null, new DateTime(2025, 7, 26, 13, 3, 58, 454, DateTimeKind.Local).AddTicks(4129), 1, 1, "Yok", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CariTurTanimlar_CariTanimId",
                table: "CariTurTanimlar",
                column: "CariTanimId");

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
                name: "CariTurTanimlar");

            migrationBuilder.DropTable(
                name: "CariYetkiliTanimlar");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "StokGrupTanimlar");

            migrationBuilder.DropTable(
                name: "TeslimatAdresTanimlar");

            migrationBuilder.DropTable(
                name: "CariTanimlar");
        }
    }
}
