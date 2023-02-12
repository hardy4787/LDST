using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LDST.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoPathForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10884d21-1f59-44cb-bf62-27be3b645638");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ee418c3-7260-4f1e-8727-0550ff31de23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca9f562-e715-473c-acf5-47d068bfd495");

            migrationBuilder.AddColumn<string>(
                name: "TitlePhotoPath",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12f67b23-b231-4631-bf46-6761d2b447df", null, "Guest", "GUEST" },
                    { "752ded75-1ddd-4686-98fb-abcff568464a", null, "Host", "HOST" },
                    { "c11f9eed-9ffd-4e69-8aeb-01e01894b703", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12f67b23-b231-4631-bf46-6761d2b447df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "752ded75-1ddd-4686-98fb-abcff568464a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c11f9eed-9ffd-4e69-8aeb-01e01894b703");

            migrationBuilder.DropColumn(
                name: "TitlePhotoPath",
                table: "Users");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10884d21-1f59-44cb-bf62-27be3b645638", null, "Guest", "GUEST" },
                    { "1ee418c3-7260-4f1e-8727-0550ff31de23", null, "Administrator", "ADMINISTRATOR" },
                    { "7ca9f562-e715-473c-acf5-47d068bfd495", null, "Host", "HOST" }
                });
        }
    }
}
