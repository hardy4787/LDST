using LDST.Domain.Common.Models;
using LDST.Domain.GameReservation;
using LDST.Domain.GameReservation.ValueObjects;
using LDST.Domain.Playground.ValueObjects;
using LDST.Domain.User.ValueObjects;

namespace LDST.Domain.Bill.ValueObjects
{
    public sealed class BillId : ValueObject
    {
        public string Value { get; }

        public BillId(string value)
        {
            Value = value;
        }

        private BillId(GameReservationId gameReservationId, GuestId userId)
        {
            Value = $"Bill_{gameReservationId.Value}_{userId.Value}";
        }

        public static BillId Create(GameReservationId gameReservationId, GuestId userId)
        {
            return new BillId(gameReservationId, userId);
        }

        public static BillId Create(string value)
        {
            return new BillId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}