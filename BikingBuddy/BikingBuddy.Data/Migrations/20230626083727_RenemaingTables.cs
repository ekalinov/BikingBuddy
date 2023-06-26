using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class RenemaingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bike_BikeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Bike_BikeType_BikeTypeId",
                table: "Bike");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Event_EventId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Activity_ActivityId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_OrganizerId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Country_CountryId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Municipality_MunicipalityId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Town_TownId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsParticipants_Event_EventId",
                table: "EventsParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Town",
                table: "Town");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Municipality",
                table: "Municipality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BikeType",
                table: "BikeType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bike",
                table: "Bike");

            migrationBuilder.RenameTable(
                name: "Town",
                newName: "Towns");

            migrationBuilder.RenameTable(
                name: "Municipality",
                newName: "Municipalities");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "BikeType",
                newName: "BikeTypes");

            migrationBuilder.RenameTable(
                name: "Bike",
                newName: "Bikes");

            migrationBuilder.RenameIndex(
                name: "IX_Event_TownId",
                table: "Events",
                newName: "IX_Events_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_OrganizerId",
                table: "Events",
                newName: "IX_Events_OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_MunicipalityId",
                table: "Events",
                newName: "IX_Events_MunicipalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_CountryId",
                table: "Events",
                newName: "IX_Events_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ActivityId",
                table: "Events",
                newName: "IX_Events_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_EventId",
                table: "Comments",
                newName: "IX_Comments_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Bike_BikeTypeId",
                table: "Bikes",
                newName: "IX_Bikes_BikeTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Towns",
                table: "Towns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Municipalities",
                table: "Municipalities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BikeTypes",
                table: "BikeTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bikes",
                table: "Bikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bikes_BikeId",
                table: "AspNetUsers",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_BikeTypes_BikeTypeId",
                table: "Bikes",
                column: "BikeTypeId",
                principalTable: "BikeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Activity_ActivityId",
                table: "Events",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_EventsParticipants_Events_EventId",
                table: "EventsParticipants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bikes_BikeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_BikeTypes_BikeTypeId",
                table: "Bikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Activity_ActivityId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Countries_CountryId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Municipalities_MunicipalityId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Towns_TownId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsParticipants_Events_EventId",
                table: "EventsParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Towns",
                table: "Towns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Municipalities",
                table: "Municipalities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BikeTypes",
                table: "BikeTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bikes",
                table: "Bikes");

            migrationBuilder.RenameTable(
                name: "Towns",
                newName: "Town");

            migrationBuilder.RenameTable(
                name: "Municipalities",
                newName: "Municipality");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "BikeTypes",
                newName: "BikeType");

            migrationBuilder.RenameTable(
                name: "Bikes",
                newName: "Bike");

            migrationBuilder.RenameIndex(
                name: "IX_Events_TownId",
                table: "Event",
                newName: "IX_Event_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganizerId",
                table: "Event",
                newName: "IX_Event_OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_MunicipalityId",
                table: "Event",
                newName: "IX_Event_MunicipalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CountryId",
                table: "Event",
                newName: "IX_Event_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ActivityId",
                table: "Event",
                newName: "IX_Event_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_EventId",
                table: "Comment",
                newName: "IX_Comment_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Bikes_BikeTypeId",
                table: "Bike",
                newName: "IX_Bike_BikeTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Town",
                table: "Town",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Municipality",
                table: "Municipality",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BikeType",
                table: "BikeType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bike",
                table: "Bike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bike_BikeId",
                table: "AspNetUsers",
                column: "BikeId",
                principalTable: "Bike",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bike_BikeType_BikeTypeId",
                table: "Bike",
                column: "BikeTypeId",
                principalTable: "BikeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Event_EventId",
                table: "Comment",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Activity_ActivityId",
                table: "Event",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_OrganizerId",
                table: "Event",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Country_CountryId",
                table: "Event",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Municipality_MunicipalityId",
                table: "Event",
                column: "MunicipalityId",
                principalTable: "Municipality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Town_TownId",
                table: "Event",
                column: "TownId",
                principalTable: "Town",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsParticipants_Event_EventId",
                table: "EventsParticipants",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
