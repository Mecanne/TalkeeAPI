using Microsoft.EntityFrameworkCore.Migrations;

namespace TalkeeAPI.Migrations
{
    public partial class added_urlImage_profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "urlImagen",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urlImagen",
                table: "Users");
        }
    }
}
