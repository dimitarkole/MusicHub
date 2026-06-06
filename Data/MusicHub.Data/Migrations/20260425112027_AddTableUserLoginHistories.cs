using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class AddTableUserLoginHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bells_AspNetUsers_BeelerId",
                table: "Bells");

            migrationBuilder.RenameColumn(
                name: "BeelerId",
                table: "Bells",
                newName: "BellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bells_BeelerId",
                table: "Bells",
                newName: "IX_Bells_BellerId");

            migrationBuilder.CreateTable(
                name: "UserLoginHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLoginHistories_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginHistories_UserId1",
                table: "UserLoginHistories",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bells_AspNetUsers_BellerId",
                table: "Bells",
                column: "BellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bells_AspNetUsers_BellerId",
                table: "Bells");

            migrationBuilder.DropTable(
                name: "UserLoginHistories");

            migrationBuilder.RenameColumn(
                name: "BellerId",
                table: "Bells",
                newName: "BeelerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bells_BellerId",
                table: "Bells",
                newName: "IX_Bells_BeelerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bells_AspNetUsers_BeelerId",
                table: "Bells",
                column: "BeelerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
