using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class SubscriptionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionClaims_Subscriptions_PayPlanId",
                table: "SubscriptionClaims");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "PayPlanId",
                table: "SubscriptionClaims",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionClaims_PayPlanId",
                table: "SubscriptionClaims",
                newName: "IX_SubscriptionClaims_SubscriptionId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionClaims_Subscriptions_SubscriptionId",
                table: "SubscriptionClaims",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionClaims_Subscriptions_SubscriptionId",
                table: "SubscriptionClaims");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "SubscriptionClaims",
                newName: "PayPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionClaims_SubscriptionId",
                table: "SubscriptionClaims",
                newName: "IX_SubscriptionClaims_PayPlanId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionClaims_Subscriptions_PayPlanId",
                table: "SubscriptionClaims",
                column: "PayPlanId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
