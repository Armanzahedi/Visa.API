using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class Addedstaticcontententitiies2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaticContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 600, nullable: true),
                    LocalName = table.Column<string>(maxLength: 600, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticContentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 600, nullable: true),
                    FieldDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    StaticContentTypeId = table.Column<string>(nullable: true),
                    StaticContentTypeId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticContentDetails_StaticContentTypes_StaticContentTypeId1",
                        column: x => x.StaticContentTypeId1,
                        principalTable: "StaticContentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaticContentDetails_StaticContentTypeId1",
                table: "StaticContentDetails",
                column: "StaticContentTypeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaticContentDetails");

            migrationBuilder.DropTable(
                name: "StaticContentTypes");
        }
    }
}
