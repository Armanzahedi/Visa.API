using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class AddedArticleTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_UserId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleHeadLine_Article_ArticleId",
                table: "ArticleHeadLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTag_Article_ArticleId",
                table: "ArticleTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleTag",
                table: "ArticleTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleHeadLine",
                table: "ArticleHeadLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCategory",
                table: "ArticleCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.RenameTable(
                name: "ArticleTag",
                newName: "ArticleTags");

            migrationBuilder.RenameTable(
                name: "ArticleHeadLine",
                newName: "ArticleHeadLines");

            migrationBuilder.RenameTable(
                name: "ArticleCategory",
                newName: "ArticleCategories");

            migrationBuilder.RenameTable(
                name: "Article",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleTag_ArticleId",
                table: "ArticleTags",
                newName: "IX_ArticleTags_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleHeadLine_ArticleId",
                table: "ArticleHeadLines",
                newName: "IX_ArticleHeadLines_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_UserId",
                table: "Articles",
                newName: "IX_Articles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_ArticleCategoryId",
                table: "Articles",
                newName: "IX_Articles_ArticleCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleTags",
                table: "ArticleTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleHeadLines",
                table: "ArticleHeadLines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCategories",
                table: "ArticleCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArticleComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    Email = table.Column<string>(maxLength: 400, nullable: true),
                    Message = table.Column<string>(maxLength: 800, nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleComments_ArticleComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ArticleComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_ParentId",
                table: "ArticleComments",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleHeadLines_Articles_ArticleId",
                table: "ArticleHeadLines",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles",
                column: "ArticleCategoryId",
                principalTable: "ArticleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleHeadLines_Articles_ArticleId",
                table: "ArticleHeadLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropTable(
                name: "ArticleComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleTags",
                table: "ArticleTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleHeadLines",
                table: "ArticleHeadLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCategories",
                table: "ArticleCategories");

            migrationBuilder.RenameTable(
                name: "ArticleTags",
                newName: "ArticleTag");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Article");

            migrationBuilder.RenameTable(
                name: "ArticleHeadLines",
                newName: "ArticleHeadLine");

            migrationBuilder.RenameTable(
                name: "ArticleCategories",
                newName: "ArticleCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleTags_ArticleId",
                table: "ArticleTag",
                newName: "IX_ArticleTag_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_UserId",
                table: "Article",
                newName: "IX_Article_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ArticleCategoryId",
                table: "Article",
                newName: "IX_Article_ArticleCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleHeadLines_ArticleId",
                table: "ArticleHeadLine",
                newName: "IX_ArticleHeadLine_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleTag",
                table: "ArticleTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleHeadLine",
                table: "ArticleHeadLine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCategory",
                table: "ArticleCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryId",
                table: "Article",
                column: "ArticleCategoryId",
                principalTable: "ArticleCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_UserId",
                table: "Article",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleHeadLine_Article_ArticleId",
                table: "ArticleHeadLine",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTag_Article_ArticleId",
                table: "ArticleTag",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
