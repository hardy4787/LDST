using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDST.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewFieldToPlayground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reviewed",
                table: "Playgrounds",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reviewed",
                table: "Playgrounds");
        }
    }
}
