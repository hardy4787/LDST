using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDST.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddress3ToPlayground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address3",
                table: "Playgrounds",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address3",
                table: "Playgrounds");
        }
    }
}
