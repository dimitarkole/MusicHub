using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class SomeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongDislike_Songs_SongId",
                table: "SongDislike");

            migrationBuilder.DropForeignKey(
                name: "FK_SongDislike_AspNetUsers_UserId",
                table: "SongDislike");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLike_Songs_SongId",
                table: "SongLike");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLike_AspNetUsers_UserId",
                table: "SongLike");

            migrationBuilder.DropForeignKey(
                name: "FK_SongViewHistory_Songs_SongId",
                table: "SongViewHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_SongViewHistory_AspNetUsers_UserId",
                table: "SongViewHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongViewHistory",
                table: "SongViewHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongLike",
                table: "SongLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongDislike",
                table: "SongDislike");

            migrationBuilder.RenameTable(
                name: "SongViewHistory",
                newName: "SongViewHistories");

            migrationBuilder.RenameTable(
                name: "SongLike",
                newName: "SongLikes");

            migrationBuilder.RenameTable(
                name: "SongDislike",
                newName: "SongDislikes");

            migrationBuilder.RenameIndex(
                name: "IX_SongViewHistory_UserId",
                table: "SongViewHistories",
                newName: "IX_SongViewHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongViewHistory_SongId",
                table: "SongViewHistories",
                newName: "IX_SongViewHistories_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_SongViewHistory_IsDeleted",
                table: "SongViewHistories",
                newName: "IX_SongViewHistories_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_SongLike_UserId",
                table: "SongLikes",
                newName: "IX_SongLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongLike_SongId",
                table: "SongLikes",
                newName: "IX_SongLikes_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_SongLike_IsDeleted",
                table: "SongLikes",
                newName: "IX_SongLikes_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_SongDislike_UserId",
                table: "SongDislikes",
                newName: "IX_SongDislikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongDislike_SongId",
                table: "SongDislikes",
                newName: "IX_SongDislikes_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_SongDislike_IsDeleted",
                table: "SongDislikes",
                newName: "IX_SongDislikes_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongViewHistories",
                table: "SongViewHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongLikes",
                table: "SongLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongDislikes",
                table: "SongDislikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SongDislikes_Songs_SongId",
                table: "SongDislikes",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongDislikes_AspNetUsers_UserId",
                table: "SongDislikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongLikes_AspNetUsers_UserId",
                table: "SongLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongViewHistories_Songs_SongId",
                table: "SongViewHistories",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongViewHistories_AspNetUsers_UserId",
                table: "SongViewHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongDislikes_Songs_SongId",
                table: "SongDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_SongDislikes_AspNetUsers_UserId",
                table: "SongDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLikes_AspNetUsers_UserId",
                table: "SongLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_SongViewHistories_Songs_SongId",
                table: "SongViewHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_SongViewHistories_AspNetUsers_UserId",
                table: "SongViewHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongViewHistories",
                table: "SongViewHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongLikes",
                table: "SongLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongDislikes",
                table: "SongDislikes");

            migrationBuilder.RenameTable(
                name: "SongViewHistories",
                newName: "SongViewHistory");

            migrationBuilder.RenameTable(
                name: "SongLikes",
                newName: "SongLike");

            migrationBuilder.RenameTable(
                name: "SongDislikes",
                newName: "SongDislike");

            migrationBuilder.RenameIndex(
                name: "IX_SongViewHistories_UserId",
                table: "SongViewHistory",
                newName: "IX_SongViewHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongViewHistories_SongId",
                table: "SongViewHistory",
                newName: "IX_SongViewHistory_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_SongViewHistories_IsDeleted",
                table: "SongViewHistory",
                newName: "IX_SongViewHistory_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_SongLikes_UserId",
                table: "SongLike",
                newName: "IX_SongLike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongLikes_SongId",
                table: "SongLike",
                newName: "IX_SongLike_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_SongLikes_IsDeleted",
                table: "SongLike",
                newName: "IX_SongLike_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_SongDislikes_UserId",
                table: "SongDislike",
                newName: "IX_SongDislike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongDislikes_SongId",
                table: "SongDislike",
                newName: "IX_SongDislike_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_SongDislikes_IsDeleted",
                table: "SongDislike",
                newName: "IX_SongDislike_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongViewHistory",
                table: "SongViewHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongLike",
                table: "SongLike",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongDislike",
                table: "SongDislike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SongDislike_Songs_SongId",
                table: "SongDislike",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongDislike_AspNetUsers_UserId",
                table: "SongDislike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongLike_Songs_SongId",
                table: "SongLike",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongLike_AspNetUsers_UserId",
                table: "SongLike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongViewHistory_Songs_SongId",
                table: "SongViewHistory",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SongViewHistory_AspNetUsers_UserId",
                table: "SongViewHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
