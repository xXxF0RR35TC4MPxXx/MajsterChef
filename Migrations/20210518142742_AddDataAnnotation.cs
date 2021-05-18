using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MajsterChef.Migrations
{
    public partial class AddDataAnnotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Przepis",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(maxLength: 100, nullable: false),
                    Opis_wykonania = table.Column<string>(maxLength: 2000, nullable: false),
                    Data_publikacji = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przepis", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Skladnik",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true),
                    Ilosc = table.Column<string>(nullable: true),
                    PrzepisID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skladnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Skladnik_Przepis_PrzepisID",
                        column: x => x.PrzepisID,
                        principalTable: "Przepis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skladnik_PrzepisID",
                table: "Skladnik",
                column: "PrzepisID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skladnik");

            migrationBuilder.DropTable(
                name: "Przepis");
        }
    }
}
