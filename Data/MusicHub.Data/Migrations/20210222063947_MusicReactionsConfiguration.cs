using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class MusicReactionsConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicReactions_Musics_MusicId",
                table: "MusicReactions");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicReactions_Musics_MusicId",
                table: "MusicReactions",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicReactions_Musics_MusicId",
                table: "MusicReactions");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicReactions_Musics_MusicId",
                table: "MusicReactions",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
