using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class EventPhotoGalleryTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventGalleryPhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGalleryPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGalleryPhoto_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventGalleryPhoto_EventId",
                table: "EventGalleryPhoto",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventGalleryPhoto");
        }
    }
}
