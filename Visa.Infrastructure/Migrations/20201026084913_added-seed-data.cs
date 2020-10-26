using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class addedseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ArticleCategories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Content Marketing" },
                    { 2, "Data Analysis" },
                    { 3, "Digital Marketing" },
                    { 4, "Web Analytics" },
                    { 5, "Social Marketing" },
                    { 6, "Great Speakers" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29bd76db-5835-406d-ad1d-7a0901447c18", "3e86f118-920d-4143-a1d1-adc589fc1f6e", "Admin", "ADMIN" },
                    { "d7be43da-622c-4cfe-98a9-5a5161120d24", "5c5b43ff-cced-45c6-a333-14e8cfb50445", "Author", "AUTHOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Information", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "75625814-138e-4831-a1ea-bf58e211adff", 0, null, "b3fc1009-ecd5-4054-a479-03807708b178", "Admin@Admin.com", false, "Admin", null, "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAENlhHEQ8LiPv/V7YU/af3gSwwEc0QqBfwoRV0pMFTvBUspoo9wczVskhZmeYshWTOg==", null, false, "3fa46fba-9901-499c-bb7c-42d10fa7b4ac", false, "Admin" });

            migrationBuilder.InsertData(
                table: "StaticContentTypes",
                columns: new[] { "Id", "Identifier", "Name" },
                values: new object[] { 1, "about-us", "About Us" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "AddedDate", "ArticleCategoryId", "Description", "Image", "Title", "UserId", "ViewCount" },
                values: new object[] { 1, new DateTime(2020, 10, 26, 12, 19, 12, 131, DateTimeKind.Local).AddTicks(5512), 1, "نحوه گرفتن اقامت رایگان", null, "نحوه گرفتن اقامت رایگان", "75625814-138e-4831-a1ea-bf58e211adff", 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "75625814-138e-4831-a1ea-bf58e211adff", "29bd76db-5835-406d-ad1d-7a0901447c18" });

            migrationBuilder.InsertData(
                table: "StaticContentDetails",
                columns: new[] { "Id", "Description", "FieldDescription", "Identifier", "Image", "Link", "StaticContentTypeId", "Title" },
                values: new object[,]
                {
                    { 1, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "Finalist", "main", "about-us.jpg", null, 1, "SEO Agency of The Year" },
                    { 2, null, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utsa.", "first-row", null, null, 1, "Increased Traffic" },
                    { 3, null, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utsa.", "second-row", null, null, 1, "Cost-Effectiveness" },
                    { 4, null, "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utsa.", "third-row", null, null, 1, "Increased Site Usability" }
                });

            migrationBuilder.InsertData(
                table: "ArticleComments",
                columns: new[] { "Id", "AddedDate", "ArticleId", "Email", "Message", "Name", "ParentId" },
                values: new object[] { 1, new DateTime(2020, 10, 26, 12, 19, 12, 139, DateTimeKind.Local).AddTicks(4222), 1, "User@Comment.com", "This is a test comment", "User", null });

            migrationBuilder.InsertData(
                table: "ArticleHeadLines",
                columns: new[] { "Id", "ArticleId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 1, " اثبات کنید که طرح تجاری یا ایده شما توسط یکی از سازمان های معرفی شده حمایت می شود. در این حالت این شرکت‌ها نامه‌ای مبنی بر پذیرفته شدن طرح تجاری شما خواهند داد.", "شرایط ویزای استارت آپ کانادا" },
                    { 2, 1, "Test", "هزینه ویزای استارت آپ کانادا" }
                });

            migrationBuilder.InsertData(
                table: "ArticleTags",
                columns: new[] { "Id", "ArticleId", "Title" },
                values: new object[] { 1, 1, "Test Tag" });

            migrationBuilder.InsertData(
                table: "ArticleComments",
                columns: new[] { "Id", "AddedDate", "ArticleId", "Email", "Message", "Name", "ParentId" },
                values: new object[] { 2, new DateTime(2020, 10, 26, 12, 19, 12, 139, DateTimeKind.Local).AddTicks(5296), 1, "User2@Comment.com", "This is a test comment reply", "User2", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArticleHeadLines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ArticleHeadLines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArticleTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7be43da-622c-4cfe-98a9-5a5161120d24");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "75625814-138e-4831-a1ea-bf58e211adff", "29bd76db-5835-406d-ad1d-7a0901447c18" });

            migrationBuilder.DeleteData(
                table: "StaticContentDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StaticContentDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StaticContentDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StaticContentDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29bd76db-5835-406d-ad1d-7a0901447c18");

            migrationBuilder.DeleteData(
                table: "StaticContentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75625814-138e-4831-a1ea-bf58e211adff");
        }
    }
}
