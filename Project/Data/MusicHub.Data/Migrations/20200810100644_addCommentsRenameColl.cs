using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class addCommentsRenameColl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentars_Commentars_CommentId",
                table: "Commentars");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentars_Songs_SongId",
                table: "Commentars");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentars_AspNetUsers_UserId",
                table: "Commentars");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentDislikes_Commentars_CommentId",
                table: "CommentDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Commentars_CommentId",
                table: "CommentLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commentars",
                table: "Commentars");

            migrationBuilder.DropIndex(
                name: "IX_Commentars_CommentId",
                table: "Commentars");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Commentars");

            migrationBuilder.RenameTable(
                name: "Commentars",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Commentars_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Commentars_SongId",
                table: "Comments",
                newName: "IX_Comments_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Commentars_IsDeleted",
                table: "Comments",
                newName: "IX_Comments_IsDeleted");

            migrationBuilder.AlterColumn<string>(
                name: "ParentCommentId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDislikes_Comments_CommentId",
                table: "CommentDislikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_Comments_CommentId",
                table: "CommentLikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Songs_SongId",
                table: "Comments",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentDislikes_Comments_CommentId",
                table: "CommentDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Comments_CommentId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Songs_SongId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Commentars");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Commentars",
                newName: "IX_Commentars_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_SongId",
                table: "Commentars",
                newName: "IX_Commentars_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_IsDeleted",
                table: "Commentars",
                newName: "IX_Commentars_IsDeleted");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCommentId",
                table: "Commentars",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentId",
                table: "Commentars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commentars",
                table: "Commentars",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Commentars_CommentId",
                table: "Commentars",
                column: "CommentId");

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
                name: "FK_Commentars_AspNetUsers_UserId",
                table: "Commentars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDislikes_Commentars_CommentId",
                table: "CommentDislikes",
                column: "CommentId",
                principalTable: "Commentars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_Commentars_CommentId",
                table: "CommentLikes",
                column: "CommentId",
                principalTable: "Commentars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
