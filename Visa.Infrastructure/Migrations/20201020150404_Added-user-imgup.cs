using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class Addeduserimgup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "AvatarName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
