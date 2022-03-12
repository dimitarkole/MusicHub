using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class RenameColl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Categories_MusicTypeId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_MusicTypeId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "MusicTypeId",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "MusicCategoryId",
                table: "Songs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_MusicCategoryId",
                table: "Songs",
                column: "MusicCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Categories_MusicCategoryId",
                table: "Songs",
                column: "MusicCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Categories_MusicCategoryId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_MusicCategoryId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "MusicCategoryId",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "MusicTypeId",
                table: "Songs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_MusicTypeId",
                table: "Songs",
                column: "MusicTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Categories_MusicTypeId",
                table: "Songs",
                column: "MusicTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
