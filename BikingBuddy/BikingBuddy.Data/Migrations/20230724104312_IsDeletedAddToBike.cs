using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class IsDeletedAddToBike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bikes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bikes");
        }
    }
}
