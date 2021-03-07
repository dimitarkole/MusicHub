using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class MusicEditCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAd",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MusicLicenseType",
                table: "Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusicLicenseType",
                table: "Musics");

            migrationBuilder.AddColumn<bool>(
                name: "IsAd",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
