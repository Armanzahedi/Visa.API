using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class Addedarticleentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ArticleTags",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleCategoryId",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles",
                column: "ArticleCategoryId",
                principalTable: "ArticleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "ArticleTags",
                type: "int",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleCategoryId",
                table: "Articles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles",
                column: "ArticleCategoryId",
                principalTable: "ArticleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
