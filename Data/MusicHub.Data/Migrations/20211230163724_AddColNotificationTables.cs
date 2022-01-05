using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class AddColNotificationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClaims_NotificationClaims_NotificationClaimId",
                table: "NotificationClaims");

            migrationBuilder.DropIndex(
                name: "IX_NotificationClaims_NotificationClaimId",
                table: "NotificationClaims");

            migrationBuilder.DropColumn(
                name: "NotificationClaimId",
                table: "NotificationClaims");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "NotificationClaims",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "NotificationClaims");

            migrationBuilder.AddColumn<string>(
                name: "NotificationClaimId",
                table: "NotificationClaims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationClaims_NotificationClaimId",
                table: "NotificationClaims",
                column: "NotificationClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClaims_NotificationClaims_NotificationClaimId",
                table: "NotificationClaims",
                column: "NotificationClaimId",
                principalTable: "NotificationClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
