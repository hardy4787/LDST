using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDST.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FieldsNameRefactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_GameResarvations_GameReservationId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_GameResarvations_GameTimeSlots_GameTimeslotId",
                table: "GameResarvations");

            migrationBuilder.DropForeignKey(
                name: "FK_GameResarvations_Guests_GuestId",
                table: "GameResarvations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameResarvations",
                table: "GameResarvations");

            migrationBuilder.RenameTable(
                name: "GameResarvations",
                newName: "GameReservations");

            migrationBuilder.RenameColumn(
                name: "GameTimeslotId",
                table: "GameReservations",
                newName: "GameTimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_GameResarvations_GuestId",
                table: "GameReservations",
                newName: "IX_GameReservations_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_GameResarvations_GameTimeslotId",
                table: "GameReservations",
                newName: "IX_GameReservations_GameTimeSlotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameReservations",
                table: "GameReservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_GameReservations_GameReservationId",
                table: "Bills",
                column: "GameReservationId",
                principalTable: "GameReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameReservations_GameTimeSlots_GameTimeSlotId",
                table: "GameReservations",
                column: "GameTimeSlotId",
                principalTable: "GameTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameReservations_Guests_GuestId",
                table: "GameReservations",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_GameReservations_GameReservationId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_GameReservations_GameTimeSlots_GameTimeSlotId",
                table: "GameReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_GameReservations_Guests_GuestId",
                table: "GameReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReservations",
                table: "GameReservations");

            migrationBuilder.RenameTable(
                name: "GameReservations",
                newName: "GameResarvations");

            migrationBuilder.RenameColumn(
                name: "GameTimeSlotId",
                table: "GameResarvations",
                newName: "GameTimeslotId");

            migrationBuilder.RenameIndex(
                name: "IX_GameReservations_GuestId",
                table: "GameResarvations",
                newName: "IX_GameResarvations_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_GameReservations_GameTimeSlotId",
                table: "GameResarvations",
                newName: "IX_GameResarvations_GameTimeslotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameResarvations",
                table: "GameResarvations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_GameResarvations_GameReservationId",
                table: "Bills",
                column: "GameReservationId",
                principalTable: "GameResarvations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameResarvations_GameTimeSlots_GameTimeslotId",
                table: "GameResarvations",
                column: "GameTimeslotId",
                principalTable: "GameTimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameResarvations_Guests_GuestId",
                table: "GameResarvations",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
