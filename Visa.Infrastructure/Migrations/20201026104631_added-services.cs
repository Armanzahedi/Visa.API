using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Visa.Infrastructure.Migrations
{
    public partial class addedservices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faq",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    Google = table.Column<string>(nullable: true),
                    Linkedin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 600, nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FileInfo = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Speaker = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 600, nullable: true),
                    Phone = table.Column<string>(maxLength: 600, nullable: true),
                    Email = table.Column<string>(maxLength: 600, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactForms_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceIncludes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 700, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ServcieId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceIncludes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceIncludes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.InsertData(
                table: "Faq",
                columns: new[] { "Id", "Answer", "Question" },
                values: new object[,]
                {
                    { 1, "An Interior Designer is a trained professional who creates and designs interior spaces which are aesthetically attractive and functional.An Interior Decorator, on the other hand, views interior design with a largely cosmetic approach using decorative elements to merely rearrange existing spaces.", "What is your company philosophy?" },
                    { 2, "An Interior Designer is a trained professional who creates and designs interior spaces which are aesthetically attractive and functional.An Interior Decorator, on the other hand, views interior design with a largely cosmetic approach using decorative elements to merely rearrange existing spaces.", "What did you do to make it a success?" }
                });

            migrationBuilder.InsertData(
                table: "Galleries",
                columns: new[] { "Id", "Image", "Title" },
                values: new object[,]
                {
                    { 1, "gallery-img1.jpg", "Gallery Image 1" },
                    { 2, "gallery-img2.jpg", "Gallery Image 2" }
                });

            migrationBuilder.InsertData(
                table: "OurTeams",
                columns: new[] { "Id", "Facebook", "Google", "Image", "Linkedin", "Name", "Role", "Twitter" },
                values: new object[,]
                {
                    { 4, null, null, "our-team-img-4.jpg", null, "EMY JACMAN", "CEO & FOUNDER", null },
                    { 2, null, null, "our-team-img-2.jpg", null, "LES WILLIAMS", "CEO & FOUNDER", null },
                    { 3, null, null, "our-team-img-3.jpg", null, "SARA STEWART", "CEO & FOUNDER", null },
                    { 1, null, null, "our-team-img-1.jpg", null, "NOUR ELDIN", "CEO & FOUNDER", null }
                });

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "Image", "Title" },
                values: new object[,]
                {
                    { 1, "partner-1.png", "ThemeForest" },
                    { 2, "partner-2.png", "Audio Jungle" },
                    { 3, "partner-3.png", "Codcanyon" },
                    { 4, "partner-4.png", "Graphic River" }
                });

            migrationBuilder.InsertData(
                table: "ServiceIncludes",
                columns: new[] { "Id", "Description", "ServcieId", "ServiceId", "Title" },
                values: new object[] { 1, "We denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleas ure of the moment, so blinded by desire, that they cannot foresee the pain and the trouble that are bound to ensue; and equal blame belongs to those who fail demoralized by the charms in their duty through the weakness of will so blinded by desire", 1, null, "Social Marketing" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Address", "Description", "Email", "File", "FileInfo", "Image", "Phone", "ShortDescription", "Thumbnail", "Title" },
                values: new object[,]
                {
                    { 5, null, null, null, null, null, "service-icon5.png", null, "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et", null, "Web Analytics" },
                    { 6, null, null, null, null, null, "service-icon6.png", null, "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et", null, "Social Marketing" },
                    { 4, null, null, null, null, null, "service-icon4.png", null, "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et", null, "Digital Marketing" },
                    { 3, null, null, null, null, null, "service-icon3.png", null, "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et", null, "Data Analysis" },
                    { 2, null, null, null, null, null, "service-icon2.png", null, "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et", null, "Content Marketing" },
                    { 1, "503 Old Buffalo Street Northwest#205, New York-3087", "We denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleas ure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrink ing from toil and pain.", "info@sbtechnosoft.com", "dummy.pdf", "Impress clients new and existing with elite construction brochures. Impress clients new and existing with elite construction.", "gallery-img1.jpg", "+0123-505-6789", "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et", "service-icon1.png", "SEO" }
                });

            migrationBuilder.InsertData(
                table: "Testimonials",
                columns: new[] { "Id", "Message", "Rate", "Speaker" },
                values: new object[] { 1, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", 5, "Ariana Hedge CEO, Devrise" });

            migrationBuilder.CreateIndex(
                name: "IX_ContactForms_ServiceId",
                table: "ContactForms",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceIncludes_ServiceId",
                table: "ServiceIncludes",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactForms");

            migrationBuilder.DropTable(
                name: "Faq");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "OurTeams");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "ServiceIncludes");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.UpdateData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 12, 19, 12, 139, DateTimeKind.Local).AddTicks(4222));

            migrationBuilder.UpdateData(
                table: "ArticleComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 12, 19, 12, 139, DateTimeKind.Local).AddTicks(5296));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedDate",
                value: new DateTime(2020, 10, 26, 12, 19, 12, 131, DateTimeKind.Local).AddTicks(5512));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29bd76db-5835-406d-ad1d-7a0901447c18",
                column: "ConcurrencyStamp",
                value: "3e86f118-920d-4143-a1d1-adc589fc1f6e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7be43da-622c-4cfe-98a9-5a5161120d24",
                column: "ConcurrencyStamp",
                value: "5c5b43ff-cced-45c6-a333-14e8cfb50445");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75625814-138e-4831-a1ea-bf58e211adff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3fc1009-ecd5-4054-a479-03807708b178", "AQAAAAEAACcQAAAAENlhHEQ8LiPv/V7YU/af3gSwwEc0QqBfwoRV0pMFTvBUspoo9wczVskhZmeYshWTOg==", "3fa46fba-9901-499c-bb7c-42d10fa7b4ac" });
        }
    }
}
