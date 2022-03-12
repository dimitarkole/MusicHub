using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class addCommentsLikeDislike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentars_Songs_MusicId",
                table: "Commentars");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Categories_MusicCategoryId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_MusicCategoryId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Commentars_MusicId",
                table: "Commentars");

            migrationBuilder.DropColumn(
                name: "MusicCategoryId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "CountDisLikes",
                table: "Commentars");

            migrationBuilder.DropColumn(
                name: "CountLikes",
                table: "Commentars");

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "Commentars");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentId",
                table: "Commentars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "Commentars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SongId",
                table: "Commentars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommentDislikes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CommentId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentDislikes_Commentars_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Commentars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentLikes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CommentId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentLikes_Commentars_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Commentars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_CategoryId",
                table: "Songs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentars_CommentId",
                table: "Commentars",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentars_SongId",
                table: "Commentars",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDislikes_CommentId",
                table: "CommentDislikes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDislikes_IsDeleted",
                table: "CommentDislikes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDislikes_UserId",
                table: "CommentDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_CommentId",
                table: "CommentLikes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_IsDeleted",
                table: "CommentLikes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_UserId",
                table: "CommentLikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentars_Commentars_CommentId",
                table: "Commentars",
                column: "CommentId",
                principalTable: "Commentars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentars_Songs_SongId",
                table: "Commentars",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Categories_CategoryId",
                table: "Songs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentars_Commentars_CommentId",
                table: "Commentars");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentars_Songs_SongId",
                table: "Commentars");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Categories_CategoryId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "CommentDislikes");

            migrationBuilder.DropTable(
                name: "CommentLikes");

            migrationBuilder.DropIndex(
                name: "IX_Songs_CategoryId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Commentars_CommentId",
                table: "Commentars");

            migrationBuilder.DropIndex(
                name: "IX_Commentars_SongId",
                table: "Commentars");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Commentars");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "Commentars");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Commentars");

            migrationBuilder.AddColumn<string>(
                name: "MusicCategoryId",
                table: "Songs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CountDisLikes",
                table: "Commentars",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CountLikes",
                table: "Commentars",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MusicId",
                table: "Commentars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_MusicCategoryId",
                table: "Songs",
                column: "MusicCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentars_MusicId",
                table: "Commentars",
                column: "MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentars_Songs_MusicId",
                table: "Commentars",
                column: "MusicId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Categories_MusicCategoryId",
                table: "Songs",
                column: "MusicCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
