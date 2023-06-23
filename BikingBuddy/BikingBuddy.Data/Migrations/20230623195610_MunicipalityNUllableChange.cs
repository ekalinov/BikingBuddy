using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class MunicipalityNUllableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Activity_RideTypeId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Municipality_MunicipalityId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "RideTypeId",
                table: "Event",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_RideTypeId",
                table: "Event",
                newName: "IX_Event_ActivityId");

            migrationBuilder.AlterColumn<int>(
                name: "MunicipalityId",
                table: "Event",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Activity_ActivityId",
                table: "Event",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Municipality_MunicipalityId",
                table: "Event",
                column: "MunicipalityId",
                principalTable: "Municipality",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Activity_ActivityId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Municipality_MunicipalityId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Event",
                newName: "RideTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ActivityId",
                table: "Event",
                newName: "IX_Event_RideTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "MunicipalityId",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Activity_RideTypeId",
                table: "Event",
                column: "RideTypeId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Municipality_MunicipalityId",
                table: "Event",
                column: "MunicipalityId",
                principalTable: "Municipality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
