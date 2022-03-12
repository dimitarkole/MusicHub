using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class renameCollAtSongTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
