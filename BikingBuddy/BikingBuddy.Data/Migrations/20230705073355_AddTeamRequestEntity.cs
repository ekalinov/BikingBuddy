using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class AddTeamRequestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_TeamManagerId",
                table: "Teams");

            migrationBuilder.CreateTable(
                name: "TeamsRequests",
                columns: table => new
                {
                    RequestFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsRequests", x => new { x.RequestFromId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamsRequests_AspNetUsers_RequestFromId",
                        column: x => x.RequestFromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamsRequests_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamsRequests_TeamId",
                table: "TeamsRequests",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_TeamManagerId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TeamsRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
