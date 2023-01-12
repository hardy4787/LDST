using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDST.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHostIdTypeToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playgrounds_Users_HostId1",
                table: "Playgrounds");

            migrationBuilder.DropIndex(
                name: "IX_Playgrounds_HostId1",
                table: "Playgrounds");

            migrationBuilder.DropColumn(
                name: "HostId1",
                table: "Playgrounds");

            migrationBuilder.AlterColumn<string>(
                name: "HostId",
                table: "Playgrounds",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Playgrounds_HostId",
                table: "Playgrounds",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playgrounds_Users_HostId",
                table: "Playgrounds",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playgrounds_Users_HostId",
                table: "Playgrounds");

            migrationBuilder.DropIndex(
                name: "IX_Playgrounds_HostId",
                table: "Playgrounds");

            migrationBuilder.AlterColumn<Guid>(
                name: "HostId",
                table: "Playgrounds",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "HostId1",
                table: "Playgrounds",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Playgrounds_HostId1",
                table: "Playgrounds",
                column: "HostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Playgrounds_Users_HostId1",
                table: "Playgrounds",
                column: "HostId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
