using LDST.Domain.Bill.ValueObjects;
using LDST.Domain.Common.Models;
using LDST.Domain.GameReservation.Enums;
using LDST.Domain.GameReservation.ValueObjects;
using LDST.Domain.GameTimeslot.ValueObjects;
using LDST.Domain.User.ValueObjects;

namespace LDST.Domain.GameReservation
{
    public sealed class GameReservation : AggregateRoot<GameReservationId>
    {
        public GameTimeslotId GameTimeslotId { get; }
        public GuestId GuestId { get; }
        public ReservationStatus Status { get; }
        public BillId? BillId { get; }

        private GameReservation(GameTimeslotId gameTimeslotId, GuestId guestId, ReservationStatus status, BillId? billId) : base(GameReservationId.Create(gameTimeslotId))
        {
            GameTimeslotId = gameTimeslotId;
            GuestId = guestId;
            Status = status;
            BillId = billId;
        }

        public static GameReservation Create(GameTimeslotId gameTimeslotId, GuestId guestId, ReservationStatus status, BillId? billId = null)
        {
            return new GameReservation(gameTimeslotId, guestId, status, billId);
        }
    }
}
