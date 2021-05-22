using Microsoft.EntityFrameworkCore.Migrations;

namespace MajsterChef.Migrations
{
    public partial class KolejnyEpizodWalkiZOcenami : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oceny",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_usera = table.Column<string>(nullable: true),
                    Id_wpisu = table.Column<int>(nullable: false),
                    Czy_ocenil = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oceny", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oceny");
        }
    }
}
