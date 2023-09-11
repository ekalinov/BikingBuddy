using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikingBuddy.Data.Migrations
{
    public partial class DefaultPhotosPathChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeamImageUrl",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "/images/default_team_image.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "/FileStorage/TeamPhotos/default_team_image.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "EventImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "/images/default_event_image.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "/FileStorage/EventPhotos/default_event_image.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "/images/default_user_icon.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "/FileStorage/UserPhotos/default_user_icon.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeamImageUrl",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "/FileStorage/TeamPhotos/default_team_image.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "/images/default_team_image.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "EventImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "/FileStorage/EventPhotos/default_event_image.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "/images/default_event_image.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "/FileStorage/UserPhotos/default_user_icon.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "/images/default_user_icon.jpg");
        }
    }
}
