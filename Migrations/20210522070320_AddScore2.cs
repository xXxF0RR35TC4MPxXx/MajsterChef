using Microsoft.EntityFrameworkCore.Migrations;

namespace MajsterChef.Migrations
{
    public partial class AddScore2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Przepis",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Przepis");
        }
    }
}
