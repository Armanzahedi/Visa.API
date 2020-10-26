using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class addedcontentidentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalName",
                table: "StaticContentTypes");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "StaticContentTypes",
                maxLength: 600,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "StaticContentDetails",
                maxLength: 600,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "StaticContentTypes");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "StaticContentDetails");

            migrationBuilder.AddColumn<string>(
                name: "LocalName",
                table: "StaticContentTypes",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true);
        }
    }
}
