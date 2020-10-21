using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class fixedcontentdetailentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticContentDetails_StaticContentTypes_StaticContentTypeId1",
                table: "StaticContentDetails");

            migrationBuilder.DropIndex(
                name: "IX_StaticContentDetails_StaticContentTypeId1",
                table: "StaticContentDetails");

            migrationBuilder.DropColumn(
                name: "StaticContentTypeId1",
                table: "StaticContentDetails");

            migrationBuilder.AlterColumn<int>(
                name: "StaticContentTypeId",
                table: "StaticContentDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticContentDetails_StaticContentTypeId",
                table: "StaticContentDetails",
                column: "StaticContentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticContentDetails_StaticContentTypes_StaticContentTypeId",
                table: "StaticContentDetails",
                column: "StaticContentTypeId",
                principalTable: "StaticContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticContentDetails_StaticContentTypes_StaticContentTypeId",
                table: "StaticContentDetails");

            migrationBuilder.DropIndex(
                name: "IX_StaticContentDetails_StaticContentTypeId",
                table: "StaticContentDetails");

            migrationBuilder.AlterColumn<string>(
                name: "StaticContentTypeId",
                table: "StaticContentDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "StaticContentTypeId1",
                table: "StaticContentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticContentDetails_StaticContentTypeId1",
                table: "StaticContentDetails",
                column: "StaticContentTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticContentDetails_StaticContentTypes_StaticContentTypeId1",
                table: "StaticContentDetails",
                column: "StaticContentTypeId1",
                principalTable: "StaticContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
