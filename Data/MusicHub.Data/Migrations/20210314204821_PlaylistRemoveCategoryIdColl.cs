using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class PlaylistRemoveCategoryIdColl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Categories_CategoryId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_CategoryId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Playlists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Playlists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_CategoryId",
                table: "Playlists",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Categories_CategoryId",
                table: "Playlists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
