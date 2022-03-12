using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class addFileCollInSongTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mp3Extension",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueSongFilesName",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Mp3Extension",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "UniqueSongFilesName",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
