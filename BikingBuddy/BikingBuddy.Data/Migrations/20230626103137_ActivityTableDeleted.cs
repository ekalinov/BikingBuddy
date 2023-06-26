using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class ActivityTableDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Activity_ActivityId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "UsersActivities");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Events",
                newName: "ActivityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ActivityId",
                table: "Events",
                newName: "IX_Events_ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ActivityTypes_ActivityTypeId",
                table: "Events",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ActivityTypes_ActivityTypeId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ActivityTypeId",
                table: "Events",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ActivityTypeId",
                table: "Events",
                newName: "IX_Events_ActivityId");

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TotalAscent = table.Column<double>(type: "float", nullable: false),
                    TotalDistance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersActivities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersActivities", x => new { x.ActivityId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_UsersActivities_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersActivities_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ActivityTypeId",
                table: "Activity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersActivities_AppUserId",
                table: "UsersActivities",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Activity_ActivityId",
                table: "Events",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
