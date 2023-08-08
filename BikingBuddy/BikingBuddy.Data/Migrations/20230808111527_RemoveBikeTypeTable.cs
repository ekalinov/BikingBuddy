using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class RemoveBikeTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikeTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BikeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BikeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mountain Bike" },
                    { 2, "Road Bike" },
                    { 3, "Gravel Bike" },
                    { 4, "Urban Bike" },
                    { 5, "Downhill Bike" },
                    { 6, "Trail Bike" },
                    { 7, "Fat Bike" },
                    { 8, "BMX" },
                    { 9, "Other" }
                });
        }
    }
}
