using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class AddEventLocationAndRelationToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventsLocations",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false,defaultValue: 23.319999999999998),
                    Latitude = table.Column<double>(type: "float", nullable: false, defaultValue: 42.689999999999998)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsLocations", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_EventsLocations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsLocations");
        }
    }
}
