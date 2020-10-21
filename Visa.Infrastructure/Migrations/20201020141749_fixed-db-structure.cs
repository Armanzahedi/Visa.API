using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class fixeddbstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "ArticleComments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "ArticleComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_ArticleId",
                table: "ArticleComments",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_Articles_ArticleId",
                table: "ArticleComments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_Articles_ArticleId",
                table: "ArticleComments");

            migrationBuilder.DropIndex(
                name: "IX_ArticleComments_ArticleId",
                table: "ArticleComments");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "ArticleComments");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "ArticleComments");
        }
    }
}
