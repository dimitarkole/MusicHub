using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class NotificationClaimsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationClaims",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotificationClaimId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationClaims_NotificationClaims_NotificationClaimId",
                        column: x => x.NotificationClaimId,
                        principalTable: "NotificationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationClaims_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationClaims_NotificationClaimId",
                table: "NotificationClaims",
                column: "NotificationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationClaims_NotificationId",
                table: "NotificationClaims",
                column: "NotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationClaims");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notifications");
        }
    }
}
