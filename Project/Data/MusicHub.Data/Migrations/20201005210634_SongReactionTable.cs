using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class SongReactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongDislikes");

            migrationBuilder.DropTable(
                name: "SongLikes");

            migrationBuilder.CreateTable(
                name: "SongReactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SongId = table.Column<string>(nullable: true),
                    Reaction = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongReactions_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongReactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongReactions_IsDeleted",
                table: "SongReactions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SongReactions_SongId",
                table: "SongReactions",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongReactions_UserId",
                table: "SongReactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongReactions");

            migrationBuilder.CreateTable(
                name: "SongDislikes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SongId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongDislikes_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SongLikes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SongId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongLikes_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongDislikes_IsDeleted",
                table: "SongDislikes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SongDislikes_SongId",
                table: "SongDislikes",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDislikes_UserId",
                table: "SongDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SongLikes_IsDeleted",
                table: "SongLikes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SongLikes_SongId",
                table: "SongLikes",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongLikes_UserId",
                table: "SongLikes",
                column: "UserId");
        }
    }
}
