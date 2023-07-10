using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class TownAndCountryAddedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bikes_BikeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Countries_CountryId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Municipalities_MunicipalityId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Towns_TownId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Events_MunicipalityId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BikeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MunicipalityId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BikeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Bikes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "AspNetUsers",
                type: "nvarchar(2)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TownId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_AppUserId",
                table: "Bikes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TownId",
                table: "AspNetUsers",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Towns_TownId",
                table: "AspNetUsers",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_AspNetUsers_AppUserId",
                table: "Bikes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Countries_CountryId",
                table: "Events",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Towns_TownId",
                table: "Events",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Towns_TownId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_AspNetUsers_AppUserId",
                table: "Bikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Countries_CountryId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Towns_TownId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_AppUserId",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TownId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MunicipalityId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BikeId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sofia-city" },
                    { 2, "Plovdiv" },
                    { 3, "Madan" },
                    { 4, "Smolyan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_MunicipalityId",
                table: "Events",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BikeId",
                table: "AspNetUsers",
                column: "BikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bikes_BikeId",
                table: "AspNetUsers",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Countries_CountryId",
                table: "Events",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Municipalities_MunicipalityId",
                table: "Events",
                column: "MunicipalityId",
                principalTable: "Municipalities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Towns_TownId",
                table: "Events",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
