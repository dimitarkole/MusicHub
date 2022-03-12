using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class addLikeDislikeHistorySongTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.DropForeignKey(
                name: "FK_Songs_Categories_Id",
                table: "Songs");*/

            migrationBuilder.DropColumn(
                name: "CountDisLikes",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "CountLikes",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "SongDislike",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SongId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDislike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongDislike_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongDislike_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SongLike",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SongId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongLike_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongLike_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SongViewHistory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SongId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongViewHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongViewHistory_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongViewHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongDislike_IsDeleted",
                table: "SongDislike",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SongDislike_SongId",
                table: "SongDislike",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDislike_UserId",
                table: "SongDislike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SongLike_IsDeleted",
                table: "SongLike",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SongLike_SongId",
                table: "SongLike",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongLike_UserId",
                table: "SongLike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SongViewHistory_IsDeleted",
                table: "SongViewHistory",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SongViewHistory_SongId",
                table: "SongViewHistory",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongViewHistory_UserId",
                table: "SongViewHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongDislike");

            migrationBuilder.DropTable(
                name: "SongLike");

            migrationBuilder.DropTable(
                name: "SongViewHistory");

            migrationBuilder.AddColumn<long>(
                name: "CountDisLikes",
                table: "Songs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CountLikes",
                table: "Songs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

           /* migrationBuilder.AddForeignKey(
                name: "FK_Songs_Categories_Id",
                table: "Songs",
                column: "Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);*/
        }
    }
}
