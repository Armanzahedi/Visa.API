using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class fixedserviceincludes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 15, 40, 36, 905, DateTimeKind.Local).AddTicks(4850));

            migrationBuilder.UpdateData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 15, 40, 36, 905, DateTimeKind.Local).AddTicks(6129));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 15, 40, 36, 898, DateTimeKind.Local).AddTicks(2200));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29bd76db-5835-406d-ad1d-7a0901447c18",
                column: "ConcurrencyStamp",
                value: "aa268b5e-5a69-42e1-9320-7b96a362bad9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7be43da-622c-4cfe-98a9-5a5161120d24",
                column: "ConcurrencyStamp",
                value: "b9975cff-02b3-4e8e-8f79-5269274ad6bc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75625814-138e-4831-a1ea-bf58e211adff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdc68b0c-6f7a-410e-9918-ad9c005661d2", "AQAAAAEAACcQAAAAEFZnd9VxpHPQ9nFC8W1QAqoW9oqQxNQUlsvzwv9TcnZzxD9Ot60tbUiKcRyIb+ot6A==", "41029199-109a-496a-98f1-c4a4cfb43748" });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "services-details.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 14, 16, 29, 155, DateTimeKind.Local).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 14, 16, 29, 155, DateTimeKind.Local).AddTicks(3032));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 14, 16, 29, 148, DateTimeKind.Local).AddTicks(5638));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29bd76db-5835-406d-ad1d-7a0901447c18",
                column: "ConcurrencyStamp",
                value: "aed4e18c-3b4f-47bb-9399-c249e11c2775");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7be43da-622c-4cfe-98a9-5a5161120d24",
                column: "ConcurrencyStamp",
                value: "4b90b286-b2c0-4e90-a590-c40a846da593");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75625814-138e-4831-a1ea-bf58e211adff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef199578-67c9-4afe-bba4-2e35279ff551", "AQAAAAEAACcQAAAAEE1Nqh2grWdWP2IbLIyML+GQH69QV+wyvwDl5iECdhnfzzI04VcKxyEh/45+0ItqAQ==", "258a62de-04e0-488d-8a35-e8d94ad7d0f7" });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "gallery-img1.jpg");
        }
    }
}
