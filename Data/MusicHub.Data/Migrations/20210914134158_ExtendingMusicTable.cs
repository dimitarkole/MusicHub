using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicHub.Data.Migrations
{
    public partial class ExtendingMusicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusicLicenseType",
                table: "Musics");

            migrationBuilder.RenameColumn(
                name: "VisibleStatus",
                table: "Musics",
                newName: "Privacy");

            migrationBuilder.AddColumn<string>(
                name: "AlbumTitle",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AppPlayback",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Attribution",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyLink",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Composer",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainsExplicitContent",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainsMusic",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayEmbedCode",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableDirectDownloads",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ISRC",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISWC",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "License",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoDerivativeWorks",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Noncommercial",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OfflineListening",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PLine",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RSSfeed",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RecordLabel",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Musics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReleaseTitle",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShareALike",
                table: "Musics",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumTitle",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "AppPlayback",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "Attribution",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "BuyLink",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "Composer",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ContainsExplicitContent",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ContainsMusic",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "DisplayEmbedCode",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "EnableDirectDownloads",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ISRC",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ISWC",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "License",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "NoDerivativeWorks",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "Noncommercial",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "OfflineListening",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "PLine",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "RSSfeed",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "RecordLabel",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ReleaseTitle",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ShareALike",
                table: "Musics");

            migrationBuilder.RenameColumn(
                name: "Privacy",
                table: "Musics",
                newName: "VisibleStatus");

            migrationBuilder.AddColumn<int>(
                name: "MusicLicenseType",
                table: "Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
