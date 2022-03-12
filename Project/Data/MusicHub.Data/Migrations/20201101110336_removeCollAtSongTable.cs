using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class removeCollAtSongTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "AudioFilePath",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFilePath",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioFilePath",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ImageFilePath",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mp3Extension",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueSongFilesName",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
