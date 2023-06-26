using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class RideTypeNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_RideTypes_RideTypeId",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RideTypes",
                table: "RideTypes");

            migrationBuilder.RenameTable(
                name: "RideTypes",
                newName: "ActivityTypes");

            migrationBuilder.RenameColumn(
                name: "RideTypeId",
                table: "Activity",
                newName: "ActivityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_RideTypeId",
                table: "Activity",
                newName: "IX_Activity_ActivityTypeId");

          

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Activity",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTypes",
                table: "ActivityTypes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Sofia-city");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Plovdiv");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Madan");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Smolyan");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_ActivityTypes_ActivityTypeId",
                table: "Activity",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_ActivityTypes_ActivityTypeId",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTypes",
                table: "ActivityTypes");

            migrationBuilder.RenameTable(
                name: "ActivityTypes",
                newName: "RideTypes");

            migrationBuilder.RenameColumn(
                name: "ActivityTypeId",
                table: "Activity",
                newName: "RideTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_ActivityTypeId",
                table: "Activity",
                newName: "IX_Activity_RideTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Activity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RideTypes",
                table: "RideTypes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Sofia-city Municipality");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Plovdiv Municipality");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Madan Municipality");

            migrationBuilder.UpdateData(
                table: "Municipality",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Smolyan Municipality");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_RideTypes_RideTypeId",
                table: "Activity",
                column: "RideTypeId",
                principalTable: "RideTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
