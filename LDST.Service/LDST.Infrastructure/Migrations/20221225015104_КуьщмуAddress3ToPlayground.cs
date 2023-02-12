using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDST.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class КуьщмуAddress3ToPlayground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address3",
                table: "Playgrounds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address3",
                table: "Playgrounds",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
