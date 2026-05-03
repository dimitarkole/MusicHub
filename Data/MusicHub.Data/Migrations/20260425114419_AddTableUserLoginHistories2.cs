using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class AddTableUserLoginHistories2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLoginHistories_AspNetUsers_UserId1",
                table: "UserLoginHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserLoginHistories_UserId1",
                table: "UserLoginHistories");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserLoginHistories");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserLoginHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginHistories_UserId",
                table: "UserLoginHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLoginHistories_AspNetUsers_UserId",
                table: "UserLoginHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLoginHistories_AspNetUsers_UserId",
                table: "UserLoginHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserLoginHistories_UserId",
                table: "UserLoginHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserLoginHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserLoginHistories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginHistories_UserId1",
                table: "UserLoginHistories",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLoginHistories_AspNetUsers_UserId1",
                table: "UserLoginHistories",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
