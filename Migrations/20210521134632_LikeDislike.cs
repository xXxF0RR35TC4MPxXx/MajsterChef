using Microsoft.EntityFrameworkCore.Migrations;

namespace MajsterChef.Migrations
{
    public partial class LikeDislike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Przepis");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Przepis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Przepis",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Przepis");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Przepis");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Przepis",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
