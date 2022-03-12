using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class DeleteConfigPlaylist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlaylistId",
                table: "PlaylistSongs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaylistId1",
                table: "PlaylistSongs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongs_PlaylistId1",
                table: "PlaylistSongs",
                column: "PlaylistId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSongs_Playlists_PlaylistId1",
                table: "PlaylistSongs",
                column: "PlaylistId1",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSongs_Playlists_PlaylistId1",
                table: "PlaylistSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Categories_Id",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistSongs_PlaylistId1",
                table: "PlaylistSongs");

            migrationBuilder.DropColumn(
                name: "PlaylistId1",
                table: "PlaylistSongs");

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistId",
                table: "PlaylistSongs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
