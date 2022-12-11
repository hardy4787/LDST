using LDST.Domain.Bill.ValueObjects;
using LDST.Domain.Common.Models;
using LDST.Domain.Common.ValueObjects;
using LDST.Domain.GameReservation.ValueObjects;
using LDST.Domain.Playground.ValueObjects;
using LDST.Domain.User.ValueObjects;

namespace LDST.Domain.Bill
{
    public sealed class Bill : AggregateRoot<BillId>
    {
        public GameReservationId GameReservationId { get; }
        public GuestId UserId { get; }
        public Price Amount { get; }
        public DateTime CreatedDateTime { get; }
        public DateTime UpdateDateTime { get; set; }

        private Bill(
            GameReservationId gameReservationId,
            GuestId userId,
            Price price,
            DateTime createdDateTime,
            DateTime updateDateTime
        ) : base(BillId.Create(gameReservationId, userId))
        {
            GameReservationId = gameReservationId;
            UserId = userId;
            Amount = price;
            CreatedDateTime = createdDateTime;
            UpdateDateTime = updateDateTime;
        }

        public static Bill Create(
            GameReservationId gameReservationId,
            GuestId userId,
            Price price,
            DateTime createdDateTime,
            DateTime updateDateTime)
        {
            return new Bill(gameReservationId, userId, price, createdDateTime, updateDateTime);
        }
    }
}