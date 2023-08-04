using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class GalleryPhotosNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventGalleryPhoto_Events_EventId",
                table: "EventGalleryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventGalleryPhoto",
                table: "EventGalleryPhoto");

            migrationBuilder.RenameTable(
                name: "EventGalleryPhoto",
                newName: "EventGalleryPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_EventGalleryPhoto_EventId",
                table: "EventGalleryPhotos",
                newName: "IX_EventGalleryPhotos_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventGalleryPhotos",
                table: "EventGalleryPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventGalleryPhotos_Events_EventId",
                table: "EventGalleryPhotos",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventGalleryPhotos_Events_EventId",
                table: "EventGalleryPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventGalleryPhotos",
                table: "EventGalleryPhotos");

            migrationBuilder.RenameTable(
                name: "EventGalleryPhotos",
                newName: "EventGalleryPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_EventGalleryPhotos_EventId",
                table: "EventGalleryPhoto",
                newName: "IX_EventGalleryPhoto_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventGalleryPhoto",
                table: "EventGalleryPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventGalleryPhoto_Events_EventId",
                table: "EventGalleryPhoto",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
